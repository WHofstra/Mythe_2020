
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] string _goToScene;
    [SerializeField] DialogueTrigger _waitTillThisEnds;

    void Start()
    {
        if (_waitTillThisEnds != null)
        {
            //Debug.Log(gameObject.name + ":" + _waitTillThisEnds.TotalDuration);
            StartCoroutine(WaitingCoroutine(_waitTillThisEnds.TotalDuration));
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag.Equals(Constants.ObjectName.PLAYER) &&
           col.gameObject.layer.Equals(Constants.Layer.PLAYER))
        {
            SceneManager.LoadScene(_goToScene);
        }
    }

    IEnumerator WaitingCoroutine(float secs)
    {
        yield return new WaitForSeconds(secs);
        SceneManager.LoadScene(_goToScene);
    }
}
