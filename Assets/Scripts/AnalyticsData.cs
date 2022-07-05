using System;
using UnityEngine;

public class AnalyticsData:MonoBehaviour
{
    public int player1BombGained;
    public int player2BombGained;
    public int player1BombUsed;
    public int player2BombUsed;

    public int player1HitByBox;
    public int player2HitByBox;
    public int player1DamagedByBomb;
    public int player2DamagedByBomb;
    //Box Position Data
    public int[] box1Position;
    public int[] box2Position;
    //Box Elimination Data
    public int[] box1ELM;
    public int[] box2ELM;
    
    public static AnalyticsData instance;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player1BombGained = 0;
        player2BombGained = 0;
        player1BombUsed = 0;
        player2BombUsed = 0;
        
        player1HitByBox = 0; 
        player2HitByBox = 0;
        player1DamagedByBomb = 0;
        player2DamagedByBomb = 0;

        box1Position = new int[10];
        box2Position = new int[10];

        box1ELM = new int[3];
        box2ELM = new int[3];
    }

    public void gainBomb(int playerID)
    {
        if (playerID == 1)
        {
            player1BombGained++;
        }
        else
        {
            player2BombGained++;
        }
    }
    
    public void useBomb(int playerID)
    {
        if (playerID == 1)
        {
            player1BombUsed += 1;
        } else
        {
            player2BombUsed += 1;
        }
    }

    public void hitByBox(int playerID)
    {
        if (playerID == 1)
        {
            player1HitByBox += 1;
        } else
        {
            player2HitByBox += 1;
        }
    }

    public void damageByBomb(int playerID)
    {
        if (playerID == 1)
        {
            player1DamagedByBomb += 1;
        } else
        {
            player2DamagedByBomb += 1;
        }
    }

    public void updateBoxPosition(int boxType, int position)
    {
        int[] boxPosition;
        if (boxType == 1)
        {
            boxPosition = box1Position;
        }
        else
        {
            boxPosition = box2Position;
        }

        boxPosition[position]++;

    }

    public void updateBoxELM(int boxType, int num)
    {
        int[] boxELM;
        if (boxType == 1)
        {
            boxELM = box1ELM;
        }
        else
        {
            boxELM = box2ELM;
        }

        if (num <= 5)
        {
            boxELM[num - 3] += 1;
        }
        else
        {
            boxELM[2] += 1;
        }
    }






}
