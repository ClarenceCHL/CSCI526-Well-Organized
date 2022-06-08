using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{


    public LayerMask detectLayer;

    [SerializeField] private Transform horizontalCheck;

    [SerializeField] private LayerMask sameBoxType;

   
    private ContactFilter2D contact2D;

    private bool matched;

    public void Start()
    {
        
        contact2D.useLayerMask = true;
        contact2D.layerMask = sameBoxType;

    }

    public void CanMoveInThisDir(Vector2 dir) {

        if (matched) return; //matched boxes waiting to be destroyed can't be pushed

        //return if box is still moving
        if ( Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 0.1f ) 
            return; 
        

        if(dir != null)
        {
            bool selfHit = false;

            // detect if hit some obstacle 
            RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.4f, dir, 0.5f);
            RaycastHit2D upHit = Physics2D.Raycast(transform.position + (Vector3)(Vector2.up) * 0.6f, Vector2.up, 0.5f);
            

            if (upHit)
            {
                Debug.Log("uphit");
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

            }
        }

    }


    int collideNumber;
    int verticalCollideNumber; 
    private Collider2D[] collisionList = new Collider2D[5];
    bool destroy; 

    private void FixedUpdate()
    {
        destroy = true;

        //horizontal 
        collideNumber = Physics2D.OverlapArea(new Vector2(horizontalCheck.position.x - 1.0f, horizontalCheck.position.y), new Vector2(horizontalCheck.position.x + 1.0f, horizontalCheck.position.y - 0.05f), contact2D, collisionList);

        if ( collideNumber > 2)  //if left and right are the same box 
        {

            collideNumber = Physics2D.OverlapArea( new Vector2 (horizontalCheck.position.x -2.0f, horizontalCheck.position.y), new Vector2(horizontalCheck.position.x + 2.0f, horizontalCheck.position.y - 0.05f), contact2D, collisionList);

            for (int i =0; i< collideNumber; i++)
            {
                Rigidbody2D rb = collisionList[i].GetComponent<Rigidbody2D>();
                // Debug.Log(rb.velocity);

                if (Mathf.Abs (rb.velocity.y) > 0.1f )
                    destroy = false;    //so box doesn't get destroyed while dropping      
            }

            if (destroy)
            {
                matched = true; 
                for (int i = 0; i < collideNumber; i++)
                {      
                    Destroy(collisionList[i].gameObject, 1.0f);
                }

            }
        }

        //vertical
        verticalCollideNumber  = Physics2D.OverlapArea(new Vector2(horizontalCheck.position.x, horizontalCheck.position.y-1.0f), new Vector2(horizontalCheck.position.x-0.05f, horizontalCheck.position.y + 1.0f), contact2D, collisionList);

        if (verticalCollideNumber > 2)  //if up and down are the same box 
        {

            collideNumber = Physics2D.OverlapArea(new Vector2(horizontalCheck.position.x, horizontalCheck.position.y-2.0f), new Vector2(horizontalCheck.position.x -0.05f, horizontalCheck.position.y +2.0f), contact2D, collisionList);

            for (int i = 0; i < collideNumber; i++)
            {
                Rigidbody2D rb = collisionList[i].GetComponent<Rigidbody2D>();
                // Debug.Log(rb.velocity);

                if (Mathf.Abs(rb.velocity.y) > 0.1f)
                    destroy = false;    //so box doesn't get destroyed while dropping      
            }
      
            if (destroy)
            {
               
                for (int i = 0; i < collideNumber; i++)
                {
                    if (collisionList[i].gameObject != null)  //avoid double deleting
                    {   

                        Destroy(collisionList[i].gameObject, 1.0f);
                    }
                }

            }
        }


    }


}
