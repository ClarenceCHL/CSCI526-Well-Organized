using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDir; // move direction
    public LayerMask detectLayer;
    private bool jump;


    [SerializeField] private Transform groundCheckTransform;

    private Rigidbody2D rb;


    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");
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

        //if in air 
        if (!Physics2D.OverlapArea(groundCheckTransform.position, new Vector2(0.1f, 0.1f), detectLayer))
        {
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
    bool CanMoveInThisDir(Vector2 dir)
    {
        // detectLayer to avoid hit player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.0f, detectLayer);
        if (!hit) return true;
        else
        {
            if (hit.collider.GetComponent<Box>() != null)
                return hit.collider.GetComponent<Box>().CanMoveInThisDir(dir);
        }
        return false;
    }




}
