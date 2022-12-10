using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Laser
{
    private static GameObject Prefab = (Resources.Load("ScriptableObjects/Laser") as ScriptableLaser).LaserPrefab;
    private static float Radius = (Resources.Load("ScriptableObjects/Laser") as ScriptableLaser).LaserRadius;
    private static float FadeTime = (Resources.Load("ScriptableObjects/Laser") as ScriptableLaser).LaserFadeTime;
    private GameObject Object;
    private Vector2 Position;
    private Vector2 AngleVector;
    public Laser (GameObject player, Vector2 angleVector)
    {
        Object = GameObject.Instantiate(Prefab, player.transform.position, player.transform.rotation);
        AngleVector = angleVector;
        Position = Object.transform.position;
        HitTargets();
        KillAfterDelay();
    }

    private void HitTargets()
    {
        List<Asteroid> items = new List<Asteroid>();
        foreach(var item in Game.Field.Asteroids.ActiveAsteroids)
        {
            if (CheckHit(item ,item.Position))
            {
                items.Add(item);
            }
        }
        HitLasers(items);
    }
    private bool CheckHit(Asteroid item,Vector2 position)
    {
        float a = Vector2.Distance(Position,position);
        float b = Vector2.Distance(Position,Position+(new Vector2(AngleVector.x,AngleVector.y))*12f);
        float c = Vector2.Distance(position,Position+(new Vector2(AngleVector.x,AngleVector.y))*12f);
        
        float cos =  ((b*b) + (a*a) - (c*c))/(2*b*a);
        if (cos > 0)
        {
            float p = (a+b+c)/2;
            double distance = (2*Math.Sqrt(p*(p-a)*(p-b)*(p-c)))/b;
            List<Asteroid> items = new List<Asteroid>();
            if (distance < item.Radius+0.3f)
            {
                return true;
            }
        }
        return false;
    }

    private void HitLasers(List<Asteroid> items)
    {
        foreach (var item in items)
        {
            item.HitLaser();
        }
    }

    public async void KillAfterDelay()
    {
        await Task.Delay((int)Math.Round(FadeTime*1000,0));
        if (Application.isPlaying) Kill();
    }

    public void Kill()
    {
        GameObject.Destroy(Object);
    }
}
