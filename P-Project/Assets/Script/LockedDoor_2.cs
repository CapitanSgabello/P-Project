//SCRIPT DELLA PORTA CHE SI APRE QUANDO SI UCCIDE UN DETERMINATO NEMICO



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor_2 : MonoBehaviour
{
   
    public Transform doorModel;
    public GameObject doorCollider;
    public float openSpeed;
    public bool shouldOpen = false;
    private float posMax;
    private float posMin;
    public float elevatorMax;
    

    // Start is called before the first frame update
    void Start()
    {
        posMin = doorModel.position.y;
        posMax = posMin + elevatorMax;
    }

    // Update is called once per frame
    void Update()
    {
       
        

        if (shouldOpen && (doorModel.position.y != posMax))
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, posMax, doorModel.position.z), openSpeed * Time.deltaTime);
            if (doorModel.position.y == posMax)
            {
                doorCollider.SetActive(false);
            }
        }

    }
}