using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostMovement : MovementController
{
    public Tilemap tileMap;
    protected float cellSize;
    public Vector3 aimPoint;
    private NonRepeatableInARowQueue<Vector3> queue;


    private void Start()
    {
        aimPoint = tileMap.WorldToCell(aimPoint);
        Debug.Log(aimPoint);
        //queue = new NonRepeatableInARowQueue<Vector3>();
        //VelocityQueue();
        //nextVelocity = queue.Dequeue();
    }
    private void UpdateVelocity()
    {
        VelocityQueue();
        if (nextVelocity == Vector2.zero)
        {
            nextVelocity = queue.Dequeue();
        }

    }
    [ContextMenu("Test")]
    protected void VelocityQueue()
    {
        var path = GetPath(aimPoint);
        var previous = path.Value;
        foreach (var point in path)
        {
            Debug.Log(point);
            if (point == aimPoint)
                continue;
            queue.Add(previous - point);
            previous = point;
        }
        foreach (var i in queue)
            Debug.Log(i);
    }

    public void Test()
    {
        var solution = GetPath(aimPoint);
        if (solution == null)
            Debug.Log("NULL");
        else
        {
            foreach (var item in solution)
                Debug.Log(item);
        }
    }
    SingleLinkedList<Vector3Int> GetPath(Vector2 aimPointt)
    {
        var startPoint = tileMap.WorldToCell(this.transform.position);
        var aimPoint = tileMap.WorldToCell(aimPointt);
        var queue = new Queue<SingleLinkedList<Vector3Int>>();
        var visited = new HashSet<Vector3Int>();
        queue.Enqueue(new SingleLinkedList<Vector3Int>(startPoint));
        visited.Add(startPoint);
        while (queue.Count > 0)
        {
            var currentList = queue.Dequeue();

            if (currentList.Value == aimPoint) return currentList;
            foreach (Vector3Int item in NeighboursOf(currentList.Value)) //check if point is in box not necessary  because of double outter wall layer
                if (!tileMap.HasTile(tileMap.WorldToCell(item)))
                    if (!visited.Contains(item))
                    {
                        var newList = new SingleLinkedList<Vector3Int>(item, currentList);
                        visited.Add(item);
                        queue.Enqueue(newList);
                        Debug.DrawLine(currentList.Value, item, Color.red, 3);
                    }
        }
        return null;
    }

    IEnumerable NeighboursOf(Vector3Int point)
    {
        var currentX = point.x;
        var currentY = point.y;
        for (var shiftX = -cellSize; shiftX <= cellSize; shiftX += cellSize)
            for (var shiftY = -cellSize; shiftY <= cellSize; shiftY += cellSize)
                if (Math.Abs(shiftX) != Math.Abs(shiftY))
                    yield return new Vector3Int(currentX + (int)shiftX, currentY + (int)shiftY, 0);
    }
}

