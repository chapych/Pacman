using System.Collections;
using System.Collections.Generic;

public class NonRepeatableInARowStack<T> : IEnumerable<T>
{
    public readonly Stack<T> List;
    public T last;

    public NonRepeatableInARowStack()
    {
        List = new Stack<T>();
        last = default(T);
    }

    public NonRepeatableInARowStack(T element)
    {
        List = new Stack<T>();
        List.Push(element);
        last = element;
    }

    public void Add(T item)
    {
        if (!item.Equals(last))
        {
            List.Push(item);
            last = item;
        }
    }

    public T Pop()
    {
        return List.Pop();
    }

    public void Clear()
    {
        List.Clear();
    }
    public IEnumerator<T> GetEnumerator()
    {
        foreach(var item in List)  
            yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
