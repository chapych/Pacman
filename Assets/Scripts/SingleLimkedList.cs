using System.Collections;
using System.Collections.Generic;

public class SingleLinkedList<T> : IEnumerable<T>
{
    public readonly T Value;
    public readonly SingleLinkedList<T> Previous;
    public readonly int Length;

    public SingleLinkedList(T value, SingleLinkedList<T> previous = null)
    {
        Value = value;
        Previous = previous;
        Length = previous?.Length + 1 ?? 1;
    }

    public IEnumerator<T> GetEnumerator()
    {
        yield return Value;
        var pathItem = Previous;
        while (pathItem != null)
        {
            yield return pathItem.Value;
            pathItem = pathItem.Previous;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
