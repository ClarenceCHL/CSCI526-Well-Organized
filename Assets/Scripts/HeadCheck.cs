using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeadCheck : MonoBehaviour
{
    /*public void GameOver()
    {
        Timer.instance.EndTimer();
        Time.timeScale = 0;
        GameOverScreen.Setup(); //score
       // enableInput = false;
    }*/


    private void OnTriggerEnter2D(Collider2D other)
    {
        string name = this.GetComponentInParent<Transform>().GetComponentInParent<Transform>().name;
        int player = 0;
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            if (name == "HeadCheck1")
            {
                
                GameManager.Instance.lostHP(1);
                player = 1;
            }
            else if(name == "HeadCheck2")
            {
               
                GameManager.Instance.lostHP(2);
                player = 2;
            }
            
           //TODO: add hit by box analysis here
        }

        

    }
}
