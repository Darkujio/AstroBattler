using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private static string PrefabName = "Prefabs/Bullet";
    private static float BaseSpeed = 8f;
    //private Action UpdateAction;
    private BulletFactory Factory;

    public Bullet()
    {
        Object = GameObject.Instantiate(Resources.Load(PrefabName)) as GameObject;
    }

    public void SetPosition(Vector2 pos)
    {
        Object.transform.position = pos;
    }

    public void PushInDirection(Vector2 speedVector)
    {
        Speed = speedVector.normalized * BaseSpeed;
    }

    public void AddBaseSpeed(Vector3 speedVector)
    {
        Speed += speedVector;
    }

    public void Init(BulletFactory factory)
    {
        Factory = factory;
        Object.SetActive(true);
        Factory.FixedUpdateAction += FixedUpdate;
    }

    public override void FixedUpdate()
    {
        Object.transform.position += Speed*Time.fixedDeltaTime;
        Vector2 pos = Object.transform.position;
        foreach(var item in Game.Field.Asteroids.ActiveAsteroids)
        {
            if (Vector2.Distance(item.Position,pos) < item.Radius) 
            {
                item.HitBullet();
                Kill();
                break;
            }
        }
        if (pos.x > FieldSizeHorizontal || pos.y > FieldSizeVertical)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Factory.FixedUpdateAction -= FixedUpdate;
        Object.SetActive(false);
        Factory.ReturnBullet(this);
        Speed = Vector3.zero;
    }
}
