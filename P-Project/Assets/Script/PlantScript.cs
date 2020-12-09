using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public CharacterController plantController;
    public GameObject explosion;
    public int health = 3;
    public float playerRange = 10f;
    public Animator plantAnim;
    public float fireRate= 5f;
    private float shootCounter;
    public bool shouldShoot;
    public GameObject bullet;                             
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        plantController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < playerRange)
        {
            if (shouldShoot)
            {
                shootCounter -= Time.deltaTime;                          //ogni quanto può sparare il nemico
                if (shootCounter <= 0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    shootCounter = fireRate;
                }
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);

        }
    }
}
