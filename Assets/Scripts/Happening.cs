using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Happening : IProbable
{
    public Rarity GetRarity()
    {
        return Rarity.Common;
    }
}
