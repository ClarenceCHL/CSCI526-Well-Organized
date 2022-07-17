using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager instance;
    
    public GameObject[] popUps;
    private int popUpIndex;
    // other objs in POP-UP 1
    public GameObject[] arrows;
    private bool left;
    private bool right;
    private bool up;
    // other objs in POP-UP 2
    public GameObject pushBox;
    public GameObject position;
    public GameObject[] sampleBoxes;
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
            for (int i = 0; i < sampleBoxes.Length; i++)
            {
                sampleBoxes[i].SetActive(true);
            }
            pushBox.SetActive(true);
            position.SetActive(true);
            
            //TODO: Add trigger here
            //if (trigger) {
            // 
            // popupIndex++
            //}
        }
        else if (popUpIndex == 2)
        {
            instance.spawn = true;
        }
    }
}
