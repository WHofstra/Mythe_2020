using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetEnemy : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    GameObject target;
    [SerializeField]
    Image ui;
    [SerializeField]
    Camera cam;
    void Update()
    {
        FindEnemys();
        if (target == null)
        {
            ui.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        else
        {
            Vector3 screenPoint = cam.WorldToViewportPoint(target.transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (!onScreen)
            {
                target = null;
            }
            Vector3 uiPos = cam.WorldToScreenPoint(target.transform.position);
            ui.transform.position = uiPos;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            ui.gameObject.transform.localScale = new Vector3(3 / distance, 3 / distance, ui.gameObject.transform.localScale.z);
        }
    }

    public void FindEnemys()
    {
        //target.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        float smallestDistance = 100;
        Collider[] targets = Physics.OverlapSphere(transform.position, 10, layer);
        if(targets.Length == 0) { 
            target = null;
            Debug.Log("null");
        }
        for (int i = 0; i < targets.Length; i++)
        {
            Vector3 screenPoint = cam.WorldToViewportPoint(targets[i].transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (Vector3.Distance(transform.position,targets[i].gameObject.transform.position) < smallestDistance  && onScreen)
            {
                smallestDistance = Vector3.Distance(transform.position, targets[i].gameObject.transform.position);
                target = targets[i].gameObject;
            }
        }
    }
    public GameObject GetTarget()
    {
        return target;
    }
}
