using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    private bool jump = false;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;

    int score = 0;


    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    private bool enableInput = true;

    /*
public void levelClear()
{
    Debug.Log("CLEAR");
    clear.Setup(score);
    enableInput = false;

}

   */


 

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enableInput)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space pressed");
                jump = true;
            }

            horizontalInput = Input.GetAxis("Horizontal");

        }

    }


    private void FixedUpdate()
    {

        rigidbodyComponent.velocity = new Vector3(horizontalInput*4, rigidbodyComponent.velocity.y, 0);


        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            Debug.Log("air");
            return;
        }
    
        if (jump)
        {

            rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jump = false;
        }




    }

    private void OnTriggerEnter(Collider other)
    {
   

        if (other.gameObject.layer == 9) 
        {
            //GameOver();
        }

    }



}
