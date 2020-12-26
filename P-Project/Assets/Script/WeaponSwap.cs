using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public static WeaponSwap instance;

    public Animator meleeAnim;
    int meeleAmmo = 1;
    int meleeDmg = 2;
    float meleeRof;
    private bool meleeActive;

    public Animator handgunAnim;
    public int handgunAmmo;
    int handgunMaxAmmo = 150;
    int handgunDmg = 1;
    float handgunRof;
    private bool handgunActive;

    public Animator shotgunAnim;
    public int shotgunAmmo;
    int shotgunMaxAmmo = 20;
    int shotgunDmg = 3;
    float shotgunRof;
    private bool shotgunActive;

    public Animator machinegunAnim;
    public int machinegunAmmo;
    int machingunMaxAmmo = 100;
    int machinegunDmg = 1;
    float machinegunRof;
    private bool machinegunActive;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        meleeActive = false;
        handgunActive = false;
        handgunAmmo = 20;
        shotgunActive = true;
        shotgunAmmo = 10;
        machinegunActive = false;
        machinegunAmmo = 0;
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

    public void setMelee()
    {
        PlayerController.instance.setWeapon(meleeDmg, 10, meleeAnim);
    }

    public void setHandgun()
    {
        PlayerController.instance.setWeapon(handgunDmg, handgunAmmo, handgunAnim);
    }

    public void setShotgun()
    {
        PlayerController.instance.setWeapon(shotgunDmg, shotgunAmmo, shotgunAnim);
    }

    public void setMachinegun()
    {
        PlayerController.instance.setWeapon(machinegunDmg, machinegunAmmo, machinegunAnim);
    }
}
