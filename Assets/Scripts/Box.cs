using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool CanMoveInThisDir(Vector2 dir) {
        // detect if hit some obstacle
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.4f, dir, 0.5f);

        if (!hit)
        {
            // no obstacle move in "dir" direction
            transform.Translate(dir);
            return true;
        }

        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
