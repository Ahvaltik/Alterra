using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map {
    
    List<Location> aLocations;
    Dictionary<Location,List<Location>> dictRoads;

    void AddLocation(Location location)
    {
        aLocations.Add(location);
        dictRoads.Add(location, new List<Location>());
    }

    void AddTwoWayRoad(Location locationA, Location locationB)
    {
        AddOneWayRoad(locationA, locationB);
        AddOneWayRoad(locationB, locationA);
    }

    void AddOneWayRoad(Location locationA, Location locationB)
    {
        dictRoads[locationA].Add(locationB);
    }

    List<Location> GetNeighbourhood(Location location)
    {
        return dictRoads[location];
    }
}
