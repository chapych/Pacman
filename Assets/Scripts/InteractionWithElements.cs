using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class InteractionWithElements : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private LayerMask dotsLayer;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ICollectable collectableItem = collision.collider.gameObject.GetComponent<ICollectable>();
        Debug.Log(collision.collider.gameObject.layer);
        if (collision.collider.gameObject.layer == dotsLayer)
        {
            Vector2 hitPosition = Vector2.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                Debug.Log(hitPosition);
            }
        }
    }


}
