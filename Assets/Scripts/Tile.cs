using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public event System.Action<int> OnGoldChanged;

    Vector3 desiredPostion;
    Coords myCoords;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] TileType tileType;

    bool hasTower = false;

    public int goldCount { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        SetRandomTileType();
        GridBuilder.Instance.OnTileSwapped += OnSwap;
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

    public void Initialize(Vector3 pos, Coords c)
    {
        SetDesiredPosition(pos);
        myCoords = c;
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
        //Debug.Log($"Add gold at {myCoords}. new = {goldCount} + {count}");

        goldCount += count;
        OnGoldChanged?.Invoke(goldCount);
    }

    public void ClearGold()
    {
        //Debug.Log($"Clear gold at {myCoords}. Was {goldCount}");
        goldCount = 0;
        OnGoldChanged?.Invoke(goldCount);
    }

    public bool CanHoldGold()
    {
        // future:
        // only accept gold if under the cap
        // and don't have other resources
        // assuming a ssytem where a tile only has 1 type at a time.
        return true;
    }

    void OnSwap(Tile a, Tile b)
    {
        if(a == this)
        {
            if(goldCount > 0)
            {
                if(b.goldCount > 0)
                {
                    AddGold(b.goldCount);
                    b.ClearGold();
                }
            }
        }
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