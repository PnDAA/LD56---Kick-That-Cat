using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtension
{
    public static T TakeOneRandom<T>(this IEnumerable<T> enumerable)
    {
        var list = enumerable.ToList();
        if (list.Count == 0)
            return default;
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    // copy paste of the TakeOnRandom above
    // in case you want to use a specific random (If it have a special seed)
    public static T TakeOneRandom<T>(this IEnumerable<T> enumerable, System.Random random)
    {
        if (random == null)
            return TakeOneRandom(enumerable);

        var list = enumerable.ToList();
        if (list.Count == 0)
            return default(T);
        int randomIndex = random.Next(list.Count);
        return list[randomIndex];
    }
}
