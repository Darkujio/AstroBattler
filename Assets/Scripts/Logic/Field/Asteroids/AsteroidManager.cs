using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager
{
    private AsteroidGenerator AsteroidsGen = new AsteroidGenerator();
    public List<Asteroid> ActiveAsteroids {get; private set;} = new List<Asteroid>();
    public void Init()
    {
        AsteroidsGen.Init(this);
    }

    public void AddAsteroid(Asteroid asteroid)
    {
        ActiveAsteroids.Add(asteroid);
    }
    public void FixedUpdate()
    {
        foreach (var item in ActiveAsteroids)
        {
            item.FixedUpdate();
        }
    }

    public void SetInactive(Asteroid asteroid)
    {
        ActiveAsteroids.Remove(asteroid);
        AsteroidsGen.ReturnAsteroid(asteroid);
    }

    public void SplitAsteroid(Asteroid asteroid)
    {
        AsteroidsGen.SplitAsteroid(asteroid.Position);
    }
}
