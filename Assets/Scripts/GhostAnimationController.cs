using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAnimationController : AnimationController
{
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //to do get velocity
        AnimationUpdate();
    }
}
