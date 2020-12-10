using System.Collections;
using System.Collections.Generic;
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
    public Animator anim;

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
    public float gravity = -400f;
    public float gravityActivation = 5f;

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
    public int damageAmount;                        //danno arma

    public Transform hitPoint;

    private void Start()
    {
        currentHealth = maxHealth;
        gravityValue = 0;
        elevatorSpeed = 170f;
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
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
        sensibility += 50;
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

        if (moveInput != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

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
                
                if (hit.transform.tag == "Zoomba")
                {
                    
                    hit.transform.GetComponent<ZoombaScript>().takeDamage(damageAmount);
                    
                }
                if (hit.transform.tag == "Armabrillo")
                {

                    hit.transform.GetComponent<ArmabrilloScript>().takeDamage(damageAmount);

                }
                if (hit.transform.tag == "EnemyKey")
                {
                    hit.transform.GetComponent<EnemyKey>().takeDamage(damageAmount);
                }

                if (hit.transform.tag == "Crystal")
                {
                    
                    hit.transform.GetComponent<CrystalScript>().TakeDamage(damageAmount);
                }
                if(hit.transform.tag == "Plant")
                {
                    hit.transform.GetComponent<PlantScript>().TakeDamage(damageAmount);
                }

                /*AudioController.instance.PlayGunShot();*/
            }
            currentAmmo--;
            gunAnim.SetTrigger("Shoot");
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount ;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            deadScreen.SetActive(true);
            hasDied = true;
        }

        /*AudioController.instance.PlayPlayerHurt();*/
    }

    public void addHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
