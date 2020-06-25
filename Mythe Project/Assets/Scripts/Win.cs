
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] string _goToScene;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag.Equals(Constants.ObjectName.PLAYER) &&
           col.gameObject.layer.Equals(Constants.Layer.PLAYER))
        {
            SceneManager.LoadScene(_goToScene);
        }
    }
}
