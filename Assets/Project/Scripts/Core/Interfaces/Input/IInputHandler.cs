using System;
using UnityEngine;

public interface IInputHandler
{
    event Action<Vector2> OnPointerDown;
    event Action<Vector2> OnPointerDrag;
    event Action<Vector2> OnPointerUp;

    bool IsPointerDown { get; }
    Vector2 CurrentPointerPosition { get; }
}