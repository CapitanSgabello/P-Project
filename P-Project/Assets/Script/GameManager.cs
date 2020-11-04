using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject settingsMenu;

    void Start()
    {
        settingsMenu.SetActive(false);
    }
}
