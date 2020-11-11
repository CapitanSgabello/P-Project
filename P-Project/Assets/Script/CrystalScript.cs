using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    public int health = 2;                        //durabilità del cristallo
    public bool open = false;                    //apertura della porta

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Funzione per prendere danno
    public void TakeDamage()
    {
        health--;
        if(health <= 0)
        {
            open = true;
        }
    }
}
