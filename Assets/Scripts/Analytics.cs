using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Analytics: MonoBehaviour
{
    public static Analytics instance;
    private AnalyticsData analyticsData;
    //Player Data
    private string level;
    private string timer;
    private string p1Score;
    private string p2Score;
    private string winner;
    private string p1BombGained;
    private string p2BombGained;
    private string p1BombUsed;
    private string p2BombUsed;
    private string p1HitByBox;
    private string p2HitByBox;
    private string p1DamagedByBomb;
    private string p2DamagedByBomb;
    //Box Position Data
    private int[] box1Position;
    private int[] box2Position;
    private string[] box1Entries;
    private string[] box2Entries;
    //Box ELM Data
    private int[] box1ELM;
    private int[] box2ELM;
    private string[] box1ELMEntries;
    private string[] box2ELMEntries;
    
    [SerializeField] private string PLAYER_DATA_URL =
        "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdDPDzgZ5pVaZPUAQBmfsAvCQfgPz_pUNfJV13iN67oGIf6Qw/formResponse";

    [SerializeField] private string BOX_DATA_URL =
        "https://docs.google.com/forms/u/0/d/e/1FAIpQLSc0pPSM5fttOb21BTtNT-us2pGe-kSv5hpHsOiSHMpIabeDTg/formResponse";

    [SerializeField] private string BOX_ELM_URL =
        "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdQOOu4w1BC8qzwpvt41PX-RSVeTakxSamCAqEOK0X3_vMT1w/formResponse";

    private void Awake()
    {
        instance = this;
        analyticsData = AnalyticsData.instance;
    }

    private void Start()
    {
        level = "";
        timer = "";
        p1Score = ""; 
        p2Score = "";
        winner = "";
        p1BombGained = "";
        p2BombGained = "";
        p1BombUsed = "";
        p2BombUsed = "";
        p1HitByBox = "";
        p2HitByBox = "";
        p1DamagedByBomb = "";
        p2DamagedByBomb = "";

        box1Entries = new string[]
        {
            "entry.511515377",
            "entry.1256125183",
            "entry.2061414801",
            "entry.206385586",
            "entry.570580400",
            "entry.1704605645",
            "entry.336050346",
            "entry.80936901",
            "entry.1355217472",
            "entry.1739283091"
        };
        box2Entries = new string[]
        {
            "entry.1968180819",
            "entry.1784996492",
            "entry.1284386034",
            "entry.1904015774",
            "entry.1449568656",
            "entry.384804528",
            "entry.188949708",
            "entry.2081073181",
            "entry.1570753691",
            "entry.108932323"
        };

        box1ELMEntries = new string[]
        {
            "entry.1385500850",
            "entry.44730658",
            "entry.1630663147"
        };

        box2ELMEntries = new string[]
        {
            "entry.117240318",
            "entry.2042926603",
            "entry.432507716"
        };

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

    
    
    private void updateBombGained(int P1BombGained, int P2BombGained)
    {
        p1BombGained = P1BombGained.ToString();
        p2BombGained = P2BombGained.ToString();
    }
    
    private void updateBombUsed(int P1BombUsed, int P2BombUsed)
    {
        p1BombUsed = P1BombUsed.ToString();
        p2BombUsed = P2BombUsed.ToString();
    }

    private void updateHitByBox(int p1HitByBox, int p2HitByBox)
    {
        this.p1HitByBox = p1HitByBox.ToString();
        this.p2HitByBox = p2HitByBox.ToString();
    }

    private void updateDamageByBomb(int p1DamagedByBomb, int p2DamagedByBomb)
    {
        this.p1DamagedByBomb = p1DamagedByBomb.ToString();
        this.p2DamagedByBomb = p2DamagedByBomb.ToString();
    }

    private void getBoxData()
    {
        box1Position = analyticsData.box1Position;
        box2Position = analyticsData.box2Position;
    }

    private void getBoxELMData()
    {
        box1ELM = analyticsData.box1ELM;
        box2ELM = analyticsData.box2ELM;
    }
    public void getAnalyticData()
    {
        updateBombGained(analyticsData.player1BombGained, analyticsData.player2BombGained);
        updateBombUsed(analyticsData.player1BombUsed, analyticsData.player2BombUsed);
        updateHitByBox(analyticsData.player1HitByBox, analyticsData.player2HitByBox);
        updateDamageByBomb(analyticsData.player1DamagedByBomb, analyticsData.player2DamagedByBomb);
        
        getBoxData();
        getBoxELMData();
    } 
    
    public void Send()
    {
        StartCoroutine(PostPlayerData());
        StartCoroutine(PostBoxData());
        StartCoroutine(PostBoxELMData());
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
        form.AddField("entry.333866671", p1HitByBox);
        form.AddField("entry.299927836", p2HitByBox);
        form.AddField("entry.1821112043", p1DamagedByBomb);
        form.AddField("entry.1695520969", p2DamagedByBomb);
        UnityWebRequest www = UnityWebRequest.Post(PLAYER_DATA_URL, form);
        yield return www.SendWebRequest();
    }
    IEnumerator PostBoxData()
    {
        WWWForm form = new WWWForm();
        for (int i = 0; i < box1Position.Length; i++)
        {
            form.AddField(box1Entries[i], box1Position[i].ToString());
        }
        for (int i = 0; i < box2Position.Length; i++)
        {
            form.AddField(box2Entries[i], box2Position[i].ToString());
        }
        UnityWebRequest www = UnityWebRequest.Post(BOX_DATA_URL, form);
        yield return www.SendWebRequest();
    }
    IEnumerator PostBoxELMData()
    {
        WWWForm form = new WWWForm();
        for (int i = 0; i < box1ELM.Length; i++)
        {
            form.AddField(box1ELMEntries[i], box1ELM[i].ToString());
        }
        for (int i = 0; i < box2ELM.Length; i++)
        {
            form.AddField(box2ELMEntries[i], box2ELM[i].ToString());
        }
        UnityWebRequest www = UnityWebRequest.Post(BOX_ELM_URL, form);
        yield return www.SendWebRequest();
    }
    
    
    
    
    
}