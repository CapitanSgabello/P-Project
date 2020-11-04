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

    /* Variabili e costanti per la gravità */
    private Vector3 playerFall;
    public float gravityValue;
    public float gravity = 800f;

    /* Variabili e costanti per gli ascensori */
    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask elevatorMask;
    public float elevatorSpeed;
    public bool isGrounded;
    public bool onElevator;

    private void Start()
    {
        gravityValue = gravity;
        elevatorSpeed = 170f;
    }
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        /* Impostazioni per l'Ascensore */
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, elevatorMask);

        if (isGrounded && onElevator)
        {
            gravityValue = elevatorSpeed;
        }
        else
        {
            gravityValue = gravity;
        }

        Move();
        LookAround();
    }

    private void LookAround()
    {
        lookInput = new Vector2(joystickVisual.Horizontal, joystickVisual.Vertical) * sensibility * Time.deltaTime;

        // vertical rotation
        cameraVertical = Mathf.Clamp(lookInput.y, minAngleVisual, maxAngleVisual);
        cameraTransform.localRotation = Quaternion.Euler(cameraTransform.localRotation.eulerAngles.x - cameraVertical, cameraTransform.localRotation.eulerAngles.y, cameraTransform.localRotation.eulerAngles.z);

        // horizontal rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    private void Move()
    {
        /*  Vecchi Comandi di movimento
        Vector2 movementDirection = new Vector2(joystickMove.Horizontal, joystickMove.Vertical)* moveSpeed* Time.deltaTime; 
        Vector3 moveHorizontal = transform.up* -movementDirection.x;
        Vector3 moveVertical = transform.right * movementDirection.y;
        characterController.Move(moveHorizontal + moveVertical) ;
        */

        /* Movimenti Del Giocatore */
        Vector3 moveInput = new Vector3(joystickMove.Horizontal, 0f, joystickMove.Vertical);
        Vector3 moveHorizontal = transform.forward * moveInput.z;
        Vector3 moveVertical = transform.right * moveInput.x;
        Vector3 move = moveHorizontal + moveVertical;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        playerFall.y = gravityValue * Time.deltaTime;

        characterController.Move(playerFall * Time.deltaTime);
    }

    /* Viene richiamata dall'oggetto ascensore che informa il giocatore di essere su un ascensore */
    public void isOnElevator(bool condition)
    {
        onElevator = condition;
    }

}
