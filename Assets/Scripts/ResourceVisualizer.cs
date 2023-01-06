using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceVisualizer : MonoBehaviour
{

    Transform visuals;
    public GameObject goldPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //visuals = Instantiate()
        GameObject go = new GameObject("Visual Holder");
        go.transform.parent = transform;
        visuals = go.transform;

        GetComponent<Tile>().OnGoldChanged += OnGoldChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGoldChanged(int newCount)
    {
        for (int i = 0; i < visuals.childCount; i++)
        {
            Destroy(visuals.GetChild(i).gameObject);
        }
        for (int i = 0; i < newCount; i++)
        {
            Instantiate(goldPrefab, transform.position + Vector3.up * 0.3f * (i+1), Quaternion.identity, visuals);
        }
    }
}
