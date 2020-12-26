using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmabrilloScript : MonoBehaviour
{
    public CharacterController enemyController;             //character controller del nemico
    public GameObject explosion;                            //animazione morte nemico
    public int health = 3;                                  //vita del nemico
    public float playerRange = 10f;
    public float moveSpeed;                                 //velocità del nemico
    public float speed;                                     //variabile per regolare la velocità del nemico
    public Animator enemyAnim;                              //animazione del nemico
    public int damageAmount;                                //danno del nemico
    public float damageRate = .5f;                         //tempo di sparo del nemico
    private float damageCounter;
    public CapsuleCollider other;

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
        enemyAnim.ResetTrigger("Hit");

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
            {
                Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
                enemyController.Move(playerDirection * Time.deltaTime * speed);

                enemyAnim.SetTrigger("Walk");
                enemyAnim.ResetTrigger("Idle");
                enemyAnim.ResetTrigger("Hit");

                /*Gravità*/
                playerFall.y = gravity * Time.deltaTime;
                enemyController.Move(playerFall * Time.deltaTime);

            }

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < .7f)
            {
                OnTriggerStay(other);
            }
    }

    public void takeDamage(int damageAmount)
    {
            health -= damageAmount;
            if (health <= 0)
            {

                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);

                AudioController.instance.PlayEnemyDeath();
            }
    }

    public void OnTriggerStay(Collider other)
    {
            damageCounter -= Time.deltaTime;                          //ogni quanto può sparare il nemico
            if (damageCounter <= 0)
            {
                if (other.tag == "Player")
                {
                    enemyAnim.ResetTrigger("Walk");
                    enemyAnim.ResetTrigger("Idle");
                    enemyAnim.SetTrigger("Hit");

                    PlayerController.instance.TakeDamage(damageAmount);

                }
                damageCounter = damageRate;
            }
    }
}
