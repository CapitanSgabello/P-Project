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
    public Joystick joystickMove;

    /*Visual var*/
    public float sensibility = 0f;
    public Joystick joystickVisual;
    private Vector2 lookInput;
    private float cameraVertical;
    public float minAngleVisual = -10f;
    public float maxAngleVisual = 10f;

    private void Start()
    {

        
     }
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
        cameraVertical = Mathf.Clamp(lookInput.y, minAngleVisual, maxAngleVisual);
        cameraTransform.localRotation = Quaternion.Euler(cameraTransform.localRotation.eulerAngles.x, cameraTransform.localRotation.eulerAngles.y + cameraVertical, cameraTransform.localRotation.eulerAngles.z);

        // horizontal rotation
        transform.Rotate(transform.forward, -lookInput.x);
    }

    private void Move()
    {
        Vector2 movementDirection = new Vector2(joystickMove.Horizontal, joystickMove.Vertical)* moveSpeed* Time.deltaTime; 
        Vector3 moveHorizontal = transform.up* -movementDirection.x;
        Vector3 moveVertical = transform.right * movementDirection.y;
        characterController.Move(moveHorizontal + moveVertical) ;
    }

}
