using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    public Rigidbody bulletRB;
    public int damageAmount;
    public float speed;
    private Vector3 direction;
    public float timetoLive;

    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.hitPoint.position - transform.position;
        direction = direction * speed;
        AudioController.instance.PlayEnemyShot();
    }

    // Update is called once per frame
    void Update()
    {
        bulletRB.velocity = direction * Time.deltaTime;
        timetoLive -= Time.deltaTime;
        if(timetoLive <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.TakeDamage(damageAmount);
        }
        
        Destroy(gameObject);
    }
}
