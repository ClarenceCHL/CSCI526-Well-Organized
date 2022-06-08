using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    GameManager _gameManager;

    public LayerMask detectLayer;

    [SerializeField] private Transform horizontalCheck;

    [SerializeField] private LayerMask sameBoxType;

    private bool spawned;
    private bool moved;

    private ContactFilter2D contact2D;
    //further use
    private int _x;
    private int _y;

    public int X
    {
        get { return _x; }
        set { _x = value; }
    }

    public int Y
    {
        get { return _y; }
        set { _y = value; }
    }

    private BoxType _type;

    public BoxType Type
    {
        get { return _type; }

        set { _type = value; }
    }

    public enum BoxType
    {
        NORMAL,
        RED,
        YELLOW,
        NULL
    };

    public void OnCreate(GameManager gameManager, BoxType color)
    {
        _gameManager = gameManager;
        _type = color;
    }

    public void Start()
    {
        moved = false;
        spawned = true;
        contact2D.useLayerMask = true;
        contact2D.layerMask = sameBoxType;
    }

   


    int collideNumber;
    private Collider2D[] collisionList = new Collider2D[5];
    bool destroy; 

    private void FixedUpdate()
    {
        if(this.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            if (spawned)
            {
                _gameManager.addToGrid(this);
                _gameManager.updateGrid(this);
                spawned = false;
            }

            if (moved)
            {
                _gameManager.updateGrid(this);
                moved = false;  
            }
            
        }


    }

    public void CanMoveInThisDir(Vector2 dir)
    {


        if (dir != null)
        {
            bool selfHit = false;

            // detect if hit some obstacle 
            RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.4f, dir, 0.5f);
            RaycastHit2D upHit = Physics2D.Raycast(transform.position + (Vector3)(Vector2.up) * 0.6f, Vector2.up, 0.5f);

            //float x = hit.transform.position.x;
            //float y = hit.transform.position.y;

            if (upHit)
            {
                //Debug.Log("uphit");
                return;
            }

            if (hit && hit.collider.name == transform.name)
            {
                selfHit = true;

            }

            if (!hit || selfHit)
            {
                // no obstacle move in "dir" direction
                transform.Translate(dir);
                moved = true;
                

                //TODO: Trigger update grid in GameManager
            }
        }


    }

    public void Move(int newX, int newY)
    {

    }

    
}
