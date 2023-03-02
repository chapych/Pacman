using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public DataScriptableObject data;
    private Animator animator;
    private float speed;
    private Vector3 velocity;
    private Rigidbody2D rb;
    private bool IsNearWall = false;
    public float threshold = 0.8f; //for IsMoving() Method
    private Vector2 previousPosition;
    private Vector2 nextVelocity;
    public LayerMask walls;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        AnimationUpdate();
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

    private void AnimationUpdate()
    {
        animator.SetFloat("Speed", velocity.magnitude);
        animator.SetFloat("HorizontalSpeed", velocity.x);
        animator.SetFloat("VerticalSpeed", velocity.y);

    }



}
