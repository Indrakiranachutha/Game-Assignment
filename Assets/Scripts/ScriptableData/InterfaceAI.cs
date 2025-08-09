using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// Interface for AI components that can move.
public interface InterfaceAI
{
     /// Property that indicates whether the AI is currently moving.
    public bool isMoving { get; }

}