using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    private ContactFilter2D contact2D;
    private Collider2D[] collisionList = new Collider2D[5];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
       int collideNumber = Physics2D.OverlapArea(new Vector2(transform.position.x - 1.0f, transform.position.y-1.0f), new Vector2(transform.position.x + 1.0f, transform.position.y +1.0f), contact2D, collisionList);
        for (int i = 0; i < collideNumber; i++)
        {
            if(collisionList[i].gameObject.layer == 7 || collisionList[i].gameObject.layer == 8)
                Destroy(collisionList[i].gameObject);
        }
    }
}