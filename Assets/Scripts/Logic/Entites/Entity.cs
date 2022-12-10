using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public static float FieldSizeVertical = (Resources.Load("ScriptableObjects/Field") as ScriptableField).FieldSizeVertical;
    public static float FieldSizeHorizontal = (Resources.Load("ScriptableObjects/Field") as ScriptableField).FieldSizeHorizontal;
    public Vector3 Speed {get; protected set;}
    public Vector2 Position {get; protected set;}
    protected GameObject Object;
    public virtual void FixedUpdate()
    {   
        if (Math.Abs(Position.x)>FieldSizeHorizontal)
        {
            Position = new Vector2(-Position.x,Position.y);
            UpdatePosition();
        }
        if (Math.Abs(Position.y)>FieldSizeVertical)
        {
            Position = new Vector2(Position.x,-Position.y);
            UpdatePosition();
        }
    }
    private void UpdatePosition()
    {
        Object.transform.position = Position;
    }
}
