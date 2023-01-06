using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{

    float timeTilSpawn = 2;
    float timeBetweenSpawns = 5;

    // Start is called before the first frame update
    void Start()
    {
        //timeTilSpawn = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        // every x seconds, spawn gold on tiles around
        timeTilSpawn -= Time.deltaTime;
        if(timeTilSpawn < 0)
        {
            timeTilSpawn = timeBetweenSpawns;
            SpawnAround();
        }
    }

    void SpawnAround()
    {
        var neighbours = GridBuilder.Instance.GetNeighbours(transform.position);

        foreach(var n in neighbours) {

            if (n.CanHoldGold())
            {
                n.AddGold(1);
            }
        }
    }
}
