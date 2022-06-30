using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    public Animator animator; 
    private ContactFilter2D contact2D;
    private Collider2D[] collisionList = new Collider2D[5];

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("explode", false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnDestroy()
    {
        animator.SetBool("explode", true);
       int collideNumber = Physics2D.OverlapArea(new Vector2(transform.position.x - 1.0f, transform.position.y-1.0f), new Vector2(transform.position.x + 1.0f, transform.position.y +1.0f), contact2D, collisionList);
        for (int i = 0; i < collideNumber; i++)
        {
            if(collisionList[i].gameObject.layer == 7 || collisionList[i].gameObject.layer == 8)
                Destroy(collisionList[i].gameObject);


            if (collisionList[i].gameObject.name == "Player 1") //player1 
            {
                if(this.gameObject.name == "P2_Bomb(Clone)")
                {
                    GameManager.Instance.lostHP(1);
                    
                    AnalyticsService.Instance.CustomData("lostHP", new Dictionary<string, object>
                    {
                        {"level", SceneManager.GetActiveScene().name},
                        {"reason", "HitByBomb"},
                        {"Player", 1}
                    });
                }
               

            }


            if (collisionList[i].gameObject.name == "Player 2") //player2
            {
                if (this.gameObject.name == "P1_Bomb(Clone)")
                {
                    GameManager.Instance.lostHP(2);
                    
                    AnalyticsService.Instance.CustomData("lostHP", new Dictionary<string, object>
                    {
                        {"level", SceneManager.GetActiveScene().name},
                        {"reason", "HitByBomb"},
                        {"Player", 2}
                    });
                }

            }
        }
    }


}