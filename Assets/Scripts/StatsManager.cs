using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager: MonoBehaviour
{
    public static float score;
    public Text scoreStat;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        scoreStat.text = "Score: " + score;
    }

}
