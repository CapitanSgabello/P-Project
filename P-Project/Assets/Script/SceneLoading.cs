using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public Image progressBar;
    private void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    private IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gamelevel = SceneManager.LoadSceneAsync(2);
        while (gamelevel.progress<=1)
        {
            progressBar.fillAmount = gamelevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
