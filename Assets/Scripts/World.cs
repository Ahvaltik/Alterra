using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum TerrainType{
//    Water,
//    Fields,
//    Swamp,
//    Forest,
//    Mountain
//}

public class World{
    static World instance;
    
    public static World GetInstance()
    {
        return instance ?? (instance = new World());
    }
    
    Vector2 worldSize;
    
    int fieldRows, fieldColumns;
    int pixelsPerField;
    
    Vector3[,] aaMapMarkers;
    TerrainType[,] aaTerrainType;

    Func<float, Color> funMapColorFunction;

    private World()
    {
        fieldRows = 64;
        fieldColumns = 64;

        pixelsPerField = 16;

        float[,] aafHeights = new float[fieldRows, fieldColumns];

        worldSize = new Vector2(fieldRows * pixelsPerField, fieldColumns * pixelsPerField);
        aaMapMarkers = new Vector3[fieldRows, fieldColumns];
        for (int x = 0; x < fieldRows; x++)
            for (int y = 0; y < fieldColumns; y++)
            {
                Vector2 point = new Vector2(
                        UnityEngine.Random.Range(x * pixelsPerField, (x + 1) * pixelsPerField),
                        UnityEngine.Random.Range(y * pixelsPerField, (y + 1) * pixelsPerField)
                    );
                float distance = Vector2.Distance(point, worldSize / 2);
                float k = Mathf.Cos(distance * 4 / Vector2.Distance(Vector2.zero, worldSize));
                aaMapMarkers[x, y] =
                    new Vector3(
                        point.x,
                        point.y,
                        UnityEngine.Random.Range(k - 0.75f, k - 0.25f)
                        //UnityEngine.Random.Range(- 1.0f, 1.0f)
                    );
            }
        /*
        for (int x = 0; x < fieldRows; x++)
            for (int y = 0; y < fieldColumns; y++)
            {
                List<Vector3> neighborhood = GetMapMarkersNear(aaMapMarkers[x, y]);
                //aafHeights[x, y] = aaMapMarkers[x, y].z * (9/8);
                aafHeights[x, y] = aaMapMarkers[x, y].z * (23 / 32);

                for (int x1 = Math.Max(x - 1, 0); x1 <= Math.Min(x + 1, fieldRows - 1); x1++)
                    for (int y1 = Math.Max(y - 1, 0); y1 <= Math.Min(y + 1, fieldColumns - 1); y1++)
                    {
                        //aafHeights[x, y] -= aaMapMarkers[x1, y1].z / 8;
                        aafHeights[x, y] += aaMapMarkers[x1, y1].z / 32;
                    }
            }

        for (int x = 0; x < fieldRows; x++)
            for (int y = 0; y < fieldColumns; y++)
                aaMapMarkers[x, y].z = aafHeights[x, y];
        */
        Color brown = Color.red / 3 + Color.green / 6 + Color.black / 2;
        Color darkBlue = Color.black / 2 + Color.blue / 2;
        Color darkGreen = darkBlue / 2 + Color.green / 2;
        Color bleakYellow = Color.gray / 2 + Color.yellow / 2;
        Dictionary<float, Color> dictColors = new Dictionary<float, Color>
        {
            { -1.0f   , darkBlue    },
            { -0.001f , Color.blue  },
            { 0.0f    , Color.white },
            { 0.001f  , bleakYellow },
            { 0.1f    , Color.gray  },
            { 0.11f   , darkGreen   },
            { 1.0f    , brown       }
        };
        funMapColorFunction = ColorMixer.ColorFuncion(dictColors);
    }

    public Vector2 GetWorldSize()
    {
        return worldSize;
    }

    public int GetMapMarkersNumber()
    {
        return fieldRows * fieldColumns;
    }

    public float GetNeighborhoodSize()
    {
        return pixelsPerField;
    }

    public List<Vector3> GetMapMarkers()
    {
        List<Vector3> result = new List<Vector3>();
        for (int x =  0; x < fieldRows; x++)
            for (int y = 0; y < fieldColumns; y++)
            {
                result.Add(aaMapMarkers[x, y]);
            }
        return result;
    }

    public List<Vector3> GetMapMarkersNear(Vector2 point, int nLevel = 1)
    {
        int row, column;
        row = Mathf.FloorToInt(point.x / pixelsPerField);
        column = Mathf.FloorToInt(point.y / pixelsPerField);
        List<Vector3> result = new List<Vector3>();
        for (int x = Math.Max(row - nLevel, 0); x <= Math.Min(row + nLevel, fieldRows - 1); x++)
            for (int y = Math.Max(column - nLevel, 0); y <= Math.Min(column + nLevel, fieldColumns - 1); y++)
            {
                result.Add(aaMapMarkers[x, y]);
            }
        return result;
    }

    public Color GetMapColor(Vector2 point)
    {
        List<Vector3> nearest = GetMapMarkersNear(point); //mapMarkers.FindAll(near => Vector3.Distance(point, near) < world.GetNeighborhoodSize());
                                                                //List<Vector3> nearest = world.GetMapMarkersNear(point, 3).FindAll(near => Vector3.Distance(point, near) < world.GetNeighborhoodSize());
        float fSumDistance = 0;
        float fHeight = 0;
        nearest.Sort((near1, near2) => Vector3.Distance(point, near1).CompareTo(Vector3.Distance(point, near2)));
        foreach (Vector3 near in nearest.GetRange(0, 2))
        //foreach (Vector3 near in nearest)
        {
            fSumDistance += Vector3.Distance(point, near);
            fHeight += near.z * Vector3.Distance(point, near);
        }
        fHeight /= fSumDistance;
        if (nearest.Count == 0)
            fHeight = 0.0f;
        
        return funMapColorFunction(fHeight);
        
    }
}
