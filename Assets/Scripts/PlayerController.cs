using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    //public int NumberOfBombsAvailable = GlobalVariables.P1BombCount;
    public Text BombNumber;
    public int P1BombCount =0;

    Vector2 moveDir; // move direction
    public LayerMask detectLayer;
    private bool jump;

    [SerializeField] private GameObject bomb; 

    [SerializeField] private Transform groundCheckTransform;

    private Rigidbody2D rb;


    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Timer.instance.BeginTimer();
    }



    private bool layBomb = false;

    // Update is called once per frame
    void Update()
    {
        BombNumber.text = "AVAILABLE BOMBS: " + P1BombCount;
        
        if (Input.GetKeyDown(KeyCode.RightShift) && P1BombCount > 0 )
        {
            Debug.Log("Bomb");
            layBomb = true;

            P1BombCount--; 
        }




        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           // Debug.Log("Space pressed");
            jump = true;
        }

        // move right
        if (Input.GetKey(KeyCode.RightArrow))
        {

            moveDir = Vector2.right;
        }

        // move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir = Vector2.left;
        }


        horizontalInput = Input.GetAxis("Horizontal");

    }




    private void FixedUpdate()
    {
        CanMoveInThisDir(moveDir);



        if (layBomb)
        {
            GameObject newBomb =  Instantiate(bomb, new Vector3(transform.position.x , transform.position.y, 0), Quaternion.identity); 
            Destroy(newBomb, 2);
            layBomb = false; 
        }

        //if in air 
        if (!Physics2D.OverlapArea(groundCheckTransform.position, new Vector2(groundCheckTransform.position.x+0.8f, groundCheckTransform.position.y-0.1f), detectLayer))
        {
            rb.velocity = new Vector2(horizontalInput * 4, rb.velocity.y);
           // Debug.Log("IN AIR");
            jump = false;

    
            return;
        }


        rb.velocity = new Vector2(horizontalInput * 4, rb.velocity.y);
       


        if (jump)
        {
            rb.velocity = new Vector2(horizontalInput * 4, 5);
            jump = false;
        }


    }

    private void Move(Vector2 dir)
    {
        transform.Translate(dir);
    }

    /**
    can move only when no obstacle
    */
    void CanMoveInThisDir(Vector2 dir)
    {
        // detectLayer to avoid hit player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.0f, detectLayer);
        if (hit) 
       
        {
            if (hit.collider.GetComponent<Box>() != null)
                 hit.collider.GetComponent<Box>().CanMoveInThisDir(dir);
        }
      //  return false;
    }




}
