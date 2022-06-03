using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCheck : MonoBehaviour
{

    public GameOverScreen GameOverScreen;

    public void GameOver()
    {
        GameOverScreen.Setup(); //score
       // enableInput = false;
    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.layer == 9)
        {
            GameOver();
            Timer.instance.EndTimer();
        }

    }
}
