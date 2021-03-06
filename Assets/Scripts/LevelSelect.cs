using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelSelect : MonoBehaviour
{
    public void StartLevel1()
    {
        SceneManager.LoadScene("level1");
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0;
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("level2");
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0;
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene("level3");
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0;
    }

    public void StartLevel4()
    {
        SceneManager.LoadScene("level4");
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0;
    }

    public void StartLevel5()
    {
        SceneManager.LoadScene("level5");
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0;
    }

    public void StartLevel6()
    {
        SceneManager.LoadScene("level6");
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0;
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0;
    }
    
    public void StartTeachingLevel1()
    {
        SceneManager.LoadScene("teaching1");
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0;
    }
    
    public void StartTeachingLevel2()
    {
        SceneManager.LoadScene("teaching2");
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0;
    }

    public void BackBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
