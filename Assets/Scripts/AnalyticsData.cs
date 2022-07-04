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
        Debug.Log("gainBomb");
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
        Debug.Log("useBomb");
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






}
