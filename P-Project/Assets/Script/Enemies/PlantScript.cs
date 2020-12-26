using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public CharacterController plantController;
    public int health;
    public Animator plantAnim;
    public float fireRate;
    //public GameObject deathAnim;
    public float range;
    private float damageCounter;
    public GameObject bullet;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        plantAnim.SetTrigger("Idle");
        plantAnim.ResetTrigger("Attack");

            damageCounter -= Time.deltaTime;

            if ((Vector3.Distance(transform.position, PlayerController.instance.transform.position) < range) && (damageCounter <= 0))
            {
                plantAnim.ResetTrigger("Idle");
                plantAnim.SetTrigger("Attack");
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                damageCounter = fireRate;

            }
    }

    public void TakeDamage(int damageAmount)
    {
            health -= damageAmount;
            if (health <= 0)
            {
                // Instantiate(deathAnim, transform.position, transform.rotation);
                Destroy(gameObject);

                AudioController.instance.PlayEnemyDeath();
            }   
    }
}
