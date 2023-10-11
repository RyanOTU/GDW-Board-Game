using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int troopPool = 20;
    [SerializeField] public Tile.PlayerNumber playerIndex;
    private GameObject[] troops;
}
