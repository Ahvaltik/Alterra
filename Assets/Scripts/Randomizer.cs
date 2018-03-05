using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common = 1,
    Uncommon = 5,
    Rare = 10,
    Mythic = 20
}

public interface IProbable
{
    Rarity GetRarity();
}

public class Randomizer<T> where T: IProbable
{
    List<T> values;
    List<float> probabilities;
    float fMaxProbability;

    private Randomizer()
    {
        values = new List<T>();
        probabilities = new List<float>();
        fMaxProbability = 0;
    }

    private T GetRandom () {
        float fRandom = Random.Range(0.0f, fMaxProbability);
        float fProbability = 0;
        for(int idx = 0; idx < values.Count; idx++)
        {
            fProbability += probabilities[idx];
            if (fRandom <= fProbability)
                return values[idx];
        }
        return default(T);
    }

    private void AddItem(T item)
    {
        values.Add(item);
        float probability = 1.0f / (int)item.GetRarity();
        probabilities.Add(probability);
        fMaxProbability += probability;
    }

    private void AddItems(IList<T> items)
    {
        foreach(T item in items)
        {
            AddItem(item);
        }
    }
    
    public static T GetRandom(IList<T> items)
    {
        Randomizer<T> randomizer = new Randomizer<T>();
        randomizer.AddItems(items);
        return randomizer.GetRandom();
    }
}
