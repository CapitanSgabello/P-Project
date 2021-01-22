using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject warning;
    public GameObject confirm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    //Functions to change the login screen UI
    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
    }
    public void RegisterScreen() // Regester button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //AudioController.instance.PlayMenu();
    }

   /* public void ableDisableWarning()
    {
        if (warning.activeSelf)
        {
            confirm.SetActive(false);
        }
        else
        {
            confirm.SetActive(true);
        }
    }*/

    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}