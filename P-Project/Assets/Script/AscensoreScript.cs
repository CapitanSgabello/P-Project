using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensoreScript : MonoBehaviour
{
    public GameObject elevator;
    public bool stop;
    public bool startElevator;
    public float limiteInferiore;
    public float delay;

    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {

        stop = true;
        startElevator = false;
        limiteInferiore = elevator.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (startElevator)
        {
            delay -= Time.deltaTime;
            if (delay <= 0f)
            {
                elevator.transform.Translate((Vector3.up) * speed * Time.deltaTime);
                PlayerController.instance.isOnElevator(true);
            }
        }
        else
        {
            delay -= Time.deltaTime;
            if(delay <= 0 && !stop)
            {
                elevator.transform.Translate((-Vector3.up) * speed * Time.deltaTime);
                if (elevator.transform.position.y <= limiteInferiore)
                {
                    stop = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            delay = 0.5f;
            startElevator = true;
            stop = false;

            AudioController.instance.PlayElevator();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.isOnElevator(false);
            delay = 1.5f;
            startElevator = false;
        }
    }



}
