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

    protected Rigidbody2D rb;
    protected Vector2 nextVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!IsWall() && nextVelocity.magnitude != 0)
        {
            
            velocity = nextVelocity;
            nextVelocity = Vector3.zero;
        }
        Vector2 translation = velocity * Time.fixedDeltaTime;
        rb.MovePosition((Vector2)transform.position + translation);
    }
    protected bool IsWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * 3*data.cellSize * 0.9f, 0, nextVelocity, 3*data.cellSize, walls);
        return (raycastHit.collider != null);
    }
}
