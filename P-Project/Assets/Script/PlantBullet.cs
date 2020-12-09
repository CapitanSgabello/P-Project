using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{

    public int damageAmount = 3;                       

    public float bulletSpeed = 3f;                   

    public CharacterController bulletController;                      

    private Vector3 direction;                     

    // Start is called before the first frame update
    void Start()
    {
        bulletController.enabled = true;
        direction = PlayerController.instance.transform.position - transform.position;
        direction = direction * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
         bulletController.Move(direction * bulletSpeed * Time.deltaTime);
        
    }

    //Funzione che identifica il danno del proiettile
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.TakeDamage(damageAmount);

            Destroy(gameObject);                                         
        }
    }
}
