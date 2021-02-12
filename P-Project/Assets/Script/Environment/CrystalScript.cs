﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

public class CrystalScript : MonoBehaviour
{
    public int health = 2;
    public bool open = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            open = true;
            AudioController.instance.PlayCrystal();
        }
    }

    //Funzione che ritorna il booleano
    public bool GetBool()
    {
        return open;
    }
}