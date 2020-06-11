
using UnityEngine;

public class Win : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
