using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class InteractionWithElements : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dots"))
        {
            tilemap.SetTile(tilemap.WorldToCell(this.transform.position), null);
        }
    }


}
