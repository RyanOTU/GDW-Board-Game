using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//Enum of tile options
//public enum TileOptions { grass, water, forest, stone, barrier };

public class GridManager : MonoBehaviour
{
    public Player[] players;

    //Grid setup with grid object and width and height for 2D array 'gridArray'
    [SerializeField] private GameObject gridObject;
    [SerializeField] private Transform grid;
    public Tile[,] gridArray;
    public int gridWidth = 34;
    public int gridHeight = 16;

    [SerializeField] private Vector2 cornerSize = new Vector2(2, 2);

    private int tileNum = 0; //Used to count the tiles for 
    public static float tileWidth;
    public static float tileHeight;

    //X and Y offset for each piece
    static float xOffset;
    static float yOffset;

    // Start is called before the first frame update
    public void CreateGrid()
    {
        tileNum = 0;
        //Create grid array from the specified height and width
        gridArray = new Tile[gridWidth, gridHeight];

        //Cell Width and Height taken from the grid object's local scale
        tileWidth = gridObject.transform.localScale.x;
        tileHeight = gridObject.transform.localScale.y;

        //X and Y offset to ensure the grid is centered ont he X and Y axis
        //based on the scale of the object * the amount of tiles in the row or column
        xOffset = (tileWidth / 2 *xMultiplier * gridWidth) - tileWidth / 2 * xMultiplier;
        yOffset = (tileHeight / 2 * gridHeight) - tileHeight / 2; 

        //Spawn tile in each cell in the array
        int curPlayer = 0;
        foreach (Player p in players)
        {
            p.playerIndex = (Tile.PlayerNumber)curPlayer;
            ++curPlayer;
        }
        Vector2[] corners = {
            new Vector2(0, 0),
            new Vector2(0, gridHeight-1),
            new Vector2(gridWidth-1, 0),
            new Vector2(gridWidth-1, gridHeight-1)
        };
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                SpawnTile(i, j);
                for (int k = 0; k < corners.Length; k++)
                {
                    if (Mathf.Abs(i-corners[k].x) < cornerSize.x && Mathf.Abs(j - corners[k].y) < cornerSize.y)
                    {
                        gridArray[i, j].SetPlayerIndex((Tile.PlayerNumber)k);
                    }
                }
            }
        }
        
        //Debug.Log("xOffset: " + xOffset + "\n yOffset: " + yOffset);
    }
    public void SpawnTile(int x, int y)
    {
        tileNum += 1;

        gridArray[x, y] = Instantiate(gridObject, grid).GetComponent<Tile>(); //Create new tile from the specified game object in the editor
        gridArray[x, y].name = "Hexagon " + tileNum;

        //Move the position of the tile
        gridArray[x, y].transform.position = GetFromCoordinate(x, y);
    }
    public Tile GetTile(int x, int y)
    {
        return gridArray[x, y];
    }

    static float xMultiplier = 1f/Mathf.Sqrt(5f/4f);
    public static Vector2 GetFromCoordinate(int x, int y)
    {
        if (x % 2 == 0)
        {
            return new Vector2((x*xMultiplier - xOffset), (y - yOffset - tileHeight / 2));
        }
        else
        {
            return new Vector2((x*xMultiplier - xOffset), (y - yOffset));
        }
    }
    public static Vector2 GetFromWorld(Vector2 pos)
    {
        Vector2 center = pos + new Vector2(xOffset, yOffset);
        center /= new Vector2(xMultiplier, 1f);
        if (Mathf.Round(center.x) % 2 == 0)
        {
            center.y += tileHeight/2;
        }
        //Round to make them integers
        center.x = Mathf.Round(center.x);
        center.y = Mathf.Round(center.y);
        return center;
    }
    public Vector2 GetFromWorldClamped(Vector2 pos)
    {
        Vector2 result = GetFromWorld(pos);
        result.x = Mathf.Clamp(result.x, 0, gridWidth-1);
        result.y = Mathf.Clamp(result.y, 0, gridHeight-1);
        return result;
    }

    public void Start()
    {
        CreateGrid();
    }
}
