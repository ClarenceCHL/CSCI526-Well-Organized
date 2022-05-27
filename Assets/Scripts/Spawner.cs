using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject objectToSpawn;

    private float dropRate = 2.0f; //drop a box every 2 seconds
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

        float xPos = Random.Range(-4, 5);

        xPos -= 0.5f;

        if (time > nextDrop)
        {
            nextDrop = time + dropRate;
            var newBox = Instantiate(objectToSpawn, new Vector3(xPos, 15, 0), Quaternion.identity);
            newBox.name = "Box#" + boxCount;
            boxCount++;
        }
    }
}
