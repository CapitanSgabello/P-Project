using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    [Header("Assegnazioni")]
    public CharacterController enemyController;                 //controller del nemico
    public GameObject death;                                    //Animazione morte del nemico
    public Animator enemyAnim;                                  //Animatore del nemico
    public CapsuleCollider other;                               //Collider giocatore
    public GameObject bullet;                                   //Proiettile del nemico
    public Transform firePoint;                                 //Punto di sparo del nemico

    [Header("Variabili Gravità")]
    private Vector3 enemyFall;                                 //Vettore caduta del nemico
    public float gravity;                                      //Gravità

    [Header("Costanti")]
    public int health = 3;                                     //Vita del nemico
    public float playerRange = 10f;                            //Range di attivazione del nemico
    public float moveSpeed;                                    //Velocità del nemico
    public float speed;                                        //Decrementatore velocità del nemico   
    public int damageAmount;                                   //Danno del nemico
    public float fireRate = .5f;                             //Rateo del nemico
    private float damageCounter;                               //Contatore sparo del nemico

    [Header("Booleani")]
    public bool shouldShoot;                                   //Nemico che spara
    public bool shouldHit;                                     //Nemico che colpisce
    public bool normal;                                        //Nemico normale
    public bool stop;
    // Start is called before the first frame update
    void Start()
    {
        gravity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        gravity = -400f;
        if ((shouldHit)&&!(stop))
        {
            enemyAnim.ResetTrigger("Walk");
            enemyAnim.SetTrigger("Idle");
            enemyAnim.ResetTrigger("Hit");

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
            {
                Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
                enemyController.Move(playerDirection * Time.deltaTime * speed);

                enemyAnim.SetTrigger("Walk");
                enemyAnim.ResetTrigger("Idle");
                enemyAnim.ResetTrigger("Hit");

                /*Gravità*/
                enemyFall.y = gravity * Time.deltaTime;
                enemyController.Move(enemyFall * Time.deltaTime);

            }

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < .7f)
            {
                OnTriggerStay(other);
            }
        }else if ((shouldShoot)&&!(stop))
        {
            enemyAnim.SetTrigger("Idle");
            enemyAnim.ResetTrigger("Attack");

            damageCounter -= Time.deltaTime;

            if ((Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange) && (damageCounter <= 0))
            {
                enemyAnim.ResetTrigger("Idle");
                enemyAnim.SetTrigger("Attack");
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                damageCounter = fireRate;

                /*Gravità*/
                enemyFall.y = gravity * Time.deltaTime;
                enemyController.Move(enemyFall * Time.deltaTime);

            }
        }else if ((normal)&& !(stop))
        {
            enemyAnim.ResetTrigger("Walk");
            enemyAnim.SetTrigger("Idle");


            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
            {
                Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
                enemyController.Move(playerDirection * Time.deltaTime * speed);

                enemyAnim.SetTrigger("Walk");
                enemyAnim.ResetTrigger("Idle");


                /*Gravità*/
                enemyFall.y = gravity * Time.deltaTime;
                enemyController.Move(enemyFall * Time.deltaTime);
            }
        }
        
    }//Fine update

    //Funzione danno nemico
    public void takeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {

            Instantiate(death, transform.position, transform.rotation);
            Destroy(gameObject);

            AudioController.instance.PlayEnemyDeath();
        }
    }

    //Funzione danno nemici 
    public void OnTriggerStay(Collider other)
    {
        if (normal)
        {
            damageCounter -= Time.deltaTime;
            if (damageCounter <= 0)
            {
                if (other.tag == "Player")
                {
                    enemyAnim.ResetTrigger("Walk");
                    enemyAnim.ResetTrigger("Idle");


                    PlayerController.instance.TakeDamage(damageAmount);
                    if (PlayerController.instance.hasDied)
                    {
                        stop = true;
                    }
                }
                damageCounter = fireRate;
            }
        }
        else if (shouldHit)
        {
            damageCounter -= Time.deltaTime;
            if (damageCounter <= 0)
            {
                if (other.tag == "Player")
                {
                    enemyAnim.ResetTrigger("Walk");
                    enemyAnim.ResetTrigger("Idle");
                    enemyAnim.SetTrigger("Hit");

                    PlayerController.instance.TakeDamage(damageAmount);
                    if (PlayerController.instance.hasDied)
                    {
                        stop = true;
                    }
                }
                damageCounter = fireRate;
            }
        }
    }
}











