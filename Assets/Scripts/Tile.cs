using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    Vector3 desiredPostion;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] TileType tileType;

    bool hasTower = false;

    public int goldCount { get; private set; }

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

    public bool CanBuild()
    {
        return !hasTower;
    }

    public void BuiltTower()
    {
        hasTower = true;
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

    public void AddGold(int count)
    {
        goldCount += count;
    }

    public void ClearGold()
    {
        goldCount = 0;
    }

    public Vector3 GetOffset()
    {
        // based on gold count
        int offset = goldCount % 9;

        // up, right, down, left
        // up-right, down-right, down-left, up-left
        switch (offset)
        {
            case 2:
                return new Vector3(0, 0, 1);
            case 3:
                return new Vector3(1, 0, 0);
            case 4:
                return new Vector3(0, 0, -1);
            case 5:
                return new Vector3(-1, 0, 0);
            case 6:
                return new Vector3(1, 0, 1);
            case 7:
                return new Vector3(1, 0, -1);
            case 8:
                return new Vector3(-1, 0, -1);
            case 0:
                return new Vector3(-1, 0, 1);
        }

        // 1 gold goes to the center
        return Vector3.zero;
    }
}

public enum TileType
{
    Grass, Forest, Mountain, Water
}