using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FigureDataScriptableObject", order = 1)]
public class DataScriptableObject : ScriptableObject
{
    public float speed;
    [SerializeField]
    public float cellSize;
    public float secondsToBeScary;
}
