using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostMovement : MovementController
{
    public Tilemap tileMap;
    public Vector3 aimPoint;
    private NonRepeatableInARowStack<Vector3> stack;
    float speed;
    public GameObject Pacman;
    [SerializeField] private BigDot bigDot;
    public bool isScaryModeOn;
    float secondsToBeScary;
    Coroutine coroutine;

    protected override void InitiateVariables()
    {
        base.InitiateVariables();
        bigDot.OnScaryGhosts += OnScaryGhostsHandler;
        speed = data.speed;
        stack = new NonRepeatableInARowStack<Vector3>();
        isScaryModeOn = false;
        secondsToBeScary = data.secondsToBeScary;
    }
    protected override void UpdateVelocity()
    {
        //if (isScaryModeOn)
        //{
        //    nextVelocity = speed * RandomVelocity();
        //    return;
        //}
        DefineAimPoint();
        UpdateVelocityQueue();
        if (stack.List.Count == 0)
        {
            nextVelocity = Vector2.zero;
            return;
        }
         nextVelocity = speed * stack.Pop();
    }
    protected void UpdateVelocityQueue()
    {
        stack.Clear();
        var path = GetPath(aimPoint);
        if (path == null) return;
        var previous = path.Value;
        foreach (var point in path)
        {
            if (point == aimPoint)
                continue;
            stack.Add(previous - point);
            previous = point;
        }
    }
    
    SingleLinkedList<Vector3> GetPath(Vector3 aimPoint)
    {
        var startPoint = tileMap.WorldToCell(this.transform.position) + Vector3.one * cellSize/2;
        var queue = new Queue<SingleLinkedList<Vector3>>();
        var visited = new HashSet<Vector3>();
        queue.Enqueue(new SingleLinkedList<Vector3>(startPoint));
        visited.Add(startPoint);
        while (queue.Count > 0)
        {
            var currentList = queue.Dequeue();
            if (tileMap.WorldToCell(currentList.Value) == aimPoint) return currentList;
            foreach (Vector3 item in NeighboursOf(currentList.Value)) //check if point is in box not necessary  because of double outter wall layer
                if (!tileMap.HasTile(tileMap.WorldToCell(item)))
                    if (!visited.Contains(item))
                    {
                        var newList = new SingleLinkedList<Vector3>(item, currentList);
                        visited.Add(item);
                        queue.Enqueue(newList);
                        Debug.DrawLine(currentList.Value, item, Color.red, 3);
                    }
        }
        return null;
    }

    IEnumerable NeighboursOf(Vector3 point)
    {
        var currentX = point.x;
        var currentY = point.y;
        for (var shiftX = -cellSize; shiftX <= cellSize; shiftX += cellSize)
            for (var shiftY = -cellSize; shiftY <= cellSize; shiftY += cellSize)
                if (Math.Abs(shiftX) != Math.Abs(shiftY))
                    yield return new Vector3(currentX + (int)shiftX, currentY + (int)shiftY, 0);
    }

    public void OnScaryGhostsHandler()
    {
        isScaryModeOn = true;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ScaryMode());
    }


    protected virtual void DefineAimPoint()
    {
        //    if (isScaryModeOn)
        //    {
        //        var xCoordinate = UnityEngine.Random.Range(-2, 2);
        //        var yCoordinate = UnityEngine.Random.Range(-2, 2);
        //        aimPoint = new Vector3(xCoordinate, yCoordinate, 0);
        //        Debug.Log(aimPoint);
        //    }
    }

    public Vector3 RandomVelocity()
    {
        var xCoordinate = UnityEngine.Random.Range(-1, 2);
        var yCoordinate = UnityEngine.Random.Range(-1, 2);
        return new Vector3(xCoordinate, yCoordinate);
    }

    IEnumerator ScaryMode()
    {
        yield return new WaitForSeconds(secondsToBeScary);
        isScaryModeOn = false;
    }
}

