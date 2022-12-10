using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EnemyEntity
{
    private static string PrefabName = "Prefabs/Asteroid";
    private Vector2 Coordinates;
    private GameObject VisualsSmall;
    private GameObject VisualsBig;
    private bool Splittable = false;
    
    public void CreateGameObject()
    {
        Object = GameObject.Instantiate(Resources.Load(PrefabName)) as GameObject;
        VisualsBig =  Object.transform.Find("VisualsBig").gameObject;
        VisualsSmall = Object.transform.Find("VisualsSmall").gameObject;    
    }
    public void Init(ScriptableAsteroid asteroid, Vector2 pos)
    {
        Radius = asteroid.AsteroidRadius;
        Speed = RandomizeSpeed(asteroid.SpeedMin, asteroid.SpeedMax);
        Position = pos;
        Object.transform.position = Position;
        
        switch (Radius)
        {
            case 0.5f:
                VisualsBig.SetActive(true);
                Splittable = true;
                break;
            case 0.25f:
                VisualsSmall.SetActive(true);
                break;

        }
    }
    protected Vector3 RandomizeSpeed(float min, float max)
    {
        float randomAngle = Random.Range(0,360);
        Vector3 speed = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * Random.Range(min,max);
        return speed;
    }

    public override void FixedUpdate()
    {
        Object.transform.position = Object.transform.position + Speed*Time.deltaTime;
        Position = Object.transform.position;
        base.FixedUpdate();
    }
    public void HitBullet()
    {
        if (Splittable)
        {
            Game.Field.Asteroids.SplitAsteroid(this);
            Kill();
        }
        else
        {
            Game.Field.IncreaseScore();
            Kill();
        }
    }

    public void HitLaser()
    {
        if (!Splittable) Game.Field.IncreaseScore();
        else
        {
            for (int i = 0; i < 3; i++)
            {
                Game.Field.IncreaseScore();
            }
        }
        Kill();
    }

    public void Kill()
    {
        VisualsBig.SetActive(false);
        VisualsSmall.SetActive(false);
        Splittable = false;
        Game.Field.Asteroids.SetInactive(this);
    }
}
