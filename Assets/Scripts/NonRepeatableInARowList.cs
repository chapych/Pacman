using System.Collections;
using System.Collections.Generic;

public class NonRepeatableInARowQueue<T> : IEnumerable<T>
{
    public readonly Queue<T> List;
    private readonly T last;

    public NonRepeatableInARowQueue()
    {
        List = new Queue<T>();
        last = default(T);
    }

    public NonRepeatableInARowQueue(T element)
    {
        List = new Queue<T>();
        List.Enqueue(element);
        last = element;
    }

    public void Add(T item)
    {
        if(!item.Equals(last))
            List.Enqueue(item);
    }

    public T Dequeue()
    {
        return List.Dequeue();
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
