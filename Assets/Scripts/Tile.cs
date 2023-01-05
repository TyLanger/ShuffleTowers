using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    Vector3 desiredPostion;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] TileType tileType;


    // Start is called before the first frame update
    void Start()
    {
        SetRandomTileType();
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

    public void SetTileType(TileType newType)
    {
        tileType = newType;

        var mat = GetComponent<MeshRenderer>().material;
        switch(tileType)
        {
            case TileType.Grass:
                mat.color = Color.yellow;
                break;

            case TileType.Forest:
                mat.color = Color.green;
                break;

            case TileType.Mountain:
                mat.color = Color.red;
                break;

            case TileType.Water:
                mat.color = Color.blue;
                break;
        }
    }

    public void SetRandomTileType()
    {
        var r = Random.Range(0, 4);

        SetTileType((TileType)r);
    }
}

public enum TileType
{
    Grass, Forest, Mountain, Water
}