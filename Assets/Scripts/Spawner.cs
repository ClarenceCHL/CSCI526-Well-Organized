using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject objectToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(objectToSpawn, new Vector3(Random.Range(0, 8), 15, 0), Quaternion.identity);
    }

    int counter = 0;
    // Update is called once per frame
    void Update()
    {
        counter++;
            if (counter == 1000)
            {
                Instantiate(objectToSpawn, new Vector3(Random.Range(0, 8), 15, 0), Quaternion.identity);
                 counter = 0;
            }
        
    }
}
