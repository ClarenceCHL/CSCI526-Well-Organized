using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject objectToSpawn1;
    public GameObject objectToSpawn2;

    private float dropRate = 1.0f; //drop a box every 1 second
    private float nextDrop = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate(objectToSpawn, new Vector3(Random.Range(0, 8), 15, 0), Quaternion.identity);
    }

    int boxCount = 0;
    float time = 0;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;


        if (time > nextDrop)
        {
            float xPos = Random.Range(-4, 5) - 0.5f;

            int boxType = Random.Range(1, 3);

            if (boxType == 1)
            {
                var newBox1 = Instantiate(objectToSpawn1, new Vector3(xPos, 15, 0), Quaternion.identity);
                newBox1.name = "Box#" + boxCount;

            }else
            {
                var newBox2 = Instantiate(objectToSpawn2, new Vector3(xPos, 15, 0), Quaternion.identity);
                newBox2.name = "Box#" + boxCount;
            }
            nextDrop = time + dropRate;
            boxCount++;
        }
    }
}
