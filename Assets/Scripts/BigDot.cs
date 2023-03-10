using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDot : Dot
{
    public event Action OnScaryGhosts = delegate { };

    public override void BeEaten(Vector3 position)
    {
        base.BeEaten(position);
        OnScaryGhosts();
    }
}
