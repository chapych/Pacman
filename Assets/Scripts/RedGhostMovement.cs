using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGhostMovement : GhostMovement
{
    protected override void DefineAimPoint()
    {
        aimPoint = tileMap.WorldToCell(Pacman.transform.position);
    }
}
