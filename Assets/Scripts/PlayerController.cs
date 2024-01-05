using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody;
    private int rotationSpeed = 160; // degrees
    private int _score = 1000;
    private float _fuel = 100;
    private float fuelConsumptionRate = 1.5f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        detectInput();
        consumeFuel();
        updateUIFuel();
        updateUIScore();
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

    void consumeFuel()
    {
        this.Fuel -= Time.deltaTime * this.fuelConsumptionRate;
    }

    void updateUIFuel()
    {
        StatsManager.fuel = this.Fuel;
    }

    void updateUIScore()
    {
        StatsManager.score = this.Score;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Fuel")
        {
            this.Fuel += 10;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Unsafe")
        {
            this.Score -= 100;
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

}
