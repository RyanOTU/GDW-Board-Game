using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] public int troopPool = 20;
    [SerializeField] public Tile.PlayerNumber playerIndex;
    [SerializeField] public TMP_Text textBox;
    private GameObject[] troops;

    private void Update()
    {
        textBox.text = "You have " + troopPool + " troops left";
    }
}
