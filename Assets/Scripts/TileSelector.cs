using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{

    Transform target;
    Vector3 offscreenHome;
    bool hasTarget = false;

    public Vector3 offset = new Vector3(0, 1.0f, 0);

    public DeselectPlane plane;

    // Start is called before the first frame update
    void Start()
    {
        //Instance = this;

        offscreenHome = transform.position;
        GridBuilder.Instance.OnTileSelected += TileSelected;
        plane.OnDeselect += TileDeselected;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasTarget) {
            transform.position = target.position + offset;
        }
    }

    void TileSelected(Transform tile)
    {
        hasTarget = true;
        target = tile;
    }

    void TileDeselected()
    {
        hasTarget = false;
        transform.position = offscreenHome;
    }

    
}
