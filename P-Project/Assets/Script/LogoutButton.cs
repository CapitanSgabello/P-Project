using UnityEngine;
using Firebase;
using Firebase.Auth;

public class LogoutButton : MonoBehaviour
{
    public void logout()
    {
        FirebaseAuth.DefaultInstance.SignOut();
    }
}
