using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager instance;
    private static float WAIT_TIME = 6f;
    public float waitTime = WAIT_TIME;
    
    public GameObject[] popUps;

    private int popUpIndex;

    // objs in POP-UP 1
    public GameObject[] arrows;
    private bool left;
    private bool right;

    private bool up;

    // objs in POP-UP 2
    public GameObject pushBox;
    public GameObject position;
    public GameObject[] sampleBoxes;

    //objs in POP-UP 7
    public GameObject button;
    
    

    void Start()
    {
        instance = GameManager.Instance;
        instance.spawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            //ask the player try basic moving method
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                left = true;
                arrows[0].SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                right = true;
                arrows[1].SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                up = true;
                arrows[2].SetActive(false);
            }

            if (left && right && up)
            {
                popUpIndex++;

            }
        }
        else if (popUpIndex == 1)
        {
            // pushing boxes to eliminate them
            for (int i = 0; i < sampleBoxes.Length; i++)
            {
                if (!sampleBoxes[i] || sampleBoxes[i].IsDestroyed())
                {
                    popUpIndex++;
                    break;
                }
                
                sampleBoxes[i].SetActive(true);
            }

            pushBox.SetActive(true);
            position.SetActive(true);
            
        }
        else if (popUpIndex == 2)
        {
            // pure text explain how to get box elimination
            pushBox.SetActive(false);
            position.SetActive(false);
            
            if (waitTime <= 0)
            {
                waitTime = WAIT_TIME;
                popUpIndex++;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        } 
        else if (popUpIndex == 3)
        {
            // pure text explain the purpose of box elimination
            if (waitTime <= 0)
            {
                waitTime = WAIT_TIME;
                popUpIndex++;
                
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        } 
        else if (popUpIndex == 4)
        {
            // ask the player to avoid boxes and get one more elimination
            instance.spawn = true;
            if (instance.player2BombGained >= 1)
            {
                popUpIndex++;
            }
        } 
        else if (popUpIndex == 5)
        {
            // ask the player to throw the box
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                popUpIndex++;
                instance.spawn = false;
            }
        }
        else if (popUpIndex == 6)
        {
            // end of this tutorial, display of next button
            button.SetActive(true);
        }
    }
}
