using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CharacterSO : ScriptableObject
{
    [Header("Info")]
    public float hp;
    public float mp;
    public float attackPower;
    public float speed;
}
