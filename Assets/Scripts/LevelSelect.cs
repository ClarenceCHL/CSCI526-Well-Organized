using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void StartLevel1()
    {
        SceneManager.LoadScene("level1");
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("level2");
    }

    public void StartLevel3()
    {
        SceneManager.LoadScene("level3");
    }

    public void StartLevel4()
    {
        SceneManager.LoadScene("level4");
    }

    public void StartLevel5()
    {
        SceneManager.LoadScene("level5");
    }

    public void StartLevel6()
    {
        SceneManager.LoadScene("level6");
    }

    public void ReturnButtom()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
