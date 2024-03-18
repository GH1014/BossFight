using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Boss Info")]
    public int bossHealth = 90;
    public int bossMaxHealth = 100;

    [Header("# Player Info")]
    public int health = 50;
    public int maxHealth = 100;

    [Header("# Game Object")]
    public Player player;
    public WarriorBoss warriorBoss;

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        Screen.SetResolution(1080, 1920, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
