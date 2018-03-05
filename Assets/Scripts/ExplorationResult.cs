using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExplorationResult : IProbable
{
    //string message;
    public Rarity GetRarity()
    {
        return Rarity.Common;
    }
}
