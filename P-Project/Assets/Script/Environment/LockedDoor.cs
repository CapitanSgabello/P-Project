using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public Transform doorModel;
    public GameObject doorCollider;
    public float openSpeed;
    private bool shouldOpen;
    private float posMax;
    private float posMin;
    public float elevatorMax;
    public GameObject crystal;

    // Start is called before the first frame update
    void Start()
    {
        posMin = doorModel.position.y;
        posMax = posMin + elevatorMax;
    }

    // Update is called once per frame
    void Update()
    {
        
        shouldOpen = crystal.GetComponent<CrystalScript>().GetBool(); 

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
