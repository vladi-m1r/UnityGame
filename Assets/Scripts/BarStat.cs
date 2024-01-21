using System;
using UnityEngine;
using UnityEngine.UI;

public class BarStat: MonoBehaviour
{
    private Slider slider;
    public bool percentSymbol;
    public Text text;

    void Awake()
    {
        this.slider = GetComponent<Slider>();
    }

    public void setMaxValue(float maxHealth)
    {
        this.slider.maxValue = maxHealth; 
    }

    public void updateValue(float currentHealth)
    {
        currentHealth = (float)Math.Round(currentHealth);
        this.text.text = "" + currentHealth + (this.percentSymbol ? "%" : "");
        this.slider.value = currentHealth;
    }

}
