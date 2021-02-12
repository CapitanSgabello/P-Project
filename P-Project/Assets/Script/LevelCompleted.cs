using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{
    void Update()
    {

    }
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

    void OnDataSent(UpdateUserDataResult result)
    {

    }
    void onError(PlayFabError error)
    {

    }

    public void OnTriggerEnter(Collider levelCompleted)
    {
        saveLevelCompleted();
        StartCoroutine(ChangeToScene("Livello2"));
    }

    public IEnumerator ChangeToScene(string sceneToChangeTo)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
