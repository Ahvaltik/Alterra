using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Location : IAging
{
    List<ExplorationResult> aPossibleExplorationResults;
    List<Happening> aPossibleHappenings;
    Happening happening;


    public ExplorationResult Explore()
    {
        return Randomizer<ExplorationResult>.GetRandom(aPossibleExplorationResults);
    }

    public bool IsAlive()
    {
        return true;
    }

    public void Tick()
    {
        happening = Randomizer<Happening>.GetRandom(aPossibleHappenings);
    }
}
