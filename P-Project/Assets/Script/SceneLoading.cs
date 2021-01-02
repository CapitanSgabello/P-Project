using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public Image progressBar;
    public float time = 10f;
    public GameObject loading;
    public GameObject game;
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
    private void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            loading.SetActive(false);
            game.SetActive(true);
        }
    }
}
