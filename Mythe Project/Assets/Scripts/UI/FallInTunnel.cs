using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallInTunnel : MonoBehaviour
{
    [SerializeField]
    GameObject TunnelVision;
    // Start is called before the first frame update
    void Start()
    {
        TunnelVision.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.tag);
        if(col.tag == "Player")
        {
            TunnelVision.SetActive(true);
            Application.LoadLevel("UnderworldScene1");
        }
    }
}
