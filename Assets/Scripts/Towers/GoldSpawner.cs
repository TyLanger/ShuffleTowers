using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{

    public Resource goldPrefab;

    float timeTilSpawn = 5;
    float timeBetweenSpawns = 5;

    // Start is called before the first frame update
    void Start()
    {
        timeTilSpawn = timeBetweenSpawns;
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
            var copy = Instantiate(goldPrefab, n.transform.position + Vector3.up * 0.3f, Quaternion.identity);
            copy.SetCoords(GridBuilder.Instance.PositionToCoords(n.transform.position));
            //n.AddGold(1);
        }
    }
}
