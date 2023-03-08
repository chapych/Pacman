using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MovementController
{

    protected float speed;
    private void Start()
    {
        speed = data.speed;
    }
    public void OnMove(InputValue inputValue)
    {
        Vector2 inputVector = inputValue.Get<Vector2>();
        if (inputVector.magnitude == 0) return;
        nextVelocity = speed * inputVector;
    }
}

    


