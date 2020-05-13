using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehavior : MonoBehaviour
{
    Vector3 startPos;
    float timer = 0;
    void Start()
    {
        startPos = transform.position;
    }
    void GoUp()
    {
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(startPos, startPos + Vector3.up *5, timer);
    }
    public void AddToAction(RockThrow rock)
    {
        rock.lifts += GoUp;
    }
}
