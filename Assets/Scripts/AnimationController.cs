using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    protected Animator animator;
    protected Vector2 velocity;
    protected void AnimationUpdate()
    {
        animator.SetFloat("Speed", velocity.magnitude);
        animator.SetFloat("HorizontalSpeed", velocity.x);
        animator.SetFloat("VerticalSpeed", velocity.y);
    }
}
