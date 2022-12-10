using UnityEngine;

public class FieldManager
{
    public AsteroidManager Asteroids {get; private set;} = new AsteroidManager();
    private int Score = 0;
    public void Init()
    {
        Asteroids.Init();
    }
    public void FixedUpdate()
    {
        Asteroids.FixedUpdate();
    }
    public void OnLoseGame()
    {
        Game.Pause(true);
        Game.Output.GameLose(Score);
    }
    public void IncreaseScore()
    {
        Score++;
    }
}
