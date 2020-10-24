﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 30f;
    private Vector2 moveInput;
    public Joystick joystick;

    private void FixedUpdate()
    {
        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        Vector3 moveHorizontal = transform.up * -moveInput.x;
        Vector3 moveVertical = transform.right * moveInput.y;
        rb.velocity = (moveHorizontal+moveVertical) * speed;
    }
}
