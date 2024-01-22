using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI: MonoBehaviour
{
    public Text text;

    public void updateValue(int newValue)
    {
        this.text.text = "" + newValue;
    }

}
