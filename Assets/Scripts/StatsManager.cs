using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager: MonoBehaviour
{
    public static float fuel;
    public static float score;
    public Text fuelStat;
    public Text scoreStat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fuelStat.text = "Fuel: " + fuel;
        scoreStat.text = "Score: " + score;
    }
}
