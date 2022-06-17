using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public void Setup()
    {
        gameObject.SetActive(true);
      //  pointsText.text = score.ToString() + " POINTS";
    }

    public void RestartButton()
    {
        //SceneManager.LoadScene("SampleScene");

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        

        
        Time.timeScale = 1.0f;
        GlobalVariables.P1MatchCount = 0; 
    }

}
