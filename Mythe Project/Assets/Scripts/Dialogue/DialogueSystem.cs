using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] DialogueTrigger[] _triggers;

    public event Action<Sprite> ChangeImage;
    public event Action<string> ChangeText;

    void Awake()
    {
        if (_triggers != null)
        {
            for (int i = 0; i < _triggers.Length; i++) {
                _triggers[i].Scene += TriggerScene;
            }
        }
    }

    void TriggerScene(DialogueScene aScene)
    {
        if (aScene.Sentences.Length > 0) {
            ChangeText(aScene.Sentences[0]);
            StartCoroutine(WaitForNextDialogue(aScene));
        }
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
}
