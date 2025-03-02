using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class DungeonUtils
{
    public static Directions GetOpositeDoor(this Directions referenceDoor)
    {
        return referenceDoor switch
        {
            Directions.Up => Directions.Down,
            Directions.Down => Directions.Up,
            Directions.Left => Directions.Right,
            Directions.Right => Directions.Left,
            _ => throw new ArgumentException("Error on getting Oposite Door")
        };
    }

    public static T GetRandomElement<T>(this T[] element)
    {
        if (element.Length == 0) throw new Exception("element zero");

        return element[Random.Range(0, element.Length)];
    }

    public static T GetRandomElement<T>(this List<T> element)
    {
        if (element.Count == 0) throw new Exception("element zero");

        return element[Random.Range(0, element.Count)];
    }

    public static (T, int) GetRandomElementAndIndex<T>(this T[] element)
    {
        if (element.Length == 0) return default;
        var index = Random.Range(0, element.Length);
        return (element[index], index);
    }

    public static (T, int) GetRandomElementAndIndex<T>(this List<T> element)
    {
        if (element.Count == 0) return default;
        var index = Random.Range(0, element.Count);
        return (element[index], index);
    }
}