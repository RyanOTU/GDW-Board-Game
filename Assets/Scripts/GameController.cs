using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject troop;
    public TMP_Text text;
    public TMP_Text cursorText;
    private Tile grabbedTroopTile = null;
    public Transform cursor;

    private Die dice = new Die();
    public int diceSize = 10;

    public int currentPlayer = 0;
    public int turnActions = 0;

    private void Start()
    {
        text.text = "Player 1, press 'Space' to roll the dice!";
    }
    private void Update()
    {
        Vector2 pos = gridManager.GetFromWorldClamped(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (pos.x < 0) return;
        Tile curTile = gridManager.GetTile((int)pos.x, (int)pos.y);
        cursor.position = GridManager.GetFromCoordinate((int)pos.x, (int)pos.y);
        cursorText.text = grabbedTroopTile ?
        ((int)Mathf.Round(Vector2.Distance(grabbedTroopTile.transform.position, curTile.transform.position) / GridManager.tileWidth * 1.02f)).ToString():"";

        if (dice.rolled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (curTile.isSpawnTile())
                {
                    if (curTile.PlayerIndex == (Tile.PlayerNumber)currentPlayer)
                    {
                        if (grabbedTroopTile)
                        {
                            TryMoveOnTile(curTile);
                        }
                        else
                        {
                            TrySpawnOnTile(curTile);
                        }
                    }
                }
                else
                {
                    if (grabbedTroopTile)
                    {
                        TryMoveOnTile(curTile);
                    }
                    else if (curTile.isOccupied)
                    {
                        TryGrabTroop(curTile);
                    }
                }

                if (turnActions <= 0)
                {
                    currentPlayer = (currentPlayer + 1) % gridManager.players.Length;
                    dice.rolled = false;
                    text.text = "Player " + (currentPlayer + 1) + ", press 'Space' to roll the dice!";
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            turnActions = dice.RollDice(diceSize);
            text.text = "Player " + (currentPlayer + 1) + " rolled a " + turnActions;
        }
    }
    public void TryGrabTroop(Tile curTile)
    {
        if (curTile.troop.playerIndex != (Tile.PlayerNumber)currentPlayer) return;
        grabbedTroopTile = curTile;
        curTile.setIsOccupied(false);
    }
    public void TrySpawnOnTile(Tile curTile)
    {
        if (!curTile.isOccupied)
        {
            if (gridManager.players[currentPlayer].troopPool > 0)
            {
                gridManager.players[currentPlayer].troopPool -= 1;
                turnActions -= 1;
                curTile.setIsOccupied(true);
                curTile.troop = Instantiate(troop, curTile.transform.position, Quaternion.identity).GetComponent<Troop>();
                curTile.troop.playerIndex = (Tile.PlayerNumber)currentPlayer;
                curTile.troop.GetComponent<SpriteRenderer>().color = curTile.playerColor[currentPlayer] + new Color(0.1f, 0.1f, 0.1f, 1f); //FIX LATER THIS IS JANK & ATROCIOUS
                                                                                                                                           //Debug.Log("Spawned troop")
            }
        }
        else
        {
            TryGrabTroop(curTile);
        }
    }
    public void TryMoveOnTile(Tile curTile)
    {
        if (curTile == grabbedTroopTile)
        {
            grabbedTroopTile.setIsOccupied(true);
            grabbedTroopTile = null;
            return;
        }
        float radius = Vector2.Distance(grabbedTroopTile.transform.position, curTile.transform.position);
        int resRadius = (int)Mathf.Round(radius / GridManager.tileWidth * 1.02f);

        if (curTile.isOccupied)
        {
            if (curTile.troop.playerIndex == (Tile.PlayerNumber)currentPlayer)
            {
                return;   
            }
            if (resRadius > turnActions + 1) //On success
            {
                return;
            }
            Destroy(curTile.troop.gameObject);
            curTile.troop = null;
            curTile.setIsOccupied(false);
        }
        if (resRadius <= turnActions)
        {
            grabbedTroopTile.troop.targetPos = curTile.transform.position;
            curTile.setIsOccupied(true);
            curTile.troop = grabbedTroopTile.troop;
            grabbedTroopTile.troop = null;
            grabbedTroopTile = null;
            turnActions -= resRadius;
        }
    text.text = "You have " + turnActions + " actions left!";
    }
}
