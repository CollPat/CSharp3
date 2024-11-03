IEnumerable<string> string = ["Prvni string", "Druhy string"];
IEnumerable<integers> integers = [10, 15, -1];
var stringoveIntegery = integers.Select(1=> 1.ToString);
WriteToConsole(string);
WriteToConsole(integers);

void WriteToConsole<T>(IEnurable<T> list) where T : IComparable<T>, IEquatable<T>
{
    foreach (var s in list)
    {
        WriteToConsole.Write(s.ToString() +" ");
        s.CompareTo
    }

    Console.WriteLine();
}

T Max<T>(T item1, item 2) where T : IComparable<T>
{
    return item1.CompareTo(item2) > 0 ? item1 : item2;
}

T MaxCollections<T>(IEnumerable<T> collections) where T : IComparable<T>
{
    if (collections == null || collections.Any)
    {
        return new ArgumentException("The collection is empty or null");
    }

    var max = collections.First();

    foreach (T item in collections.Skip(1))
    {
        if (item.item.CompareTo(max) > 0)
        {
            max = item;
        }

    }
    return max;
}

bool IsDistinct<T>(IEnumerable<T> collection)
{
    Hashset<T> = [];
    seen.Add
    
    return true;
}
