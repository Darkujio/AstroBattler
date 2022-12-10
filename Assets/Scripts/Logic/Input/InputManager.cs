using UnityEngine;
using System;

public class InputManager
{
    public float HorizontalInput {get; private set;}
    public float VerticalInput{get; private set;}
    public Action Fire;
    public Action FireLaser;
    public Action<int> Rotate;
    public Action Move;
    public void Init()
    {
        
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.A)) Rotate?.Invoke(1);
        if (Input.GetKey(KeyCode.D)) Rotate?.Invoke(-1);
        if (Input.GetKey(KeyCode.W)) Move?.Invoke();
        //HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space)) Fire?.Invoke();
        if (Input.GetKeyDown(KeyCode.R)) FireLaser?.Invoke();
    }
}
