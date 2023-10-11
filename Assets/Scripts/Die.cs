using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die
{
    public bool rolled = false;
    public int RollDice(int diceSize)
    {
        int roll = Random.Range(1, diceSize + 1);
        rolled = true;
        return roll;
    }
}
