using System.Collections;
using System.Collections.Generic;
using System.Runtime.Hosting;
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
