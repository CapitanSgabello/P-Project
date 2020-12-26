using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMachinegun : MonoBehaviour
{

    public int ammoAmount = 15;

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
            WeaponSwap.instance.machinegunAmmo += ammoAmount;

            AudioController.instance.PlayAmmoPickup();

            Destroy(gameObject);
        }
    }
}
