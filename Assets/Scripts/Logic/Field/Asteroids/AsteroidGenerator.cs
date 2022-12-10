using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AsteroidGenerator
{
    private static ScriptableAsteroid SOBig = Resources.Load("ScriptableObjects/Asteroids/AsteroidBig") as ScriptableAsteroid;
    private static ScriptableAsteroid SOSmall = Resources.Load("ScriptableObjects/Asteroids/AsteroidSmall") as ScriptableAsteroid;
    public static float FieldSizeVertical = (Resources.Load("ScriptableObjects/Field") as ScriptableField).FieldSizeVertical;
    public static float FieldSizeHorizontal = (Resources.Load("ScriptableObjects/Field") as ScriptableField).FieldSizeHorizontal;
    private static int FirstSpawnSecDelay = 5;
    private static int SpawnSecDelay = 15;
    private int CurrentDelay = FirstSpawnSecDelay;
    private Stack<Asteroid> InstantiatedAsteroids = new Stack<Asteroid>();
    private AsteroidManager Manager;

    public void Init(AsteroidManager manager)
    {
        Manager = manager;
        SpawnDelay();
        CurrentDelay = SpawnSecDelay;
    }

    private Asteroid GetAsteroid()
    {
        if (InstantiatedAsteroids.Count != 0)
        {
            return InstantiatedAsteroids.Pop();
        }
        else
        {
            var asteroid = new Asteroid();
            asteroid.CreateGameObject();
            return asteroid;
        }
    }

    private void AsteroidSpawn()
    {
        for (int i = 0; i < SOBig.AmountPerWave; i++)
        {
            InitAsteroid(SOBig, RandomizePosition());
        }
        for (int i = 0; i < SOSmall.AmountPerWave; i++)
        {
            InitAsteroid(SOSmall, RandomizePosition());
        }
        SpawnDelay();
    }

    private void InitAsteroid(ScriptableAsteroid asteroidSO,Vector2 pos)
    {
        var asteroid = GetAsteroid();
        asteroid.Init(asteroidSO, pos);
        Manager.AddAsteroid(asteroid);
    }

    private Vector2 RandomizePosition()
    {
        float x;
        float y;
        if(Random.Range(0, 2) == 0)
        {
            x = FieldSizeHorizontal;
            y = Random.Range(-FieldSizeVertical,FieldSizeVertical);
        }
        else
        {
            y = FieldSizeVertical;
            x = Random.Range(-FieldSizeHorizontal,FieldSizeHorizontal);
        }
        var startingPos = new Vector2(x,y);
        return startingPos;
    }

    public async void SpawnDelay()
    {
        await Task.Delay(CurrentDelay*1000);
        if (Application.isPlaying) AsteroidSpawn();
    }

    public void ReturnAsteroid(Asteroid asteroid)
    {
        InstantiatedAsteroids.Push(asteroid);
    }

    public void SplitAsteroid(Vector2 pos)
    {
        for (int i = 0; i < 2; i++)
        {
            InitAsteroid(SOSmall, pos);
        }
    }
    

}
