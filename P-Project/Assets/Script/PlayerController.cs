using Microsoft.SqlServer.Server;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*Variabili e costanti d'istanza*/
    public static PlayerController instance;
    public CharacterController characterController;
    public Camera viewCam;
    public GameObject deadScreen;

    /*Variabili e costanti per il movimento*/
    public float moveSpeed;
    public Joystick joystickMove;

    /*Variabili e costanti per la visuale*/
    public float sensibility;
    public Joystick joystickVisual;
    private Vector2 lookInput;
    private float cameraVertical;
    public float minAngleVisual = -10f;
    public float maxAngleVisual = 10f;

    /* Variabili e costanti per la gravità */
    private Vector3 playerFall;
    public float gravityValue;
    public float gravity = -800f;

    /* Variabili e costanti per gli ascensori */
    public Transform groundCheck;
    public float groundDistance = 1f;
    public LayerMask elevatorMask;
    public float elevatorSpeed;
    public bool isGrounded;
    public bool onElevator;

    /* Variabili e costanti per lo sparo*/
    public GameObject bulletImpact;
    public Animator gunAnim;
    public int currentAmmo;

    public int currentHealth;
    public int maxHealth = 100;
    private bool hasDied;

    public CrystalScript crystal;

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

        if (!hasDied)
        {
            Move();
            LookAround();
        }
    }

    private void LookAround()
    {
        lookInput = new Vector2(joystickVisual.Horizontal, joystickVisual.Vertical) * sensibility * Time.deltaTime;

        /*Rotazione verticale*/
        cameraVertical = Mathf.Clamp(lookInput.y, minAngleVisual, maxAngleVisual);
        viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles.x - cameraVertical, viewCam.transform.localRotation.eulerAngles.y, viewCam.transform.localRotation.eulerAngles.z);

        /*Rotazione orizzontale*/
        transform.Rotate(transform.up, lookInput.x);
    }
    public void modifySensibity()
    {
        sensibility += 15;
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

    public void Fire()
    {
        if (currentAmmo > 0)
        {
            Ray ray = viewCam.ViewportPointToRay(new Vector3(.5f, .5f, .0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(bulletImpact, hit.point, transform.rotation);

                if (hit.transform.CompareTag("Crystal"))
                {
                    crystal.TakeDamage();
                }
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<EnemyController>().takeDamage();
                }
            }
            currentAmmo--;
            gunAnim.SetTrigger("Shoot");
        }
    }

    public void takeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            deadScreen.SetActive(true);
            hasDied = true;
        }
    }

    public void addHealth(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
