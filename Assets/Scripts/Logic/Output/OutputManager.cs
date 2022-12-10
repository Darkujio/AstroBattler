using System;
using TMPro;
using UnityEngine;
public class OutputManager
{
    private TextMeshProUGUI Coordinates;
    private TextMeshProUGUI Angle;
    private TextMeshProUGUI Speed;
    private TextMeshProUGUI LaserCharges;
    private TextMeshProUGUI LaserCooldown;
    private TextMeshProUGUI FinalScore;
    private GameObject FinalScoreGO;
    private PlayerManager Player;
    public void Init()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        Coordinates = canvas.Find("Coordinates").GetComponent<TextMeshProUGUI>();
        Angle = canvas.Find("Angle").GetComponent<TextMeshProUGUI>();
        Speed = canvas.Find("Speed").GetComponent<TextMeshProUGUI>();
        LaserCharges = canvas.Find("LaserCharges").GetComponent<TextMeshProUGUI>();
        LaserCooldown = canvas.Find("LaserCooldown").GetComponent<TextMeshProUGUI>();

        FinalScoreGO = canvas.Find("FinalScore").gameObject;
        FinalScore = FinalScoreGO.transform.Find("Score").GetComponent<TextMeshProUGUI>();

        Player = Game.Player;
    }
    public void Update()
    {
        LaserCharges.SetText($"Laser Charges Left: {Player.CurrentLaserCharges}");

        float cooldown = Player.LaserCooldown;
        if (cooldown > 0) 
        {
            LaserCooldown.SetText($"Cooldown before next charge: {cooldown.ToString("F2")}");
        }
        else
        {
            LaserCooldown.SetText("Laser charges full");
        }

        Speed.SetText($"Speed : {(Player.Speed.magnitude).ToString("F2")}");

        Angle.SetText($"Angle : {Player.Angle.ToString("F2")}");

        Coordinates.SetText($"Coordinats : {Player.Position}");
    }

    public void GameLose(int finalScore)
    {
        Coordinates.gameObject.SetActive(false);
        Angle.gameObject.SetActive(false);
        Speed.gameObject.SetActive(false);
        LaserCharges.gameObject.SetActive(false);
        LaserCooldown.gameObject.SetActive(false);

        FinalScore.SetText($"Score : {finalScore}");
        FinalScoreGO.SetActive(true);
    }
}
