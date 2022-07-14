using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Image pauseScreen;

    public void ReturnButtom()
    {
        SceneManager.LoadScene("LevelSelect");
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0;
        GlobalVariables.P2MatchCount = 0;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name);
    }

    public void PauseBtn()
    {
        Time.timeScale = 0.0f;
        pauseScreen.gameObject.SetActive(true); 
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseScreen.gameObject.SetActive(false);
    }

  
}
