using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoScript : MonoBehaviour
{
    public Text score;
    public float ammo;
    void Start()
    {
        score = GetComponent<Text>();
        ammo = PlayerController.instance.currentHealth;
    }

    void Update()
    {
        ammo = PlayerController.instance.currentAmmo;
        score.text = "" + ammo;
    }
}
