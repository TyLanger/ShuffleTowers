using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{

    public static MousePosition Instance;

    public Vector3 GridPosition { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Ray CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane eyePlane = new Plane(Vector3.up, Vector3.zero);

        if (eyePlane.Raycast(CameraRay, out float cameraDist))
        {
            Vector3 lookPoint = CameraRay.GetPoint(cameraDist);
            Vector3 eyeLookPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
            GridPosition = eyeLookPoint;
            
        }
    }
}
