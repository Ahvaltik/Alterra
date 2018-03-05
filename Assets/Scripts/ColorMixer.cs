using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMixer {

    public static Color MixColor(Color a, Color b, float f)
    {
        f = Mathf.Clamp01(f);
        return a * f + b * (1.0f - f);
    }

    public static Func<float, Color> ColorFuncion(Dictionary<float, Color> dictColors)
    {
        return (float f) => FindInDictionary(dictColors, f);
    }

    public static Color FindInDictionary(Dictionary<float, Color> dictColors, float f)
    {
        List<float> afJoints = new List<float>(dictColors.Keys);
        if (afJoints.Count == 0)
            return Color.black;
        if (afJoints.Count == 1)
            return dictColors[afJoints[0]];
        afJoints.Sort();
        if (f < afJoints[0])
            return dictColors[afJoints[0]];
        for (int idx = 0; idx < afJoints.Count - 1; idx++)
        {
            if (f < afJoints[idx + 1])
                return MixColor(dictColors[afJoints[idx + 1]], dictColors[afJoints[idx]], (f - afJoints[idx]) / (afJoints[idx + 1] - afJoints[idx]));
        }
        return dictColors[afJoints[-1]];
    }
}
