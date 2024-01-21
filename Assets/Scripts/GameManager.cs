using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RetryBtn()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
