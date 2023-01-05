using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{

    private Vector3 mOffset;
    float mzCoord;

    Coords myCoords;

    private Vector3 GetMouseWorldPos()
    {
        /*Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mzCoord;

        //Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mousePoint);
        //return new Vector3(mouseWorld.x, 1.0f, mouseWorld.z);
        return Camera.main.ScreenToWorldPoint(mousePoint);*/


        return MousePosition.Instance.GridPosition;
    }

    private void OnMouseDown()
    {
        mzCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        // mzCoord = 0.0f;

        mOffset = transform.position - GetMouseWorldPos();

        myCoords = GridBuilder.Instance.PositionToCoords(transform.position);

        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void OnMouseDrag()
    {
        // this isn't quite right
        // but close enough
        // I don't want the tiles to go up and down based on perspective, but oh well
        //Vector3 mouse = GetMouseWorldPos();
        //transform.position = new Vector3(mouse.x + mOffset.x, transform.position.y, mouse.z + mOffset.z);
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
        //Debug.Log($"Position: {transform.position}, Coords: {c.x}, {c.y}");
        //Debug.Log($"In bounds? {GridBuilder.Instance.IsInBounds(c)}");

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
