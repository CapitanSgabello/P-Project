using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour

{
    public static AudioController instance;

    public AudioSource ammo, enemyDeath, enemyShot, gunShot, health, playerHurt, elevator, crystal, key, menu, deathScreen;

    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAmmoPickup()
    {
        ammo.Stop();
        ammo.Play();
    }

    public void PlayEnemyDeath()
    {
        enemyDeath.Stop();
        enemyDeath.Play();
    }

    public void PlayEnemyShot()
    {
        enemyShot.Stop();
        enemyShot.Play();
    }
   
    public void PlayGunShot()
    {
        gunShot.Stop();
        gunShot.Play();
    }

    public void PlayHealthPickup()
    {
        health.Stop();
        health.Play();
    }

    public void PlayPlayerHurt()
    {
        playerHurt.Stop();
        playerHurt.Play();
    }

    public void PlayElevator()
    {
        elevator.Stop();
        elevator.Play();
    }

    public void PlayCrystal()
    {
        crystal.Stop();
        crystal.Play();
    }


    public void PlayKeyPickup()
    {
        key.Stop();
        key.Play();
    }

    public void PlayMenu()
    {
        menu.Stop();
        menu.Play();
    }

    public void PlayDeathScreen()
    {
        deathScreen.Stop();
        deathScreen.Play();
    }


}
