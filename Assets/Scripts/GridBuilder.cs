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

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        grid = new Tile[width, height];
        PopulateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

}

public struct Coords
{
    public int x;
    public int y;
}