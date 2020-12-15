using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoombaScript : MonoBehaviour
{
    public CharacterController zoombaController;             //character controller del nemico
    public GameObject explosion;                            //animazione morte nemico
    public int health = 3;                                  //vita del nemico
    public float playerRange = 10f;
    public float moveSpeed;                                 //velocità del nemico
    public float speed;                                     //variabile per regolare la velocità del nemico
    public Animator enemyAnim;                              //animazione del nemico
    public int damageAmount;                                //danno del nemico
    public float damageRate = .5f;                         //tempo di sparo del nemico
    private float damageCounter;
    

    /* Variabili e costanti per la gravità */
    private Vector3 playerFall;
    public float gravity;

    // Start is called before the first frame update
    void Start()
    {
        gravity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        gravity = -400f;
        enemyAnim.ResetTrigger("Walk");
        enemyAnim.SetTrigger("Idle");


        if (!PlayerController.instance.paused)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
            {
                Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
                zoombaController.Move(playerDirection * Time.deltaTime * speed);

                enemyAnim.SetTrigger("Walk");
                enemyAnim.ResetTrigger("Idle");


                /*Gravità*/
                playerFall.y = gravity * Time.deltaTime;
                zoombaController.Move(playerFall * Time.deltaTime);
            }
        }
    }

    public void takeDamage(int damageAmount)
    {
        if (!PlayerController.instance.paused)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                Destroy(gameObject);
                Instantiate(explosion, transform.position, transform.rotation);

                AudioController.instance.PlayEnemyDeath();
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (!PlayerController.instance.paused)
        {
            damageCounter -= Time.deltaTime;                          //ogni quanto può sparare il nemico
            if (damageCounter <= 0)
            {
                if (other.tag == "Player")
                {
                    enemyAnim.ResetTrigger("Walk");
                    enemyAnim.ResetTrigger("Idle");


                    PlayerController.instance.TakeDamage(damageAmount);

                }
                damageCounter = damageRate;
            }
        }
    }
}