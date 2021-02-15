using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    public void saveLevelCompleted()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"level 1","completed"}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSent, onError);
    }

    public void saveLevelCompleted2()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"level 2","completed"}
            }
        };

        PlayFabClientAPI.UpdateUserData(request, OnDataSent, onError);
    }

    void OnDataSent(UpdateUserDataResult result)
    {

    }
    void onError(PlayFabError error)
    {

    }

    public void OnTriggerEnter(Collider levelCompleted)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello1"))
        {
            saveLevelCompleted();
            StartCoroutine(ChangeToScene("Livello2"));
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Livello2"))
        {
            saveLevelCompleted2();
            StartCoroutine(ChangeToScene("MAIN MENU"));
        }

    }

    public IEnumerator ChangeToScene(string sceneToChangeTo)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
