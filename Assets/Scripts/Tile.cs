using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    Vector3 desiredPostion;

    [SerializeField] float moveSpeed = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, desiredPostion, moveSpeed * Time.deltaTime);
    }

    public void SetDesiredPosition(Vector3 pos)
    {
        desiredPostion = pos;
    }
}
