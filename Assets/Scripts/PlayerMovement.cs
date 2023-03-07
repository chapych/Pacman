using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private DataScriptableObject data;
    [SerializeReference]
    private LayerMask walls;
    [HideInInspector]
    public Vector3 velocity;

    private float speed;
    private Rigidbody2D rb;
    private Vector2 nextVelocity;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = data.speed;
    }

    private void FixedUpdate()
    {
        if(!IsWall() && nextVelocity.magnitude!=0)
        {
            velocity = nextVelocity;
            nextVelocity = Vector2.zero;
        }
        Vector2 translation = velocity * Time.fixedDeltaTime;
        rb.MovePosition((Vector2)transform.position + translation);
        
    }

    public void OnMove(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        if (inputVector.magnitude == 0) return;
        nextVelocity = speed * inputVector;
    }

    public bool IsWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, Vector2.one * data.cellSize*0.9f, 0, nextVelocity, data.cellSize, walls);
        return (raycastHit.collider != null);
    }

    



}
