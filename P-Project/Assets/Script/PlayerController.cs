using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public CharacterController characterController;
    public Transform cameraTransform;

    /*Moves var*/
    public float moveSpeed = 0f;
    private Vector2 inputMove;
    public Joystick joystickMove;

    /*Visual var*/
    public float sensibility = 0f;
    private Vector2 inputVisual;
    public Joystick joystickVisual;
    private Vector2 lookInput;
    private float cameraVertical;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        Move();
        LookAround();
    }

    private void LookAround()
    {
        lookInput = new Vector2(joystickVisual.Horizontal, joystickVisual.Vertical) * sensibility * Time.deltaTime;

        // vertical rotation
        cameraVertical = Mathf.Clamp(cameraVertical - lookInput.y, -135f, 135f);
        cameraTransform.localRotation = Quaternion.Euler(cameraVertical,0 ,0);

        // horizontal rotation
        transform.Rotate(transform.forward, -lookInput.x);
    }

    private void Move()
    {
        Vector2 movementDirection = new Vector2(joystickMove.Horizontal, joystickMove.Vertical) * moveSpeed * Time.deltaTime; 
        characterController.Move(transform.up * -movementDirection.x + transform.right * movementDirection.y);
    }

}
