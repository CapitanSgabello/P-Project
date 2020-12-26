using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoHandgun : MonoBehaviour
{

    public int ammoAmount = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WeaponSwap.instance.handgunAmmo += ammoAmount;

            AudioController.instance.PlayAmmoPickup();

            Destroy(gameObject);
        }
    }
}
