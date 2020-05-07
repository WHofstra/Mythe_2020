using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float defualtSpeed = 5f;
    float speed = 5;
    float mouseSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir.Normalize();
            transform.position += transform.forward * dir.z * speed * Time.deltaTime;
            transform.position += transform.right * dir.x * speed * Time.deltaTime;
        Vector3 rotation = transform.eulerAngles;

        rotation.y += Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime; // Standart Left-/Right Arrows and A & D Keys

        transform.eulerAngles = rotation;
    }
}
