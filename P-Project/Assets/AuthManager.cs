using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Collections;

public class AuthManager : MonoBehaviour
{
    //Login variables
    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;
    public Text warningLoginText;
    public Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public InputField emailRegisterField;
    public InputField usernameRegisterField;
    public InputField passwordRegisterField;
    public Text warningRegisterText;

    public Text showLoadedText;
    public GameObject L1;
    public GameObject L1locked;
    public GameObject L2locked;
    public GameObject L3locked;

    public void registerButton()
    {
        if(passwordRegisterField.text.Length < 6)
        {
            warningRegisterText.text = "Password too short!";
            return;
        }
        var request = new RegisterPlayFabUserRequest {
            Email = emailRegisterField.text,
            Password = passwordRegisterField.text,
            RequireBothUsernameAndEmail = false
    };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        UIManager.instance.MenuScreen();
    }

    private void OnError(PlayFabError error)
    {
        warningRegisterText.text = error.ErrorMessage;
    }
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        warningRegisterText.text = "Registered!";
        UIManager.instance.MenuScreen();
    }
    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailLoginField.text,
            Password = passwordLoginField.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        confirmLoginText.text = "Logged in!";
        getLevelCompleted();
        UIManager.instance.MenuScreen();
    }

    public void getLevelCompleted()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecived, OnError);
    }

    private void OnDataRecived(GetUserDataResult result)
    {
        if(result.Data!=null && result.Data.ContainsKey("level 1"))
        {
            L1locked.SetActive(false);
            L1.GetComponent<Button>().interactable = true;
        }
    }
}