using System;

public class Game
{
    public static FieldManager Field = new FieldManager();
    public static InputManager Input = new InputManager();
    public static OutputManager Output = new OutputManager();
    public static PlayerManager Player = new PlayerManager();
    private static bool Paused = false;
    public static void Start()
    {
        Player.Init();
        Output.Init();
        Field.Init();
    }
    public static void FixedUpdate()
    {
        if (!Paused)
        {
            Player.FixedUpdate();
            Field.FixedUpdate();
        }
    }
    public static void Update()
    {
        if (!Paused)
        {
            Input.Update();
            Output.Update();
        }
    }
    public static void Pause(bool value)
    {
        Paused = value;
    }
}