using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory
{
    private Stack<Bullet> InstantiatedBullets = new Stack<Bullet>();
    public Action FixedUpdateAction;
    public void FireBullet(Vector2 coordintaes, Vector2 angle, Vector3 baseSpeed)
    {
        Bullet bullet;
        if (InstantiatedBullets.Count != 0)
        {
            bullet = InstantiatedBullets.Pop();
        }
        else
        {
            bullet = new Bullet();
        }
        bullet.SetPosition(coordintaes);
        bullet.PushInDirection(angle);
        bullet.AddBaseSpeed(baseSpeed);
        bullet.Init(this);
    }

    public void FixedUpdate()
    {
        FixedUpdateAction?.Invoke();
    }

    public void ReturnBullet(Bullet bullet)
    {
        InstantiatedBullets.Push(bullet);
    }
}
