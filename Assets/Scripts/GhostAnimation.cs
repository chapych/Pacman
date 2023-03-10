using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimation : AnimationController
{
    bool isScaryMode = false;
    float secondsToBeScary;
    [SerializeField] private BigDot bigDot;
    private GhostMovement ghostMovement;

    protected override void InitiateVariables()
    {
        base.InitiateVariables();
        ghostMovement = GetComponent<GhostMovement>();
        Debug.Log(ghostMovement);
    }

    protected override void AnimationUpdate()
    {
        base.AnimationUpdate();
        animator.SetBool("IsBigDotEaten", ghostMovement.isScaryModeOn);
    }

    

    
}
