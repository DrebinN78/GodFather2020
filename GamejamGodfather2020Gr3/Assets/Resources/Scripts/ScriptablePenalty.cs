using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum effect
{
    Jump,
    Speed
}

[CreateAssetMenu(fileName = "New Penalty", menuName = "Penalty")]
public class ScriptablePenalty : ScriptableObject
{   
    public effect effectType = effect.Jump;
    [Range(0f, 2f)] public float valueModifier = 1;
}
