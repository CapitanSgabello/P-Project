using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Trap : MonoBehaviour
{

    // public Collider areoOfDamage;
    private int dmg_amount = 1;
    private float rateOfDmg = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rateOfDmg > 0)
        {
            rateOfDmg -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && rateOfDmg <= 0)
        {
            PlayerController.instance.TakeDamage(dmg_amount);
            rateOfDmg = 0.15f;
        }
    }
}
