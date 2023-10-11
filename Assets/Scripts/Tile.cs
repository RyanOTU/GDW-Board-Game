using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;
    public Troop troop;

    public enum PlayerNumber
    {
        Player1,
        Player2,
        Player3,
        Player4,
        None
    }
    public PlayerNumber PlayerIndex = PlayerNumber.None;
    public Color[] playerColor = {
        Color.red,
        Color.blue,
        Color.yellow,
        Color.green
    };

    public void SetPlayerIndex(PlayerNumber num)
    {
        PlayerIndex = num;
        GetComponent<SpriteRenderer>().color = playerColor[(int)num];
    }

    public bool isSpawnTile()
    {
        return PlayerIndex != PlayerNumber.None;
    }
    public void setIsOccupied(bool b)
    {
        if (isOccupied == b) return;
        isOccupied = b;
        //Multiply Colour by .5 when you add a troop, and multiply by 2 when a troop is added
        GetComponent<SpriteRenderer>().color *= isOccupied ? 0.5f : 2f;
    }
}
