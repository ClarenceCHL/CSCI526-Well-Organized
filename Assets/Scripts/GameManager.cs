using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Box box;
    private float dropRate = 2.0f; //drop a box every 2 seconds
    private float nextDrop = 0.0f;

    public GameObject shoko;
    public GameObject player;
    public GameObject mainCamera;

    int boxCount = 0;
    float time = 0;

    //public List<Box> boxList = new List<Box>();

    public int width = 10;
    public int height = 9;

    static Box[,] grid;

    PlayerController playerController;

    private static GameManager _instance;

    public static GameManager Instance { get => _instance; set => _instance = value; }

    private void Awake()
    {
        _instance = this;
        grid = new Box[width, height];
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        time += Time.deltaTime;
        
        if (time > nextDrop)
        {
            dropBox();
            nextDrop = time + dropRate;
        }


    }

    public List<Box> checkMatched(Box box)
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
    }

}
