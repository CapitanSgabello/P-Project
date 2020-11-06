using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CharacterController characterController;


    public int health;                                     //vita del nemico
    public float PlayerRange = 10f;
    public float moveSpeed;                               //velocità del nemico
    public float moveCalibrate;                           //variabile per calibrare la velocità del nemico
    public Animator moveAnim;                             //animazione per il movimento
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveAnim.SetBool("Idle", true);
        moveAnim.SetBool("Walk", false);
        
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < PlayerRange)
        {
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
            characterController.Move(playerDirection * Time.deltaTime * moveCalibrate);
            
            moveAnim.SetBool("Idle", false);
            moveAnim.SetBool("Walk", true);
        }
    }
}
