using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;


    void Start()
    {

        rigidbodyComponent = GetComponent<Rigidbody>();
       // rigidbodyComponent.gravity = -0.02f;
    }

    public bool IsCollide; //if box is collided with another box
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.layer == 9) //if other object is also a box 
        {
            //rigidbodyComponent.mass = 1000;
           // Destroy(rigidbodyComponent);
           // UnityEngine.Object.Destroy(rigidbodyComponent);
        }

    }
}
