using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _defualtSpeed;
    [SerializeField] float _mouseSpeed;
    [SerializeField] float _jumpingHeight;
    [SerializeField] float _lookingAngle;

    Rigidbody rb;
    Collider boxCollider;

    Vector3 dir;
    Vector3 rotation;
    Vector3 camRotation;

    float speed;
    bool arrowKeysPressed;
    bool onPlatform;
    bool negativeRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<Collider>();

        speed = _defualtSpeed;
        arrowKeysPressed = false;
        onPlatform = false;
        negativeRotation = false;
    }

    void Update()
    {
        GetRayNormal(8);
    }

    void FixedUpdate()
    {
        InputKeysAndMouse();
    }

    void InputKeysAndMouse()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
            Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            arrowKeysPressed = true;
        } else {
            arrowKeysPressed = false;
        }

        //Standard Arrow Keys and A-, W-, S- and D Keys
        dir = new Vector3(Input.GetAxis(Constants.InputString.HORIZONTAL), 0, Input.GetAxis(Constants.InputString.VERTICAL));
        dir.Normalize();

        if (arrowKeysPressed)
        {
            transform.position += transform.forward * dir.z * speed * Time.deltaTime;
            transform.position += transform.right * dir.x * speed * Time.deltaTime;
        }

        //Mouse Rotation
        rotation = transform.eulerAngles;
        rotation.y += Input.GetAxis(Constants.InputString.MOUSE_X) * _mouseSpeed * Time.deltaTime;
        transform.eulerAngles = rotation;

        //Make Sure the Camera is the Gameobject's First Child
        camRotation = transform.GetChild(0).transform.eulerAngles;

        //Camera Rotation Boundaries
        camRotation.x -= Input.GetAxis(Constants.InputString.MOUSE_Y) * _mouseSpeed * Time.deltaTime;
        if (camRotation.x > _lookingAngle && camRotation.x < (360 - _lookingAngle)) {
            camRotation.x += Input.GetAxis(Constants.InputString.MOUSE_Y) * _mouseSpeed * Time.deltaTime * 1.5f;
        }
        transform.GetChild(0).transform.eulerAngles = camRotation;

        //Spacebar
        if (Input.GetKeyDown(KeyCode.Space) && onPlatform)
        {
            rb.AddForce(new Vector3(0, _jumpingHeight * 50, 0));
            onPlatform = false; //To Prevent an Extra Leap in 1 Frame
        }

        //Left Shift Key or Right Shift Key
        if (Input.GetAxis(Constants.InputString.VERTICAL) > 0 && Input.GetAxis(Constants.InputString.HORIZONTAL) == 0 && onPlatform)
        {
            speed = _defualtSpeed + (Input.GetAxis(Constants.InputString.RUN) * 10);
        } else {
            speed = _defualtSpeed;
        }
    }

    void GetRayNormal(int layer)
    {
        RaycastHit hit;
        int layerMask = 0 << layer;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 30000, layerMask)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.blue);
        }

        if ((hit.distance - 0.2f) <= (boxCollider.bounds.size.y / 2)) {
            onPlatform = true;
            //Debug.Log("On ground"); //To Check if the Player Collides with the Ground Below
        } else {
            onPlatform = false;
            //Debug.Log("In air");
        }
    }
}
