using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIArrow : MonoBehaviour
{
    Vector3 offset;

    float rotationSpeed;
    float hor;

    void Start()
    {
        //hor = Input.GetAxis(Constants.InputString.MOUSE_X);
        //rotationSpeed = 5.0f;
        //offset = new Vector3(0, 0, 0);
    }

    void Update()
    {
        //offset = Quaternion.AngleAxis(hor * rotationSpeed, Vector3.up) * offset;
    }
}
