﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CharacterController enemyController;             //character controller del nemico

    public int health = 3;                                  //vita del nemico
    public float playerRange = 10f;
    public float moveSpeed;                                 //velocità del nemico
    public float speed;                                     //variabile per regolare la velocità del nemico
    public Animator enemyAnim;                              //animazione del nemico

    /* Variabili e costanti per la gravità */
    private Vector3 playerFall;
    public float gravity = -800f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyAnim.SetBool("Walk", false);                       //se il nemico è fermo bool walk = false e bool idle = true
        enemyAnim.SetBool("Idle", true);



        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
        {
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
            enemyController.Move(playerDirection * Time.deltaTime * speed);

            enemyAnim.SetBool("Walk", true);                    //se il nemico comincia a camminare walk = true e idle = false
            enemyAnim.SetBool("Idle", false);
            
            /*Gravità*/
            playerFall.y = gravity * Time.deltaTime;
            enemyController.Move(playerFall * Time.deltaTime);
        }
    }
}