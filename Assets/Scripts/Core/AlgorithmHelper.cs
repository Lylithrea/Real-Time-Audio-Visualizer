using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tooling
{
    public static class AlgorithmHelper
    {

        public static float EaseSimple(float x, float effectiveness)
        {
            return (Mathf.Pow(x, 2 * (3 - 2 * x))) * effectiveness;
        }

        public static float Exp(float x, float effectiveness)
        {
            float value = Mathf.Exp(x * Mathf.Abs(effectiveness));
            if (effectiveness < 0)
            {
                value *= -1;
            }
            return value;
        }

        public static Vector3 AxisVector(Axis ax)
        {
            switch (ax)
            {
                case Axis.x:
                    return new Vector3(1, 0, 0);
                case Axis.y:
                    return new Vector3(0, 1, 0);
                case Axis.z:
                    return new Vector3(0, 0, 1);
                default:
                    Debug.LogWarning("Axis is not supported");
                    return new Vector3(0, 0, 0);
            }
        }

        public static void AddMethodToZone(Zones zone, System.Action method)
        {
            switch (zone)
            {
                case Zones.sub:
                    break;
                case Zones.bass:
                    break;
                case Zones.lowMid:
                    break;
                case Zones.mid:
                    break;
                case Zones.highmid:
                    break;
                case Zones.presence:
                    break;
                case Zones.brilliance:
                    break;
                default:
                    Debug.LogWarning("Zone is not supported.");
                    break;
            }
        }


    }

    public enum Zones
    {
        sub,
        bass,
        lowMid,
        mid,
        highmid,
        presence,
        brilliance
    }

    public enum Pass
    {
        low,
        mid,
        high,
        lowlow,
        lowmid,
        lowhigh,
        midlow,
        midmid,
        midhigh,
        highlow,
        highmid,
        highhigh
    }

    public enum Algorithm
    {
        Linear,
        Log,
        Exp,
        EaseSimple
    }

    public enum Axis
    {
        x,
        y,
        z
    }
}
