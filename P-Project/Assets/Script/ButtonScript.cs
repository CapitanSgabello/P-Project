using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Collider buttonCollider;               //collider del bottone
    public Boolean heldDown;                      //booleano per dire se il pulsante è stato premuto



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Funzione per capire se aprire la porta
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            heldDown = true;
        }
    }

    //Funzione per capire se chiudere la porta
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            heldDown = false;
        }
    }


}
