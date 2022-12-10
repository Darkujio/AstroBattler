using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    private static float PlayerHitRadius = (Resources.Load("ScriptableObjects/Player") as ScriptablePlayer).PlayerHitRadius;
    public float Radius {get; protected set;}
    public override void FixedUpdate()
    {   
        var playerPos = Game.Player.Position;
        if (Vector2.Distance(playerPos,Position) < Radius+PlayerHitRadius)
        {
            Game.Field.OnLoseGame();
        }
        base.FixedUpdate();
    }
}
