using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CharacterController enemyController;             //character controller del nemico
    public GameObject player;

    public int health = 3;                                  //vita del nemico
    public float playerRange = 10f;
    public float moveSpeed;                                 //velocità del nemico
    public float speed;                                     //variabile per regolare la velocità del nemico
    public Animator enemyAnim;                              //animazione del nemico

    /* Variabili e costanti per la gravità */
    private Vector3 enemyFall;
    public float gravity = -800f;

    void Start()
    {

    }

    void Update()
    {
        enemyAnim.SetBool("Walk", false);                       //se il nemico è fermo bool walk = false e bool idle = true
        enemyAnim.SetBool("Idle", true);

        if (Vector3.Distance(transform.position, player.transform.position) < playerRange)
        {
            Vector3 playerDirection = player.transform.position - transform.position;
            enemyController.Move(playerDirection * Time.deltaTime * speed);

            enemyAnim.SetBool("Walk", true);                    //se il nemico comincia a camminare walk = true e idle = false
            enemyAnim.SetBool("Idle", false);
            
            /*Gravità*/
            enemyFall.y = gravity * Time.deltaTime;
            enemyController.Move(enemyFall * Time.deltaTime);
        }
        else
            player.GetComponent<PlayerController>().takeDamage(5);
    }

    public void takeDamage()
    {
        health--;
        if (health <= 0)
            Destroy(gameObject);
    }
}
