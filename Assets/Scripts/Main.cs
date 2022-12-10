using UnityEngine;

public class Main : MonoBehaviour
{
    private bool IsPaused;
    private void Awake()
    {
        Game.Start();
        IsPaused = false;
    }

    private void Update()
    {
        if (!IsPaused)
        Game.Update();
    }

    private void FixedUpdate()
    {
        if (!IsPaused)
        Game.FixedUpdate();
    }
}
