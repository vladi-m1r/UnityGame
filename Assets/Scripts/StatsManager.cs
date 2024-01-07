using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager: MonoBehaviour
{
    public static float fuel;
    public static float score;
    public static float health;
    public Text fuelStat;
    public Text scoreStat;
    public Text healthStat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fuelStat.text = "Fuel: " + fuel;
        scoreStat.text = "Score: " + score;
        healthStat.text = "Health: " + health;
    }
}
