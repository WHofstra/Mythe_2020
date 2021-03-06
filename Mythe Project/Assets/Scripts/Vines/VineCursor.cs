﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineCursor : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;

    PlayerAttack player;

    float rotation;

    void Start()
    {
        player = FindObjectOfType<PlayerAttack>();

        if (player != null)
        {
            player = player.GetComponent<PlayerAttack>();
            player.ChangeCursorPosition += ChangePosition;
            player.TurnCursorOff += DisableVisibility;
        }

        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        rotation = transform.GetChild(0).rotation.eulerAngles.z;
        rotation -= (Input.GetAxis(Constants.InputString.MOUSE_X) * _rotationSpeed * Time.deltaTime);
        rotation += (_rotationSpeed * Time.deltaTime);

        transform.GetChild(0).rotation = Quaternion.Euler(transform.GetChild(0).rotation.eulerAngles.x,
            transform.GetChild(0).rotation.eulerAngles.y, rotation);
    }

    void ChangePosition(RaycastHit position)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.position = position.point;
        transform.up = position.normal;
    }

    void DisableVisibility()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
