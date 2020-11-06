using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform doorModel;                          
    public GameObject colliderP;                         //collider porta

    public float openSpeed;                             //velocità apertura porta

    private Boolean shouldOpen;                        //booleano per l'apertura della porta

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldOpen && (doorModel.position.y != 2f))
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, 2f, doorModel.position.z), openSpeed* Time.deltaTime);

            if(doorModel.position.y == 2f)
            {
                colliderP.SetActive(false);
            }
        }else if(!shouldOpen && (doorModel.position.y != 1f))
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, 1f, doorModel.position.z), openSpeed * Time.deltaTime);

            if (doorModel.position.y == 1f)
            {
                colliderP.SetActive(true);
            }
        }


    }

    //Funzione per l'apertura della porta
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            shouldOpen = true;
        }
    }

    //Funzione per la chiusura della porta
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shouldOpen = false;
        }
    }
}
