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
    PlayerAnimator anim;
    Transform child;

    Vector3 dir;
    Vector3 rotation;
    Vector3 camRotation;

    float speed;
    float currentSpeed;
    bool arrowKeysPressed;
    bool onPlatform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<Collider>();
        anim = GetComponent<PlayerAnimator>();
        child = transform.GetChild(0).transform;

        speed = _defualtSpeed;
        currentSpeed = speed;
        arrowKeysPressed = false;
        onPlatform = false;
        //Screen.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        GetRayNormal(8);
    }

    void FixedUpdate()
    {
        InputKeysAndMouse();
        GradualMovement();
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
            currentSpeed = _defualtSpeed;
        }

        //Standard Arrow Keys and A-, W-, S- and D Keys
        dir = new Vector3(Input.GetAxis(Constants.InputString.HORIZONTAL), 0, Input.GetAxis(Constants.InputString.VERTICAL));
        dir.Normalize();

        if (arrowKeysPressed)
        {
            transform.position += transform.forward * dir.z * currentSpeed * Time.deltaTime;
            transform.position += transform.right * dir.x * currentSpeed * Time.deltaTime;
        }

        if (anim != null)
        {
            if (Input.GetAxis(Constants.InputString.RUN) != 0 && onPlatform && arrowKeysPressed) {
                anim.SetBoolTo(Constants.AnimatorTriggerString.RUNNING, true);
            }
            else {
                anim.SetBoolTo(Constants.AnimatorTriggerString.RUNNING, false);
            }
        }

        //Mouse Rotation
        rotation = transform.eulerAngles;
        rotation.y += Input.GetAxis(Constants.InputString.MOUSE_X) * _mouseSpeed * Time.deltaTime;
        transform.eulerAngles = rotation;

        //Make Sure the Camera is the Gameobject's First Child
        camRotation = child.eulerAngles;

        //Camera Rotation Boundaries
        camRotation.x -= Input.GetAxis(Constants.InputString.MOUSE_Y) * _mouseSpeed * Time.deltaTime;
        if (camRotation.x > _lookingAngle && camRotation.x < (360 - _lookingAngle)) {
            camRotation.x += Input.GetAxis(Constants.InputString.MOUSE_Y) * _mouseSpeed * Time.deltaTime * 1.5f;
        }
        child.eulerAngles = camRotation;

        //Spacebar
        if (Input.GetKeyDown(KeyCode.Space) && onPlatform)
        {
            rb.AddForce(new Vector3(0, _jumpingHeight * 50, 0));
            onPlatform = false; //To Prevent an Extra Leap in 1 Frame
        }

        //Left Shift Key or Right Shift Key
        speed = _defualtSpeed + (Input.GetAxis(Constants.InputString.RUN) * 5);
    }

    void GradualMovement()
    {
        if (speed > currentSpeed)
        {
            currentSpeed += Time.deltaTime*6;
        }
        if (speed < currentSpeed)
        {
            currentSpeed -= Time.deltaTime *10;
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

        if (((hit.distance - 0.2f) <= (boxCollider.bounds.size.y / 2)) && (hit.distance > 0)) {
            onPlatform = true;
            //Debug.Log("On ground"); //To Check if the Player Collides with the Ground Below
        } else {
            onPlatform = false;
            //Debug.Log("In air");
        }
    }
}
