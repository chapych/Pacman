using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    protected DataScriptableObject data;
    [SerializeReference]
    protected LayerMask walls;
    [HideInInspector]
    public Vector3 velocity;
    protected float cellSize;
    protected Rigidbody2D rb;
    protected Vector2 nextVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cellSize = data.cellSize;
    }

    private void FixedUpdate()
    {
        UpdateVelocity();
        if (!IsWall() && nextVelocity.magnitude != 0)
        {
            
           // Debug.Log(this.transform.position + " position");
            velocity = nextVelocity;
            nextVelocity = Vector3.zero;
            
        }
        Vector2 translation = velocity * Time.fixedDeltaTime;
        rb.MovePosition((Vector2)transform.position + translation);
    }
    protected bool IsWall()
    {
        
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * cellSize * 0.7f, 0, nextVelocity, cellSize, walls);
        return (raycastHit.collider != null);
    }

    protected virtual void UpdateVelocity()
    {

    }
}
