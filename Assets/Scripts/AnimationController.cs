using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    Vector2 velocity;
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        this.velocity = playerMovement.velocity;
        AnimationUpdate();
    }

    private void AnimationUpdate()
    {
        animator.SetFloat("Speed", velocity.magnitude);
        animator.SetFloat("HorizontalSpeed", velocity.x);
        animator.SetFloat("VerticalSpeed", velocity.y);
    }
}
