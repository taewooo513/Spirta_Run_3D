using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Scriptable", order = 1)]
public class Scriptable : ScriptableObject
{
    public string name;
    public string description;
}

