using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    // Start is called before the first frame update

    public void Setup()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }


    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }


}
