using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IAging {
    Location location;

    public bool IsAlive()
    {
        return true;
    }

    public void Tick()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetLocation(Location newLocation)
    {
        location = newLocation;
    }
}
