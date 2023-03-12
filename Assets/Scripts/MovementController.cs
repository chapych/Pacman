using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] protected DataScriptableObject data;
    [SerializeField] protected LayerMask walls;
    [HideInInspector] public Vector3 Velocity;
    protected float cellSize;
    protected Rigidbody2D rb;
    protected Vector3 nextVelocity;

    void Awake() => InitiateVariables();

    protected virtual void InitiateVariables()
    {
        rb = GetComponent<Rigidbody2D>();
        cellSize = data.cellSize;
    }

    private void FixedUpdate()
    {
        if (IsWall(Velocity, cellSize / 3))
        {
            Velocity = Vector3.zero;
            return;
        }

        UpdateVelocity();

        if (!IsWall(nextVelocity, cellSize) && nextVelocity.magnitude != 0)
        {
            Velocity = nextVelocity;
            nextVelocity = Vector3.zero;
        }
        Vector2 translation = Velocity * Time.fixedDeltaTime;
        rb.MovePosition((Vector2)transform.position + translation);
    }
    bool IsWall(Vector2 direction, float depth)
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * cellSize * 0.7f, 0, direction, depth, walls);
        return raycastHit.collider != null;
    }

    protected virtual void UpdateVelocity() {}
}
