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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speed = data.speed;
    }

    private void Update()
    {
        transform.position += Time.deltaTime * velocity;
    }

    public void OnMove(InputValue inputValue)
    {
       velocity = speed * inputValue.Get<Vector2>();
       Debug.Log(velocity);
       animator.SetFloat("Speed", velocity.magnitude);
       animator.SetFloat("HorizontalSpeed", velocity.x);
       animator.SetFloat("VerticalSpeed", velocity.y);
       
    }

   
}
