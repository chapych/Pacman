using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkGhostMovement : GhostMovement
{
    private int multiscale = 4;
    protected override void DefineAimPoint()
    {
        var pacmanPosition = Pacman.transform.position;
        var shift = Pacman.GetComponent<MovementController>().velocity.normalized;
        aimPoint = tileMap.WorldToCell(Pacman.transform.position + multiscale * shift);
    }
}
