using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithButtonScript : MonoBehaviour
{
    public Transform doorModel;
    public GameObject doorCollider;
    public float openSpeed;
    private bool shouldOpen;
    public ButtonScript button;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shouldOpen = button.heldDown;

        if (shouldOpen && (doorModel.position.y != 2f))
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, 2f, doorModel.position.z), openSpeed * Time.deltaTime);
            if (doorModel.position.y == 2f)
            {
                doorCollider.SetActive(false);
            }
        }
        else if (!shouldOpen && (doorModel.position.y != 1f))
        {
            doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, 1f, doorModel.position.z), openSpeed * Time.deltaTime);
            if (doorModel.position.y == 1f)
            {
                doorCollider.SetActive(true);
            }
        }
    }

}
