using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDir; // move direction
    public LayerMask detectLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move right
        if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDir = Vector2.right;
        // move left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDir = Vector2.left;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            moveDir = Vector2.up;


        // has pressed button left or right
        if (moveDir != Vector2.zero) {
            if (CanMoveInThisDir(moveDir)) {
                Move(moveDir);
            }
        }
        moveDir = Vector2.zero;
    }

    private void Move(Vector2 dir)
    {
        transform.Translate(dir);
    }

    /**
    can move only when no obstacle
    */
    bool CanMoveInThisDir(Vector2 dir) {
        // detectLayer to avoid hit player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.4f, detectLayer);
        if (!hit) return true;
        else {
            if (hit.collider.GetComponent<Box>() != null)
                return hit.collider.GetComponent<Box>().CanMoveInThisDir(dir);
        }
        return false;
    }
}
