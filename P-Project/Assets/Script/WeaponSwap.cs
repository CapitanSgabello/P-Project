using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public static WeaponSwap instance;

    public Animator meleeAnim;
    int meeleAmmo = 1;
    int meleeDmg = 2;
    float meleeRof = 0.3f;
    public bool meleeActive;
    
    

    public Animator handgunAnim;
    public int handgunAmmo;
    int handgunMaxAmmo = 150;
    int handgunDmg = 1;
    float handgunRof = 0.2f;
    private bool handgunActive;

    public Animator shotgunAnim;
    public int shotgunAmmo;
    int shotgunMaxAmmo = 20;
    int shotgunDmg = 3;
    float shotgunRof = 0.5f;
    private bool shotgunActive;

    public Animator machinegunAnim;
    public int machinegunAmmo;
    int machingunMaxAmmo = 100;
    int machinegunDmg = 1;
    float machinegunRof = 0.05f;
    private bool machinegunActive;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        meleeActive = false;
        handgunActive = true;
        handgunAmmo = 20;
        shotgunActive = false;
        shotgunAmmo = 10;
        machinegunActive = false;
        machinegunAmmo = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (meleeActive)
        {
            setMelee();
        }
        else if (handgunActive)
        {
            setHandgun();
        }
        else if (shotgunActive)
        {
            setShotgun();
        }
        else if (machinegunActive)
        {
            setMachinegun();
        }
    }

    private void setMelee()
    {
        PlayerController.instance.setWeapon(meleeDmg, meeleAmmo, meleeRof, meleeAnim);
    }

    private void setHandgun()
    {
        PlayerController.instance.setWeapon(handgunDmg, handgunAmmo, handgunRof, handgunAnim);
    }

    private void setShotgun()
    {
        PlayerController.instance.setWeapon(shotgunDmg, shotgunAmmo, shotgunRof, shotgunAnim);
    }

    private void setMachinegun()
    {
        PlayerController.instance.setWeapon(machinegunDmg, machinegunAmmo, machinegunRof, machinegunAnim);
    }

    public void ammoReduction()
    {
        if (handgunActive)
        {
            handgunAmmo--;
        }
        else if (shotgunActive)
        {
            shotgunAmmo--;
        }
        else if (machinegunActive)
        {
            machinegunAmmo--;
        }
    }

    public void meleeActivation()
    {
        meleeActive = true;
        handgunActive = false;
        shotgunActive = false;
        machinegunActive = false;
    }

    public void handgunActivation()
    {
        meleeActive = false;
        handgunActive = true;
        shotgunActive = false;
        machinegunActive = false;
    }

    public void shotgunActivation()
    {
        meleeActive = false;
        handgunActive = false;
        shotgunActive = true;
        machinegunActive = false;
    }

    public void machinegunActivation()
    {
        meleeActive = false;
        handgunActive = false;
        shotgunActive = false;
        machinegunActive = true;
    }
}
