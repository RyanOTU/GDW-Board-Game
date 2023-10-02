using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//Enum of tile options
//public enum TileOptions { grass, water, forest, stone, barrier };

public class Grid : MonoBehaviour
{
    //Grid width and height for 2D array 'gridArray'
    [SerializeField] private GameObject gridObject;
    public GameObject[,] gridArray;
    private int gridWidth = 34;
    private int gridHeight = 16;

    private float tileWidth;
    private float tileHeight;

    //X and Y offset for each piece
    float xOffset;
    float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        //Create grid array from the specified height and width
        gridArray = new GameObject[gridWidth, gridHeight];

        //Cell Width and Height taken from the grid object's local scale
        tileWidth = gridObject.transform.localScale.x;
        tileHeight = gridObject.transform.localScale.y;

        //X and Y offset to ensure the grid is centered ont he X and Y axis
        //based on the scale of the object * the amount of tiles in the row or column
        xOffset = (tileWidth / 2 * gridWidth) - tileWidth / 2;
        yOffset = (tileHeight / 2  * gridHeight) - tileHeight / 2;

        //Spawn tile in each cell in the array
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Debug.Log("Row: " + i + "\n Column: " + j);
                SpawnTile(i, j);
            }
        }
        Debug.Log("xOffset: " + xOffset + "\n yOffset: " + yOffset);
    }
    public void SpawnTile(int x, int y)
    {
        //Parent scale is set to the current scale of the parent object
        ///Example: If you change your grid parent object to 0.5 scale, the
        ///grid will place and scale correctly in response to the change
        Vector3 parentScale = this.GetComponent<Transform>().localScale;

        GameObject tile = GameObject.Instantiate(gridObject); //Create new tile from the specified game object in the editor

        //Move the position of the tile
        if (x % 2 == 0)
        {
            tile.transform.position = new Vector2((x - xOffset) * parentScale.x, (y - yOffset - tileHeight/2) * parentScale.y);

        }
        else
        {
            tile.transform.position = new Vector2((x - xOffset) * parentScale.x, (y - yOffset) * parentScale.y);
        }
        tile.transform.localScale = parentScale;
    }
}
