using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager instance;
    private static float WAIT_TIME = 1f;
    private float waitTime = WAIT_TIME;

    public GameObject instructions;
    public GameObject instructionButton;
    
    public GameObject mechanics;
    //public GameObject prevButton;
    public GameObject mechanicButton;

    private int popUpIndex = -1;

    // objs in POP-UP 1
    public GameObject position;
    public GameObject arrow;
    public GameObject pushBoxText;
    public GameObject[] sampleBoxes;

    //objs in POP-UP 2
    public GameObject boxElimText;
    public GameObject panel;
    public GameObject cld;
    //objs in POP-UP 3
    public GameObject throwBombText;
    public GameObject nextLevelText;
    
    public GameObject nextButton;
    

    void Start()
    {
        instance = GameManager.Instance;
        instance.spawn = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (popUpIndex == 0)
        {
            // pushing boxes to eliminate them
            displayBoxes();
            displayPushBox();

        }
        else if (popUpIndex == 1)
        {
            // Display mechanics
            hidePushBox();
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                enableMechanics();
            }
            

        }
        else if (popUpIndex == 2)
        {
            // ask the player to avoid boxes and get one more elimination
            
            
            displayBoxElim();
            instance.spawn = true;
            cld.SetActive(false);
            
            if (instance.player2BombGained >= 1)
            {
                popUpIndex++;
            }
        } 
        else if (popUpIndex == 3)
        {
            waitTime = 3 * WAIT_TIME;
            boxElimText.SetActive(false);
            throwBombText.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            // end of this tutorial, display of next button
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                throwBombText.SetActive(false);
                nextLevelText.SetActive(true);
                nextButton.SetActive(true);

                Time.timeScale = 0;
            }
            
        }
    }

    private void displayBoxes()
    {
        for (int i = 0; i < sampleBoxes.Length; i++)
        {
            if (!sampleBoxes[i] || sampleBoxes[i].IsDestroyed())
            {
                popUpIndex++;
                break;
            }
                
            sampleBoxes[i].SetActive(true);
        }
    }

    private void displayPushBox()
    {
        arrow.SetActive(true);
        position.SetActive(true);
        panel.SetActive(true);
        pushBoxText.SetActive(true);
    }

    private void hidePushBox()
    {
        arrow.SetActive(false);
        position.SetActive(false);
        panel.SetActive(false);
        pushBoxText.SetActive(false);
    }

    private void displayBoxElim()
    {
        panel.SetActive(true);
        boxElimText.SetActive(true);
    }
    public void disableInstruction()
    {
        instructions.SetActive(false);
        instructionButton.SetActive(false);
        popUpIndex = 0;
    }

    public void enableMechanics()
    {
        mechanics.SetActive(true);
        mechanicButton.SetActive(true);
        //prevButton.SetActive(false);
    }
    
    public void disableMechanics()
    {
        mechanics.SetActive(false);
        mechanicButton.SetActive(false);
        popUpIndex++;
        //prevButton.SetActive(false);

    }

    public void backToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
