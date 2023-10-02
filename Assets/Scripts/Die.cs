using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public int diceSize;
    public int RollDice(int diceSize)
    {
        int roll = Random.Range(1, diceSize);
        return roll;
    }
}
