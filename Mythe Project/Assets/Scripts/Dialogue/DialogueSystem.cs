using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] DialogueTrigger[] _triggers;

    public event Action EndImage;
    public event Action<Sprite> ChangeImage;
    public event Action<string> ChangeText;

    IEnumerator[] playingCoroutine = new IEnumerator[2];
    int triggerTurn;

    void Awake()
    {
        if (_triggers != null)
        {
            for (int i = 0; i < _triggers.Length; i++) {
                _triggers[i].Scene += TriggerScene;
                _triggers[i].NextTrigger += GoToNextScene;
            }

            for (int i = 1; i < _triggers.Length; i++) {
                _triggers[i].gameObject.SetActive(false);
            }
        }

        triggerTurn = 0;
    }

    void TriggerScene(DialogueScene aScene)
    {
        for (int i = 0; i < playingCoroutine.Length; i++)
        {
            if (playingCoroutine[i] != null) {
                StopCoroutine(playingCoroutine[i]);
            }
        }

        playingCoroutine[0] = WaitForNextDialogue(aScene);

        if (aScene.Sentences.Length > 0) {
            ChangeText(aScene.Sentences[0]);
            StartCoroutine(playingCoroutine[0]);
        }

        if (aScene.GetType().ToString().Equals(Constants.ClassType.CUTSCENE))
        {
            TriggerSlide((Cutscene)aScene);
        }
    }

    void TriggerSlide(Cutscene aScene)
    {
        playingCoroutine[1] = WaitForNextImage(aScene);

        if (aScene.Images.Length > 0) {
            ChangeImage(aScene.Images[0]);
            StartCoroutine(playingCoroutine[1]);
        }
    }

    void GoToNextScene()
    {
        _triggers[triggerTurn].gameObject.SetActive(false);
        if (triggerTurn < _triggers.Length - 1)
        {
            triggerTurn++;
            _triggers[triggerTurn].gameObject.SetActive(true);
        }
    }

    void CheckForLoop(DialogueTrigger aTrigger)
    {
        Debug.Log(aTrigger.gameObject.name + "'s Loop: " + aTrigger.Dialogue.GetLoop());
    }

    IEnumerator WaitForNextDialogue(DialogueScene scene)
    {
        if (scene.SentDuration.Length >= (scene.Sentences.Length - 1))
        {
            for (int i = 1; i < scene.Sentences.Length; i++) {
                yield return new WaitForSeconds(scene.SentDuration[i - 1]);
                ChangeText(scene.Sentences[i]);
            }
        } else {
            for (int i = 1; i < scene.Sentences.Length; i++) {
                yield return new WaitForSeconds(scene.SentDuration[0]);
                ChangeText(scene.Sentences[i]);
            }
        }

        yield return new WaitForSeconds(scene.SentDuration[scene.SentDuration.Length - 1]);
        ChangeText("");
    }

    IEnumerator WaitForNextImage(Cutscene scene)
    {
        if (scene.ImgDuration.Length >= (scene.Images.Length - 1))
        {
            for (int i = 1; i < scene.Images.Length; i++)
            {
                yield return new WaitForSeconds(scene.ImgDuration[i - 1]);
                ChangeImage(scene.Images[i]);
            }
        }
        else
        {
            for (int i = 1; i < scene.Images.Length; i++)
            {
                yield return new WaitForSeconds(scene.ImgDuration[0]);
                ChangeImage(scene.Images[i]);
            }
        }

        yield return new WaitForSeconds(scene.ImgDuration[scene.ImgDuration.Length - 1]);
        EndImage();
    }
}
