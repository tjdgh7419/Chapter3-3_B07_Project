using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    float hp = 1000f;  
    public void TakeDamage(float damage)
    {
        hp -= damage;
    }
}
