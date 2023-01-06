using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{

    [SerializeField] int width = 10;
    [SerializeField] int height = 10;
    [SerializeField] float spacing = 1.0f;

    [SerializeField] Tile tilePrefab;

    Tile[,] grid;

    public static GridBuilder Instance;

    public event System.Action<Transform> OnTileSelected;
    public event System.Action<Coords, Coords> OnTileSwapped;

    public DeselectPlane plane;

    bool tileSelected = false;
    Coords currentSelected;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        grid = new Tile[width, height];
        PopulateGrid();

        plane.OnDeselect += TileDeselected;

    }
    

    void PopulateGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 pos = GetPosition(i, j);
                Tile copy = Instantiate(tilePrefab, transform.position, Quaternion.identity, transform);
                copy.gameObject.name = string.Format("Tile {0}, {1}", i, j);
                copy.SetDesiredPosition(pos);
                //Debug.Log("Pos: " + pos + "Coords: " + PositionToCoords(pos).x + ", " + PositionToCoords(pos).y);
                grid[i, j] = copy;


            }
        }
    }

    public Vector3 GetPosition(int x, int y)
    {
        return transform.position + new Vector3(x * spacing - (spacing * (width-1) / 2), 0.0f, y * spacing - (spacing * (height-1) / 2));
    }

    public Coords PositionToCoords(Vector3 pos)
    {
        // tile 0, 0 is -4.725, -4.725
        // center of grid is 0,0,0
        // need to go from -4.725, -4.725 to Coords(0,0)

        // use getPosition solve for x
        // x * spacing - (spacing * (width-1) / 2)   == pos.x

        float x = (width - 1) + (pos.x - (spacing * (width - 1) / 2)) / spacing;
        float y = (height - 1) + (pos.z - (spacing * (width - 1) / 2)) / spacing;


        Coords c = new Coords
        {
            x =  Mathf.RoundToInt(x),
            y =  Mathf.RoundToInt(y)
        };

        return c;
    }

    public Tile GetTile(Coords c)
    {
        return grid[c.x, c.y];
    }

    public bool IsInBounds(Coords c)
    {
        return c.x < width && c.y < height && c.x >= 0 && c.y >= 0;
    }

    public void Swap(Coords a, Coords b)
    {

        Tile temp = grid[a.x, a.y];
        grid[a.x, a.y] = grid[b.x, b.y];
        grid[b.x, b.y] = temp;

        grid[a.x, a.y].SetDesiredPosition(GetPosition(a.x, a.y));
        grid[b.x, b.y].SetDesiredPosition(GetPosition(b.x, b.y));

        if(currentSelected.Equals(a))
        {
            currentSelected = b;
        } else if(currentSelected.Equals(b))
        {
            currentSelected = a;
        }

        OnTileSwapped?.Invoke(b, a);
    }

    public void TileSelected(Coords c)
    {
        var trans = grid[c.x, c.y].transform;
        OnTileSelected?.Invoke(trans);
        currentSelected = c;
        tileSelected = true;
    }

    void TileDeselected()
    {
        tileSelected = false;
    }

    public bool CanBuildOnSelection()
    {
        if(HasSelection())
        {
            return grid[currentSelected.x, currentSelected.y].CanBuild();
        } 
        else
        {
            return false;
        }
    }

    public void BuiltOnSelection()
    {
        grid[currentSelected.x, currentSelected.y].BuiltTower();
    }

    public bool HasSelection()
    {
        return tileSelected;
    }

    public Transform GetSelectedTile()
    {
        return grid[currentSelected.x, currentSelected.y].transform;
    }

    public Tile[] GetNeighbours(Vector3 position)
    {
        return GetNeighbours(PositionToCoords(position));
    }

    public Tile[] GetNeighbours(Coords center)
    {
        Tile[] v = new Tile[4];

        Coords up = new Coords
        {
            x = center.x,
            y = center.y + 1
        };
        if(IsInBounds(up))
        {
            v[0] = grid[up.x, up.y];
        }
        Coords right = new Coords
        {
            x = center.x + 1,
            y = center.y
        };
        if (IsInBounds(right))
        {
            v[1] = grid[right.x, right.y];
        }
        Coords down = new Coords
        {
            x = center.x,
            y = center.y - 1
        };
        if (IsInBounds(down))
        {
            v[2] = grid[down.x, down.y];
        }
        Coords left = new Coords
        {
            x = center.x - 1,
            y = center.y
        };
        if (IsInBounds(left))
        {
            v[3] = grid[left.x, left.y];
        }

        return v;
    }

    public void AddGold(Coords c, int count)
    {
        grid[c.x, c.y].AddGold(count);
    }
}

public struct Coords
{
    public int x;
    public int y;
}