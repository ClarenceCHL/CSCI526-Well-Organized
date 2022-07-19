using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectReset : MonoBehaviour
{
    public GameObject box;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Box")
        {
            box.transform.position = new Vector3(1.5f, -1.5f, 0.0f);
            player.transform.position = new Vector3(-3.5f, -3.5f, 0.0f);
        }
    }
}
