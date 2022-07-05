using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public void ReturnButtom()
    {
        Analytics.instance.updateLevel(SceneManager.GetActiveScene().name);
        
        SceneManager.LoadScene("LevelSelect");
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0;
        GlobalVariables.P2MatchCount = 0;
    }
}
