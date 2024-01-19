using System;
using UnityEngine;

[Serializable]
public class Ship
{
    public int healthMax;
    private int _health;
    public float propelForce;
    public int rotationSpeed;
    public float _fuel = 100;
    public float fuelRecover;
    public float fuelConsumptionRate;

    public void initSettings()
    {
        this.Health = this.healthMax;
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
            _fuel = value < 0 ? 0 : value;
        }
    }


}