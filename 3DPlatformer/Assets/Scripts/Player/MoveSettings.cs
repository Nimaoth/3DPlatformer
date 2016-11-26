using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


[System.Serializable]
public class MoveSettings
{
    public float RunVelocity = 12;
    public float RotateVelocity = 100;
    public float JumpVelocity = 8;
    public float DistanceToGround = 1.3f;
    public LayerMask Ground;
}