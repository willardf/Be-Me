using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum DragState
{
    NoTouch,
    TouchStart,
    Dragging,
    Released
}

public class Controller
{
    public static bool HasTouch { get { return Input.GetMouseButton(0) || Input.touchCount == 1; } }

    public static Vector2 Position
    {
        get { return Camera.main.ScreenToWorldPoint(RawPosition); }
    }

    public static Vector2 RawPosition
    {
        get { return (Input.touchCount == 1) ? Input.GetTouch(0).position : (Vector2)Input.mousePosition; }
    }

    public bool TouchDown { get; private set; }
    public bool TouchUp { get; private set; }

    private bool amTouching;
    private bool hadTouch;

    public DragState CheckDrag(Collider2D coll)
    {
        if (!amTouching && HasTouch && coll.OverlapPoint(Position))
        {
            amTouching = true;
            return DragState.TouchStart;
        }
        else if (amTouching)
        {
            if (HasTouch)
            {
                return DragState.Dragging;
            }

            amTouching = false;
            return DragState.Released;
        }
        
        return DragState.NoTouch;
    }
    
    public void Update()
    {
        this.TouchDown = !this.hadTouch && HasTouch;
        this.TouchUp = this.hadTouch && !HasTouch;
        this.hadTouch = HasTouch;
    }
    
    public void Reset()
    {
        this.TouchDown = false;
        this.TouchUp = false;
        this.hadTouch = false;
        this.amTouching = false;
    }
}