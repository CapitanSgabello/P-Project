using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class DoorScript : MonoBehaviour
    {
        public Transform doorModel;
        public GameObject doorCollider;
        public float openSpeed;
        private bool shouldOpen;
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
            if (shouldOpen && (doorModel.position.y != posMax) )
            {
                doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, posMax, doorModel.position.z), openSpeed * Time.deltaTime);
                if (doorModel.position.y == posMax)
                {
                    doorCollider.SetActive(false);
                }
            }
            else if (!shouldOpen && (doorModel.position.y != posMin))
            {
                doorModel.position = Vector3.MoveTowards(doorModel.position, new Vector3(doorModel.position.x, posMin, doorModel.position.z), openSpeed * Time.deltaTime);
                if (doorModel.position.y == posMin)
                {
                    doorCollider.SetActive(true);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                shouldOpen = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                shouldOpen = false;
            }
        }
}

