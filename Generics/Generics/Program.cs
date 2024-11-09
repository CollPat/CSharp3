IEnumerable<string> strings = ["Prvni string", "Druhy string"];
IEnumerable<integers> integers = [10, 15, -1];
var stringoveIntegery = integers.Select(1=> 1.ToString);
WriteToConsole(strings);
WriteToConsole(integers);

static void WriteToConsole<T>(IEnurable<T> list) where T : IComparable<T>, IEquatable<T>
{
    foreach (var s in list)
    {
        Console.Write(s.ToString() + " ");
    }

    Console.WriteLine();
}

static T Max<T>(T item1, T item2) where T : IComparable<T>
{
    return item1.CompareTo(item2) > 0 ? item1 : item2;
}

static T MaxCollections<T>(IEnumerable<T> collections) where T : IComparable<T>
{
    if (collections == null || !collections.Any())
    {
        throw new ArgumentException("The collection is empty or null");
    }

    var max = collections.First();

    foreach (T item in collections.Skip(1))
    {
        if (item.CompareTo(max) > 0)
        {
            max = item;
        }

    }
    return max;
}

static bool IsDistinct<T>(IEnumerable<T> collection)
{
    Hashset<T> seen = new Hashset<T>();
    foreach (var item in collection);
    {
        if(!seen.Add(item))
        {
            return false;
        }
    }

    return true;
}
