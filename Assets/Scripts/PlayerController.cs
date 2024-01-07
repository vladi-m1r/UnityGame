using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public int nivel;
    public int healthMax;
    private int _healt;
    private int healtLoseByCollision;
    private int scoreLoseByCollision;
    public float timeToNextCollision = 1;
    private float timeToNextCollisionCount;
    private Rigidbody rigidbody;
    private int rotationSpeed = 160; // degrees
    private int _score = 1000;
    private float _fuel = 100;
    private float fuelRecover = 10;
    private float fuelConsumptionRate = 1.5f;

    void Start()
    {
        this.timeToNextCollisionCount = timeToNextCollision;
        this.Health = this.healthMax;
        this.healtLoseByCollision = healthMax / 5 - nivel + 1;
        this.scoreLoseByCollision = 100;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        detectInput();
        consumeFuel();
        updateUIFuel();
        updateUIScore();
        updateUIHealth();
        updateCountToNexCollision();
    }

    void detectInput()
    {
        propel();
        rotate();
    }

    void propel()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up);
        }
    }

    void rotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotateRight();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rotateBack();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rotateFront();
        }
    }

    void rotateRight()
    {
        Vector3 eulerAngleVelocity = new Vector3(0, 0, -this.rotationSpeed);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
    }

    void rotateLeft()
    {
        Vector3 eulerAngleVelocity = new Vector3(0, 0, this.rotationSpeed);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
    }

    void rotateBack()
    {
        Vector3 eulerAngleVelocity = new Vector3(this.rotationSpeed, 0, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
    }

    void rotateFront()
    {
        Vector3 eulerAngleVelocity = new Vector3(-this.rotationSpeed, 0, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
    }

    void consumeFuel()
    {
        this.Fuel -= Time.deltaTime * this.fuelConsumptionRate;
    }

    void updateCountToNexCollision()
    {
        if (this.timeToNextCollisionCount > 0)
        {
            this.timeToNextCollisionCount -= Time.deltaTime;
        }
    }

    void updateUIFuel()
    {
        StatsManager.fuel = this.Fuel;
    }

    void updateUIScore()
    {
        StatsManager.score = this.Score;
    }

    void updateUIHealth()
    {
        StatsManager.health = this.Health;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Fuel")
        {
            this.Fuel += this.fuelRecover;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Unsafe")
        {
            if (this.timeToNextCollisionCount <= 0) {
                this.Score -= this.scoreLoseByCollision;
                this.Health -= this.healtLoseByCollision;
                this.timeToNextCollisionCount = this.timeToNextCollision;
            }
        }
        else if (collision.gameObject.tag == "Arrival")
        {
            Debug.Log("Final del nivel");
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

    public int Score
    {
        get => _score;
        set
        {
            _score = value < 0 ? 0 : value;
        }
    }

    public int Health
    {
        get => _healt;
        set
        {
            _healt = value < 0 ? 0 : value;
        }
    }

}
