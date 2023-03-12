using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostMovement : MovementController
{
    public Tilemap tileMap;
    public Vector3 aimPoint;
    float speed;
    public GameObject Pacman;
    [SerializeField] private BigDot bigDot;
    public bool isScaryModeOn;
    float secondsToBeScary;
    Coroutine coroutine;
    SingleLinkedList<Vector3> path;

    protected override void InitiateVariables()
    {
        base.InitiateVariables();
        bigDot.OnScaryGhosts += OnScaryGhostsHandler;
        speed = data.speed;
        isScaryModeOn = false;
        secondsToBeScary = data.secondsToBeScary;
        path = new SingleLinkedList<Vector3>(this.transform.position);
    }

    
    protected override void UpdateVelocity()
    {
        Debug.Log("---");
        foreach(var el in path){
            Debug.Log(el);
        }
        DefineAimPoint();
        UpdateVelocityQueue();
        if (path.Length <= 1)
        {
            nextVelocity = Vector2.zero;
            return;
        }
        Vector3 nextPosition = path.Pop();
         nextVelocity = speed * (nextPosition - this.transform.position);
         Debug.Log(this.transform.position + " " + nextPosition);
    }
    protected void UpdateVelocityQueue()
    {
        var pathStart = BreadthFirstSearch(this.transform.position, path.Value);
        var pathEnd = BreadthFirstSearch(path.Value, aimPoint);
        Debug.Log("PathStart" + pathStart);
        path.Previous = pathEnd;
        pathStart.Previous = path;
        path = pathStart;
        if (path == null) return; //nextVelocity = null;
    }
    
    SingleLinkedList<Vector3> BreadthFirstSearch(Vector3 endOfPreviousPath, Vector3 aim) //at 1st iteration end..Path = this.transform.position
    {
        endOfPreviousPath = tileMap.WorldToCell(endOfPreviousPath);
        
        var start = tileMap.WorldToCell(aim);
        var queue = new Queue<SingleLinkedList<Vector3>>();
        var visited = new HashSet<Vector3>();

        queue.Enqueue(new SingleLinkedList<Vector3>(start));
        visited.Add(start);
        while (queue.Count > 0)
        {
            var currentList = queue.Dequeue();
            var current = currentList.Value;
            foreach (Vector3 item in NeighboursOf(current)) //check if point is in box not necessary  because of double outter wall layer
                if (!tileMap.HasTile(tileMap.WorldToCell(item)))
                    if (!visited.Contains(item))
                    {
                        if (tileMap.WorldToCell(item) == endOfPreviousPath) return currentList;  //mb its not neccesary to transfrom it into  cell coordinate system
                        var newList = new SingleLinkedList<Vector3>(item, currentList);
                        visited.Add(item);
                        queue.Enqueue(newList);
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
    protected virtual void DefineAimPoint(){}
    IEnumerator ScaryMode()
    {
        yield return new WaitForSeconds(secondsToBeScary);
        isScaryModeOn = false;
    }
}

