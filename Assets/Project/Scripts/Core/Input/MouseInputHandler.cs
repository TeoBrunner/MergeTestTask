using System;
using UnityEngine;
using Zenject;

public class MouseInputHandler : IInputHandler, ITickable
{
    public event Action<Vector2> OnPointerDown;
    public event Action<Vector2> OnPointerDrag;
    public event Action<Vector2> OnPointerUp;

    public bool IsPointerDown { get; private set; }
    public Vector2 CurrentPointerPosition => Input.mousePosition;

    public void Tick()
    {
        Vector2 mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            IsPointerDown = true;
            OnPointerDown?.Invoke(mousePos);
        }

        if (Input.GetMouseButton(0) && IsPointerDown)
        {
        
            OnPointerDrag?.Invoke(mousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsPointerDown = false;
            OnPointerUp?.Invoke(mousePos);
        }
    }
}