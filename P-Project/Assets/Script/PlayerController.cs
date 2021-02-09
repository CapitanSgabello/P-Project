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
    private Animator gunAnim;
    public int currentAmmo;
    private int weaponDamage;
    private float weaponRoF;
    private float rateOfFire;

    public int currentHealth;
    public int maxHealth = 100;
    public bool hasDied;

    public Transform hitPoint;

    public AudioSource background;
    
    private void Start()
    {
        currentHealth = maxHealth;
        gravityValue = 0;
        elevatorSpeed = 170f;
        weaponRoF = 0;
        rateOfFire = 0;
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

        rateOfFire -= Time.deltaTime;
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
        if (!hasDied)
        {   

            if (currentAmmo > 0 && rateOfFire <= 0)
            {
                Ray ray = viewCam.ViewportPointToRay(new Vector3(.5f, .5f, .0f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (!WeaponSwap.instance.meleeActive) 
                    {
                        Instantiate(bulletImpact, hit.point, transform.rotation);
                        
                    }
                    
                    if (hit.transform.tag == "EnemyKey")
                    {
                        hit.transform.GetComponent<EnemyKey>().takeDamage(weaponDamage);
                    }

                    if (hit.transform.tag == "Crystal")
                    {

                        hit.transform.GetComponent<CrystalScript>().TakeDamage(weaponDamage);
                    }
                    
                    if (hit.transform.tag == "Enemy")
                    {
                        hit.transform.GetComponent<EnemyController>().takeDamage(weaponDamage);
                    }

                    if  (WeaponSwap.instance.meleeActive)
                    AudioController.instance.PlayAxeHit();

                    else
                        AudioController.instance.PlayGunShot();


                }
                WeaponSwap.instance.ammoReduction();
                gunAnim.SetTrigger("Shoot");
                rateOfFire = weaponRoF;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount ;

        if (currentHealth <= 0)
        {
            //currentHealth = 0;
            deadScreen.SetActive(true);
            hasDied = true;
            
        }
        if(currentHealth == 0)
        {
            AudioController.instance.PlayPlayerHurt();
            AudioController.instance.PlayDeathScreen();
            background.Stop();
           
        }
            }

    public void addHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void setWeapon(int dmg, int ammo, float RoF, Animator anim)
    {
        weaponDamage = dmg;
        currentAmmo = ammo;
        gunAnim = anim;
        weaponRoF = RoF;
    }
}
