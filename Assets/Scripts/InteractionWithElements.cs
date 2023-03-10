using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class InteractionWithElements : MonoBehaviour
{
    public event Action<Vector3> OnBeEaten = x => { };
    //bool hasEntered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collectable = other.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.BeEaten(this.transform.position);
        }
        
    }

}
