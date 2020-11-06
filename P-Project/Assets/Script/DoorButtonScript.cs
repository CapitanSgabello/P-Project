using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonScript : MonoBehaviour
{
    public Transform doorModel1;
    public GameObject colliderB;                         //collider porta
    public ButtonScript button;
    public float openSpeed1;                             //velocità apertura porta

    private Boolean shouldOpen1;                        //booleano per l'apertura della porta

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shouldOpen1 = button.heldDown;


        if (shouldOpen1 && (doorModel1.position.y != 2f))
        {
            doorModel1.position = Vector3.MoveTowards(doorModel1.position, new Vector3(doorModel1.position.x, 2f, doorModel1.position.z), openSpeed1 * Time.deltaTime);

            if (doorModel1.position.y == 2f)
            {
                colliderB.SetActive(false);
            }
        }
        else if (!shouldOpen1 && (doorModel1.position.y != 1f))
        {
            doorModel1.position = Vector3.MoveTowards(doorModel1.position, new Vector3(doorModel1.position.x, 1f, doorModel1.position.z), openSpeed1 * Time.deltaTime);

            if (doorModel1.position.y == 1f)
            {
                colliderB.SetActive(true);
            }
        }


    }
}
