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
    private float threshold = 0.1f; //for IsMoving() Method
    private Vector2 previousPosition;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        speed = data.speed;
    }

   

    private void FixedUpdate()
    {
        rb.velocity = velocity;
        if (!IsMoving()) animator.SetFloat("Speed", 0);
        else animator.SetFloat("Speed", velocity.magnitude);
    }

    private void Update()
    {
       
    }

    public void OnMove(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        if (inputVector.magnitude == 0) return;
        velocity = speed * inputVector;
        animator.SetFloat("HorizontalSpeed", velocity.x);
        animator.SetFloat("VerticalSpeed", velocity.y);
    }

    private bool IsMoving()
    {
        Vector2 currentPosition = transform.position;
        Debug.Log(currentPosition + " "+ previousPosition);
        if ((previousPosition - currentPosition).magnitude > 0) isMoving = true;
        else isMoving = false;
        Debug.Log((previousPosition - currentPosition).magnitude);
        previousPosition = currentPosition;
        return isMoving;
    }



}
