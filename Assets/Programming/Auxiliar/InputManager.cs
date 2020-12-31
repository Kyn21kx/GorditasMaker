using System;
using UnityEngine;

public abstract class InputManager {

    public static Vector2 MovementInput { get { return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); } }
    public static Vector2 RotationInput { get { return new Vector2(Input.GetAxis("KeysHorizontal"), Input.GetAxis("KeysVertical")); } }

}
