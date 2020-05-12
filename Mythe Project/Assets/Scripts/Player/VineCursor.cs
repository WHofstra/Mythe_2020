using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineCursor : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;

    SpriteRenderer renderer;
    PlayerAttack player;

    float rotation;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerAttack>();

        if (player != null)
        {
            player = player.GetComponent<PlayerAttack>();
            player.ChangeCursorPosition += ChangePosition;
            player.TurnCursorOff += DisableVisibility;
        }

        renderer.enabled = false;
    }

    void Update()
    {
        rotation = transform.rotation.eulerAngles.z;
        rotation += (_rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y, rotation);
    }

    void ChangePosition(Vector3 position)
    {
        renderer.enabled = true;
        transform.position = position;
    }

    void DisableVisibility()
    {
        renderer.enabled = false;
    }
}
