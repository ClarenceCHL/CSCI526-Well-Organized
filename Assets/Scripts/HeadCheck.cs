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


    private void OnTriggerEnter2D(Collider2D other)
    {


<<<<<<< HEAD
        if (other.gameObject.layer == 7)
        {
            GameOver();
            Timer.instance.EndTimer();
=======
        if (other.gameObject.layer == 7 || other.gameObject.layer == 8)
        {
            GameOver();
            Timer.instance.EndTimer();
            Time.timeScale = 0;
>>>>>>> main
        }

    }
}
