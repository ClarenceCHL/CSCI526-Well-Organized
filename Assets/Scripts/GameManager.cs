using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //[SerializeField] Box box;

    private Scene curScene;

    public GameObject objectToSpawn1;
    public GameObject objectToSpawn2;

    public GameObject player1;
    public GameObject player2;

    public Text P1Score;
    public Text P2Score;

    public Text P1HP;
    public Text P2HP;

    private float dropRate = 2.0f; //drop a box every 2 seconds
    private float nextDrop = 0.0f;

    private int player1Score;
    private int player2Score;

    private int player1HP;
    private int player2HP;

    private Vector3 P1RespawnPoint;
    private Vector3 P2RespawnPoint;

    public GameOverScreen GameOverScreen1;
    public GameOverScreen GameOverScreen2;

    public Camera mainCamera;
    public RectTransform parentCanvas;
    public GameObject P1BombAdd;
    public GameObject P2BombAdd;

    //public GameObject shoko;
    //public GameObject player;
    //public GameObject mainCamera;

    int boxCount = 0;
    float time = 0.0f;

    float width = 0.0f;
    float height = 0.0f;

    //public List<Box> boxList = new List<Box>();

    //public int width = 10;
    //public int height = 9;

    //static Box[,] grid;

    public PlayerController playerController;

    private static GameManager _instance;

    public static GameManager Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        _instance = this;
        //grid = new Box[width, height];
    }

    // Start is called before the first frame update
    void Start()
    {
        //startPosition();
        player1Score = 0;
        player2Score = 0;
        player1HP = 3;
        player2HP = 3;
        curScene = SceneManager.GetActiveScene();
        if (curScene.name == "teaching1" || curScene.name == "teaching2")
        {
            player1HP = 1;
            player2HP = 1;
        }
        if (curScene.name == "teaching2")
        {
            string str_bomb = "Bomb";
            for (int i = 1; i <= 8; i++)
            {
                string bombObj = str_bomb + i;
                GameObject Bomb = GameObject.Find(bombObj);
                Destroy(Bomb, 2);
            }

        }
        P1HP.text = "P1 HP: " + player1HP;
        P2HP.text = "P2 HP: " + player2HP;
        P1RespawnPoint = new Vector3(-3.5f, 7.0f, 0.0f);
        P2RespawnPoint = new Vector3(3.5f, 7.0f, 0.0f);
        width = parentCanvas.sizeDelta.x;
        height = parentCanvas.sizeDelta.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        time += Time.deltaTime;

        
        if (curScene.name != "teaching1" && curScene.name != "teaching2")
        {
            if (time > nextDrop)
            {
                dropBox();
                nextDrop = time + dropRate;
            }
        }      
        

        if (player1HP <= 0)
        {
            gameOver(2);
        }
        else if(player2HP <= 0)
        {
            gameOver(1);
        }
    }

    void dropBox()
    {
        float xPos = Random.Range(-4, 5) - 0.5f;

        int boxType = Random.Range(1, 3);

        GameObject newBox;

        AnalyticsResult boxPosition = Analytics.CustomEvent("boxPosition", new Dictionary<string, object>
        {
            { SceneManager.GetActiveScene().name, xPos + boxType}
        });

        if (boxType == 1)
        {
            newBox = Instantiate(objectToSpawn1, new Vector3(xPos, 15, 0), Quaternion.identity);
            newBox.name = "Box#" + boxCount;

        }
        else
        {
            newBox = Instantiate(objectToSpawn2, new Vector3(xPos, 15, 0), Quaternion.identity);
            newBox.name = "Box#" + boxCount;
        }
        nextDrop = time + dropRate;
        boxCount++;
    }

    public void addScore(int i)
    {
        if(i == 1)
        {
            player1Score += 10;
            P1Score.text = "P1 Score: " + player1Score;
            StartCoroutine(playerBombAdd(i));
        }
        else if(i == 2)
        {
            player2Score += 10;
            P2Score.text = "P2 Score: " + player2Score;
            StartCoroutine(playerBombAdd(i));
        }
    }

    public void lostHP(int playerID)
    {
        if(playerID == 1)
        {
            if(player1HP > 0)
            {
                StartCoroutine(player1Respawn(2.0f));
                player1HP--;
;               P1HP.text = "P1 HP: " + player1HP;
            }
            
        }
        else
        {
            if(player2HP > 0)
            {
                StartCoroutine(player2Respawn(2.0f));
                player2HP--;
                P2HP.text = "P2 HP: " + player2HP;
            }
        }
        
    }

    IEnumerator playerBombAdd(int i)
    {
        if (i == 1)
        {
            Vector3 playerPos = new Vector3(player1.transform.position.x - 1.0f, player1.transform.position.y + 1.0f, player1.transform.position.z);
            Vector3 pos = mainCamera.WorldToScreenPoint(playerPos);
            P1BombAdd.transform.position = pos;
            P1BombAdd.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            P1BombAdd.SetActive(false);
        }
        else if (i == 2)
        {
            Vector3 playerPos = new Vector3(player2.transform.position.x - 1.0f, player2.transform.position.y + 1.0f, player2.transform.position.z);
            Vector3 pos = mainCamera.WorldToScreenPoint(playerPos);
            P2BombAdd.transform.position = pos;
            P2BombAdd.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            P2BombAdd.SetActive(false);
        }
        
    }

    IEnumerator player1Respawn(float timeInMS)
    {

        player1.SetActive(false);

        yield return new WaitForSeconds(timeInMS);

        player1.SetActive(true);
        player1.transform.position = P1RespawnPoint;
    }

    IEnumerator player2Respawn(float timeInMS)
    {

        player2.SetActive(false);

        yield return new WaitForSeconds(timeInMS);

        player2.SetActive(true);
        player2.transform.position = P2RespawnPoint;
    }

    private void gameOver(int i)
    {
        Timer.instance.EndTimer();
        Time.timeScale = 0;
        if (i == 1)
        {
            AnalyticsResult winPlayer = Analytics.CustomEvent("winPlayer", new Dictionary<string, object>
        {
            { "P1 win", SceneManager.GetActiveScene().name}
        });
            GameOverScreen1.Setup();
        }
        else
        {
            AnalyticsResult winPlayer = Analytics.CustomEvent("winPlayer", new Dictionary<string, object>
        {
            { "P2 win", SceneManager.GetActiveScene().name}
        });
            GameOverScreen2.Setup();
        }
    }

    /*public List<Box> checkMatched(Box box)
    {
        Box.BoxType color = box.Type;
        List<Box> finishedMatch = new List<Box>();
        int x = Mathf.RoundToInt(box.GetComponent<Transform>().position.x), y = Mathf.RoundToInt(box.GetComponent<Transform>().position.y);
        finishedMatch.Add(box);

        

        //Check 5
        if (compareCoordColor(x - 2, y, color) && compareCoordColor(x - 1, y, color) && 
            compareCoordColor(x + 1, y, color) && compareCoordColor(x + 2, y, color))
        {
            finishedMatch.Add(grid[x - 2, y]);
            finishedMatch.Add(grid[x + 2, y]);
            finishedMatch.Add(grid[x - 1, y]);
            finishedMatch.Add(grid[x + 1, y]);
        }
        else
        {
            for (int dir = -1; dir <= 1; dir += 2)
            {
                
                if (compareCoordColor(x + 2 * dir, y, color) && compareCoordColor(x - 1, y, color) && compareCoordColor(x + 1, y, color))
                {
                    finishedMatch.Add(grid[x + 2 * dir, y]);
                    finishedMatch.Add(grid[x - 1, y]);
                    finishedMatch.Add(grid[x + 1, y]);
                    break;
                }

                if (compareCoordColor(x + 2 * dir, y, color) && compareCoordColor(x + 1 * dir, y, color))
                {
                    finishedMatch.Add(grid[x + 2 * dir, y]);
                    finishedMatch.Add(grid[x + 1 * dir, y]);
                    break;
                }

                if (dir == -1 && compareCoordColor(x + 1, y, color) && compareCoordColor(x - 1, y, color))
                {
                    finishedMatch.Add(grid[x - 1, y]);
                    finishedMatch.Add(grid[x + 1, y]);
                    break;
                }
            }
        }

        return finishedMatch;
    }

    public void addToGrid(Box box)
    {
        int roundedX = Mathf.RoundToInt(box.transform.position.x);
        int roundedY = Mathf.RoundToInt(box.transform.position.y);
        grid[roundedX, roundedY] = box;
        box.X = roundedX;
        box.Y = roundedY;
        printGrid();
    }

    private void dropBox()
    {
        Box newBox;

        float xPos = Random.Range(0, 10);

        int boxType = 1;
        //TODO: change if/else logic
        if (boxType == 1)
        {
            newBox = Instantiate(box, new Vector3(xPos, 15, 0), Quaternion.identity);
            newBox.OnCreate(this, Box.BoxType.NORMAL);
            newBox.name = "Box#" + boxCount;
            //Debug.Log(newBox.Type);
            //foreach (Box i in boxList)
            //{
                //Debug.Log(i.GetComponent<Transform>().position);
            //}
        }
        else
        {
            newBox = Instantiate(box, new Vector3(xPos, 15, 0), Quaternion.identity);
            newBox.GetComponent<SpriteRenderer>().color = Color.red;
            newBox.OnCreate(this, Box.BoxType.RED);
            newBox.name = "Box#" + boxCount;
        }
        
        boxCount++;
    }


    private void startPosition()
    {
        shoko.transform.position = new Vector3(4.5f, 3.5f, 0.0f);
        player.transform.position = new Vector3(4.5f, 0.0f, 0.0f);
        mainCamera.transform.position = new Vector3(5.0f, 3.5f, -10.0f);
    }

    private Box.BoxType getCoordColor(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height || grid[x, y] == null)
        {
            return Box.BoxType.NULL;
        }
        else
        {
            return grid[x, y].Type;
        }
    }

    private bool compareCoordColor(int x, int y, Box.BoxType color)
    {
        //Debug.Log(getCoordColor(x, y) == color);
        return getCoordColor(x, y) == color;
    }

    public void updateGrid(Box box)
    {
        int x = Mathf.RoundToInt(box.transform.position.x), y = Mathf.RoundToInt(box.transform.position.y);

        if(x > width || x < 0 || y > height || y < 0)
        {
            return;
        }

        if(x != box.X || y != box.Y)
        {
            grid[x, y] = grid[box.X, box.Y];
            grid[box.X, box.Y] = null;
            box.X = x;
            box.Y = y;
            printGrid();
        }

        

        List<Box> removeList = checkMatched(box);
        if (removeList.Count >= 3)
        {
            foreach (Box bob in removeList)
            {
                Destroy(bob.gameObject);
                Destroy(bob);
            }
        }
        return;
    }

    private void printGrid()
    {
        for (int row = height - 1; row >= 0; row--)
        {
            string line = " ";
            for (int col = 0; col < width; col++)
            {
                line += getCoordColor(col, row);
            }
            Debug.Log(line);
        }
        Debug.Log("********************************************");
    }*/

}
