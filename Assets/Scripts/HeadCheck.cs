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

        Debug.Log("Trigger");
        if (other.gameObject.layer == 7)
        {
            GameOver();
            Debug.Log("Dead");
        }

    }
}
