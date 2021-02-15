using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivation : MonoBehaviour
{
    public GameObject enemies;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            enemies.SetActive(true);
    }
}
