using UnityEngine;
using Firebase;
using Firebase.Database;

public class SavingData : MonoBehaviour
{
    void Start()
    {
        // Get the root reference location of the database.
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
}
