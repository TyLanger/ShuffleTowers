using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{

    private Vector3 mOffset;

    Coords myCoords;

    private Vector3 GetMouseWorldPos()
    {
        return MousePosition.Instance.GridPosition;
    }

    private void OnMouseDown()
    {
        mOffset = transform.position - GetMouseWorldPos();

        myCoords = GridBuilder.Instance.PositionToCoords(transform.position);
        // select this
        GridBuilder.Instance.TileSelected(myCoords);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset + Vector3.up;
        Swap();
    }

    private void OnMouseUp()
    {
        Swap();
    }

    void Swap()
    {
        Coords c = GridBuilder.Instance.PositionToCoords(transform.position);

        if (!myCoords.Equals(c))
        {
            if (GridBuilder.Instance.IsInBounds(c))
            {
                GridBuilder.Instance.Swap(myCoords, c);
                myCoords = c;
            }
        }

        
    }
}
