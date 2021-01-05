using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManageer : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void setMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
