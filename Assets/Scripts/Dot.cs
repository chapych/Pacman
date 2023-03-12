using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Dot : MonoBehaviour, ICollectable
{
    Tilemap tilemap;
     void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public virtual void BeEaten(Vector3 position)
    {
        tilemap.SetTile(tilemap.WorldToCell(position), null);
        //to-do add bonus
    }

}
