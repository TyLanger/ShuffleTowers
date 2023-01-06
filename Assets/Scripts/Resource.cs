using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    Coords myCoords;

    // Start is called before the first frame update
    void Start()
    {
        GridBuilder.Instance.OnTileSwapped += Swap;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target + Vector3.up * 0.3f, moveSpeed * Time.deltaTime);
    }

    public void SetCoords(Coords c)
    {
        myCoords = c;
        GridBuilder.Instance.AddGold(c, 1);

        UpdateTarget();
    }

    void Swap(Coords a, Coords b)
    {
        //Debug.Log("Swap");
        // a is what the player is holding
        // was I b?
        if(myCoords.Equals(b))
        {
            //Debug.Log("MAtched B");
            GridBuilder.Instance.AddGold(b, -1);
            GridBuilder.Instance.AddGold(a, 1);

            // move to a
            myCoords = a;
            UpdateTarget();
        }
    }

    void UpdateTarget()
    {
        //target = GridBuilder.Instance.GetPosition(myCoords.x, myCoords.y);
        Tile tile = GridBuilder.Instance.GetTile(myCoords);
        Vector3 offset = tile.GetOffset();
        transform.parent = tile.transform;
        transform.localPosition = offset * 0.35f + Vector3.up * 0.3f;
    }
}
