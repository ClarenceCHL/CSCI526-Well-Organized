using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Box : MonoBehaviour
{

    public PlayerController player1;
    public PlayerController2 player2;

    public LayerMask detectLayer;

    [SerializeField] private Transform horizontalCheck;

    [SerializeField] private LayerMask sameBoxType;

    GameManager _gameManager;

    private ContactFilter2D contact2D;

    private bool matched;

    private Scene curScene;

    public void Start()
    {

        contact2D.useLayerMask = true;
        contact2D.layerMask = sameBoxType;
        _gameManager = GameManager.Instance;
    }

    // check is wallBox or not
    int testNumber;
    private Collider2D[] testList = new Collider2D[5];
    private ContactFilter2D test2D;

    public void CanMoveInThisDir(Vector2 dir)
    {

        if (matched) return; //matched boxes waiting to be destroyed can't be pushed

        //return if box is still moving
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 0.01f)
            return;

        if (dir != null)
        {
            bool selfHit = false;
            bool isWallBox = false;

            // detect if hit some obstacle 
            RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.4f, dir, 0.8f);
            RaycastHit2D upHit = Physics2D.Raycast(transform.position + (Vector3)(Vector2.up) * 0.6f, Vector2.up, 0.5f);


            if (upHit)
            {
                testNumber = Physics2D.OverlapArea(horizontalCheck.position,
                                                   new Vector2(horizontalCheck.position.x - 0.05f, horizontalCheck.position.y + 2.0f),
                                                   test2D, testList);
                for (int i = 0; i < testNumber; i++)
                {
                    Rigidbody2D targetBox = testList[i].GetComponent<Rigidbody2D>();
                    if (targetBox.gameObject.layer == 3)
                    {
                        isWallBox = true;
                    }
                }
                if (!isWallBox)
                {
                    return;
                }
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
    bool destroying = false;
    bool isPlayer1; //see which player earns bomb 



    private void FixedUpdate()
    {
        destroy = true;

        //horizontal 
        collideNumber = Physics2D.OverlapArea(new Vector2(horizontalCheck.position.x - 1.0f, horizontalCheck.position.y),
                                              new Vector2(horizontalCheck.position.x + 1.0f, horizontalCheck.position.y - 0.05f),
                                              contact2D, collisionList);

        if (!destroying && collideNumber > 2)  //if left and right are the same box 
        {

            collideNumber = Physics2D.OverlapArea(new Vector2(horizontalCheck.position.x - 2.0f, horizontalCheck.position.y),
                                                  new Vector2(horizontalCheck.position.x + 2.0f, horizontalCheck.position.y - 0.05f),
                                                  contact2D, collisionList);
            
            for (int i = 0; i < collideNumber; i++)
            {
                Rigidbody2D rb = collisionList[i].GetComponent<Rigidbody2D>();
                // Debug.Log(rb.velocity);

                if (Mathf.Abs(rb.velocity.y) > 0.1f)
                    destroy = false;    //so box doesn't get destroyed while dropping    
            }

            if (destroy)
            {

                // Horizontal analytics
                
                Analytics.CustomEvent("boxDestroyedH", new Dictionary<string, object>
                {
                    { "num_boxes", collideNumber},
                    {"level", SceneManager.GetActiveScene().name}
                });
                
                
                destroying = true;

                if (collisionList[0].gameObject.layer == 7) //box1
                {
                    isPlayer1 = true;
                }
                else
                {
                    isPlayer1 = false;
                }


                for (int i = 0; i < collideNumber; i++)
                {
                    collisionList[i].gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 0.5f); //change color 
                    Destroy(collisionList[i].gameObject, 1.0f);
                    if (contact2D.layerMask.value == 128)
                    {
                        _gameManager.addScore(1);
                    }
                    else if (contact2D.layerMask.value == 1280)
                    {
                        _gameManager.addScore(2);
                    }
                }
                if(SceneManager.GetActiveScene().name == "teaching1")
                {
                    return;
                }
                if (isPlayer1)
                {
                    GlobalVariables.P1MatchCount++;

                    if (GlobalVariables.P1MatchCount > 0 && GlobalVariables.P1MatchCount % 2 == 0)
                    {
                        player1.P1BombCount++;
                        _gameManager.showBombPlus(1);
                        _gameManager.gainBomb(1);
                        //GlobalVariables.P1MatchCount -= 2;
                    }
                }
                else
                {
                    GlobalVariables.P2MatchCount++;
                    curScene = SceneManager.GetActiveScene();
                    
                    if (GlobalVariables.P2MatchCount == 1 && curScene.name == "teaching2")
                    {
                        player2.P2BombCount += 10;
                        _gameManager.showBombPlus(3);
                        GlobalVariables.P2MatchCount--;
                    }
                    if (GlobalVariables.P2MatchCount > 0 && GlobalVariables.P2MatchCount % 2 == 0)
                    {
                        player2.P2BombCount++;
                        _gameManager.showBombPlus(2);
                        _gameManager.gainBomb(2);
                        //GlobalVariables.P2MatchCount -= 2;

                    }
                }
            }
            else
            {
                destroying = false;
            }
        }

        //vertical
        verticalCollideNumber = Physics2D.OverlapArea(new Vector2(horizontalCheck.position.x, horizontalCheck.position.y - 1.0f), new Vector2(horizontalCheck.position.x - 0.05f, horizontalCheck.position.y + 1.0f), contact2D, collisionList);

        destroy = true;

        if (!destroying && verticalCollideNumber > 2)  //if up and down are the same box 
        {

            collideNumber = Physics2D.OverlapArea(new Vector2(horizontalCheck.position.x, horizontalCheck.position.y - 2.0f), new Vector2(horizontalCheck.position.x - 0.05f, horizontalCheck.position.y + 2.0f), contact2D, collisionList);

            for (int i = 0; i < collideNumber; i++)
            {
                Rigidbody2D rb = collisionList[i].GetComponent<Rigidbody2D>();
                // Debug.Log(rb.velocity);

                if (Mathf.Abs(rb.velocity.y) > 0.01f)
                    destroy = false;    //so box doesn't get destroyed while dropping

            }

            if (destroy)
            {
                destroying = true;
                
                // Vertical Analytics
                Analytics.CustomEvent("boxDestroyedV", new Dictionary<string, object>
                {
                    { "num_boxes", collideNumber},
                    {"level", SceneManager.GetActiveScene().name}
                });

                if (collisionList[0].gameObject.layer == 7) //box1
                {
                    isPlayer1 = true;
                }
                else
                {
                    isPlayer1 = false;
                }


                for (int i = 0; i < collideNumber; i++)
                {
                    if (collisionList[i].gameObject != null)  //avoid double deleting
                    {
                        collisionList[i].gameObject.GetComponent<SpriteRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 1.0f); //change color 

                        Destroy(collisionList[i].gameObject, 1.0f);
                        if (contact2D.layerMask.value == 128)
                        {
                            _gameManager.addScore(1);
                        }
                        else if (contact2D.layerMask.value == 1280)
                        {
                            _gameManager.addScore(2);
                        }
                    }
                }
                if (SceneManager.GetActiveScene().name == "teaching1")
                {
                    return;
                }
                if (isPlayer1)
                {
                    GlobalVariables.P1MatchCount++;
                    if (GlobalVariables.P1MatchCount > 0 && GlobalVariables.P1MatchCount % 2 == 0)
                    {
                        player1.P1BombCount++;
                        _gameManager.showBombPlus(1);
                        //GlobalVariables.P1MatchCount -= 2;

                        _gameManager.gainBomb(1);
                    }
                }
                else
                {
                    GlobalVariables.P2MatchCount++;
                    if (GlobalVariables.P2MatchCount > 0 && GlobalVariables.P2MatchCount % 2 == 0)
                    {
                        player2.P2BombCount++;
                        _gameManager.showBombPlus(2);
                        //GlobalVariables.P2MatchCount -= 2;

                        _gameManager.gainBomb(2);
                    }



                }


            }
            else
            {
                destroying = false;
            }
        }


    }


}