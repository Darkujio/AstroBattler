using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerManager : Entity
{
    private static GameObject Prefab = (Resources.Load("ScriptableObjects/Player") as ScriptablePlayer).PlayerPrefab;
    private static int MaxLaserCharges = (Resources.Load("ScriptableObjects/Player") as ScriptablePlayer).MaxLaserCharges;
    private static float MaxLaserCooldown = (Resources.Load("ScriptableObjects/Player") as ScriptablePlayer).MaxLaserCooldown;
    private static float StaticSpeedDecrease = (Resources.Load("ScriptableObjects/Player") as ScriptablePlayer).StaticSpeedDecrease;
    private static float Acceleration = (Resources.Load("ScriptableObjects/Player") as ScriptablePlayer).Acceleration;
    public int CurrentLaserCharges {get; private set;}
    public float LaserCooldown {get; private set;}
    public float Angle {get; private set;}
    public Vector3 AngleVector {get; private set;} = new Vector3(1,0,0);
    private BulletFactory Bullets;
    public void Init()
    {
        Game.Input.Fire += Fire;
        Game.Input.FireLaser += FireLaser;
        Game.Input.Rotate += Rotate;
        Game.Input.Move += Move;
        CurrentLaserCharges = MaxLaserCharges;
        Object = GameObject.Instantiate(Prefab);
        Bullets = new BulletFactory();
    }
    public override void FixedUpdate()
    {
        if (LaserCooldown != 0) 
        {
            LaserCooldown -= Time.fixedDeltaTime;
            if (LaserCooldown <=0) 
            {
                CurrentLaserCharges += 1;
                if (CurrentLaserCharges < MaxLaserCharges) LaserCooldown = MaxLaserCooldown;
                else LaserCooldown = 0;
            }
        }
        if (Speed != Vector3.zero)
        {
            Object.transform.position = Object.transform.position + Speed*Time.deltaTime;
            Speed = Speed - (Speed.normalized * (StaticSpeedDecrease * Time.deltaTime)) - (0.9f * Speed * Time.deltaTime);
            if (Speed.magnitude <= 0.01f) Speed = Vector3.zero;
            Position = Object.transform.position;
        }

        base.FixedUpdate();

        Bullets.FixedUpdate();
    }

    private void Rotate(int input)
    {
        Object.transform.Rotate(new Vector3(0,0,90*input*Time.deltaTime));
        Angle = Object.transform.rotation.eulerAngles.z;

        float radians = (Angle) * Mathf.Deg2Rad;
        AngleVector = new Vector3((float)Math.Cos(radians), (float)Math.Sin(radians), 0);
    }

    private void Move()
    {
        Speed += (AngleVector)*Time.deltaTime*Acceleration;
    }

    private void Fire()
    {
        Bullets.FireBullet(Position,AngleVector,Speed);
    }

    private void FireLaser()
    {
        if (CurrentLaserCharges > 0 )
        {
            CurrentLaserCharges -= 1;
            if (LaserCooldown == 0) LaserCooldown = MaxLaserCooldown;

            new Laser(Object, AngleVector);

            // List<Asteroid> items = new List<Asteroid>();
            // foreach(var item in Game.Field.Asteroids.ActiveAsteroids)
            // {
            //     if (CheckHit(item ,item.Position))
            //     {
            //         items.Add(item);
            //     }
            // }
            // HitLasers(items);
        }
    }

    // private bool CheckHit(Asteroid item,Vector2 position)
    // {
    //     float a = Vector2.Distance(Position,position);
    //     float b = Vector2.Distance(Position,Position+new Vector2(AngleVector.x,AngleVector.y));
    //     float c = Vector2.Distance(position,Position+new Vector2(AngleVector.x,AngleVector.y));
    //     float p = (a+b+c)/2;
    //     double distance = (2*Math.Sqrt(p*(p-a)*(p-b)*(p-c)))/b;
    //     List<Asteroid> items = new List<Asteroid>();
    //     if (distance < item.Radius+0.3f)
    //     {
    //         return true;
    //     }
    //     return false;
    // }

    // private void HitLasers(List<Asteroid> items)
    // {
    //     foreach (var item in items)
    //     {
    //         item.HitLaser();
    //     }
    // }
}
