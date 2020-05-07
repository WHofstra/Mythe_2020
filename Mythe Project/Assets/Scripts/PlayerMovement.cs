using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float _defualtSpeed;

    [SerializeField]
    float _mouseSpeed;

    Rigidbody rb;
    float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = _defualtSpeed;
    }

    void FixedUpdate()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir.Normalize();
            transform.position += transform.forward * dir.z * speed * Time.deltaTime;
            transform.position += transform.right * dir.x * speed * Time.deltaTime;
        Vector3 rotation = transform.eulerAngles;

        rotation.y += Input.GetAxis("Mouse X") * _mouseSpeed * Time.deltaTime; // Standart Left-/Right Arrows and A & D Keys

        transform.eulerAngles = rotation;
    }
}
