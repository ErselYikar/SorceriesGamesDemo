using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Movement Variables") ]
public class PlayerMovementVariables : ScriptableObject
{
    public Vector3 MovementSpeed; // In minion stack character only moves on Z AXIS
    public float JumpForce;
    public float GravityScale;
}
