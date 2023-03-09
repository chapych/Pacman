using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    private MovementController playerMovement;
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.velocity = playerMovement.velocity;
        AnimationUpdate();
    }

}
