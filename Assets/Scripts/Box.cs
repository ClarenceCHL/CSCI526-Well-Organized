using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{


    public LayerMask detectLayer;

    [SerializeField] private Transform horizontalCheck;

    [SerializeField] private LayerMask sameBoxType;

   
    private ContactFilter2D contact2D;
   

    public void Start()
    {

        contact2D.useLayerMask = true;
        contact2D.layerMask = sameBoxType;
    }

    public void CanMoveInThisDir(Vector2 dir) {


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
    private Collider2D[] collisionList = new Collider2D[5];
    bool destroy; 

    private void FixedUpdate()
    {
        destroy = true;


        collideNumber = Physics2D.OverlapArea(new Vector2(horizontalCheck.position.x - 1.0f, horizontalCheck.position.y), new Vector2(horizontalCheck.position.x + 1.0f, horizontalCheck.position.y - 0.4f), contact2D, collisionList);
        {

            if ( collideNumber > 2)  //if left and right are the same box 
            {

                collideNumber = Physics2D.OverlapArea( new Vector2 (horizontalCheck.position.x -2.0f, horizontalCheck.position.y), new Vector2(horizontalCheck.position.x + 2.0f, horizontalCheck.position.y - 0.4f), contact2D, collisionList);

                for (int i =0; i< collideNumber; i++)
                {
                    Rigidbody2D rb = collisionList[i].GetComponent<Rigidbody2D>();
                    Debug.Log(rb.velocity);

                    if (rb.velocity.y != 0)
                        destroy = false;    //so box doesn't get destroyed while dropping
           
                }
                if (destroy)
                {
                    for (int i = 0; i < collideNumber; i++)
                    {
                      
                        Destroy(collisionList[i].gameObject);


                    }

                }
            }
        }


    }


}
