using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAging
{
    void Tick();
    bool IsAlive();
}

public class Time {
    static Time instance;

    static Time GetInstance()
    {
        return instance ?? (instance = new Time());
    }

    List<IAging> aAging;

    Time()
    {
        aAging = new List<IAging>();
    }

	void Tick()
    {
        foreach(IAging obj in aAging)
        {
            obj.Tick();
            if(!obj.IsAlive())
            {
                aAging.Remove(obj);
            }
        }
    }
	
	void AddAging(IAging aging)
    {
        aAging.Add(aging);
    }
}
