using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Analytics: MonoBehaviour
{
    public static Analytics instance;
    private AnalyticsData analyticsData;
    
    private string level;
    private string timer;
    private string p1Score;
    private string p2Score;
    private string winner;
    private string p1BombGained;
    private string p2BombGained;
    private string p1BombUsed;
    private string p2BombUsed;

    private string PLAYER_DATA_URL =
        "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdDPDzgZ5pVaZPUAQBmfsAvCQfgPz_pUNfJV13iN67oGIf6Qw/formResponse";

    private void Awake()
    {
        instance = this;
        analyticsData = AnalyticsData.instance;
    }

    public void updateLevel(string level)
    {
        this.level = level;
    }
    
    public void updateTimer(float elapsed_time)
    {
        timer = elapsed_time.ToString();
    }

    public void updateScore(int P1Score, int P2Score)
    {
        p1Score = P1Score.ToString();
        p2Score = P2Score.ToString();
    }

    public void updateWinner(int playerID)
    {
        winner = playerID.ToString();
    }

    
    
    public void updateBombGained(int P1BombGained, int P2BombGained)
    {
        p1BombGained = P1BombGained.ToString();
        p2BombGained = P2BombGained.ToString();
    }
    
    public void updateBombUsed(int P1BombUsed, int P2BombUsed)
    {
        p1BombUsed = P1BombUsed.ToString();
        p2BombUsed = P2BombUsed.ToString();
    }


    public void getAnalyticData()
    {
        updateBombGained(analyticsData.player1BombGained, analyticsData.player2BombGained);
        updateBombUsed(analyticsData.player1BombUsed, analyticsData.player2BombUsed);
        
    } 
    public void Send()
    {
        StartCoroutine(PostPlayerData());
    }

    IEnumerator PostPlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1647871975", level);
        form.AddField("entry.133440955", winner);
        form.AddField("entry.708284665", timer);
        form.AddField("entry.856680602", p1BombGained);
        form.AddField("entry.1579663834", p1BombUsed);
        form.AddField("entry.1578529338", p2BombGained);
        form.AddField("entry.615295889", p2BombUsed);
        form.AddField("entry.1234766828", p1Score);
        form.AddField("entry.2036727179", p2Score);
        UnityWebRequest www = UnityWebRequest.Post(PLAYER_DATA_URL, form);
        yield return www.SendWebRequest();
    }
    
    
    
    
    
}