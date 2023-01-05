using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeselectPlane : MonoBehaviour
{
    public event System.Action OnDeselect;

    private void OnMouseDown()
    {
        // mouse missed all other colliders
        // deselect
        OnDeselect?.Invoke();
    }
}
