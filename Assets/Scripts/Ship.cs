using System;
using UnityEngine;

[Serializable]
public class Ship
{
    public int healthMax;
    private int _health;
    public float propelForce;
    public int rotationSpeed;
    private float _fuel = 100;
    public float fuelMax;
    public float fuelRecover;
    public float fuelConsumptionRate;

    public void initSettings()
    {
        this.Health = this.healthMax;
        this.Fuel = this.fuelMax;
    }

    public void takeDamage(int damage)
    {
        this.Health -= damage;
    }

    public void consumeFuel()
    {
        this.Fuel -= Time.deltaTime * this.fuelConsumptionRate;
    }

    public void recoverFuel()
    {
        this.Fuel += this.fuelRecover;
    }

    public bool fuelIsZero()
    {
        return this.Fuel == 0;
    }

    public bool isDead()
    {
        return this.Health <= 0;
    }

    public int Health
    {
        get => _health;
        set
        {
            _health = value < 0 ? 0 : value;
        }
    }

    public float Fuel
    {
        get => _fuel;
        set
        {
            if (value > 100)
            {
                _fuel = 100;
            }
            else if (value < 0)
            {
                _fuel = 0;
            }
            else
            {
                _fuel = value;
            }
        }
    }


}