using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    

    void Awake()
    {
        
    }
    public void ObtainItemHandler()
    {
        Debug.Log("smth");
        Destroy(this);
    }

}
