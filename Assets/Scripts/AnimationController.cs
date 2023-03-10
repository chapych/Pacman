using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    protected Animator animator;
    private MovementController characterMovement;
    protected Vector2 velocity;
    [SerializeField] protected DataScriptableObject data;

    void Awake()
    {
        InitiateVariables();
    }

    protected virtual void InitiateVariables()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<MovementController>();
    }

    void Update()
    {
        this.velocity = characterMovement.velocity;
        AnimationUpdate();
    }

    protected virtual void AnimationUpdate()
    {
        animator.SetFloat("Speed", velocity.magnitude);
        animator.SetFloat("HorizontalSpeed", velocity.x);
        animator.SetFloat("VerticalSpeed", velocity.y);
    }

    

}
