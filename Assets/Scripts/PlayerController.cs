using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public int nivel;
    public int healthMax;
    private int _healt;
    public float propelForce;
    private int healtLoseByCollision;
    private int scoreLoseByCollision;
    public float timeToNextCollision = 1;
    private float timeToNextCollisionCount;
    private Rigidbody rigidBody;
    private int rotationSpeed = 160; // degrees
    private int _score = 1000;
    private float _fuel = 100;
    private float fuelRecover = 10;
    private float fuelConsumptionRate = 1.5f;
    public GameObject audioSourceManager;
    public GameObject floatingText;

    void Start()
    {
        this.timeToNextCollisionCount = timeToNextCollision;
        this.Health = this.healthMax;
        this.healtLoseByCollision = this.healthMax / (5 - nivel + 2);
        this.scoreLoseByCollision = 100;
        this.rigidBody = GetComponent<Rigidbody>();
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
            if (this.Fuel > 0)
            {
                rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * this.propelForce);
            }
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
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }

    void rotateLeft()
    {
        Vector3 eulerAngleVelocity = new Vector3(0, 0, this.rotationSpeed);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }

    void rotateBack()
    {
        Vector3 eulerAngleVelocity = new Vector3(this.rotationSpeed, 0, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }

    void rotateFront()
    {
        Vector3 eulerAngleVelocity = new Vector3(-this.rotationSpeed, 0, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
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
        AudioSource fuelAudioSource = this.audioSourceManager.GetComponents<AudioSource>()[2];
        if (this.Fuel <= 6 && this.Fuel > 0 && !fuelAudioSource.isPlaying) {
            fuelAudioSource.Play();
        }
    }

    void updateUIScore()
    {
        StatsManager.score = this.Score;
    }

    void updateUIHealth()
    {
        StatsManager.health = this.Health;
        if (this.Health <= 0) 
        {
            this.audioSourceManager.GetComponents<AudioSource>()[3].Play();
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Fuel")
        {
            this.audioSourceManager.GetComponents<AudioSource>()[1].Play();
            this.Fuel += this.fuelRecover;
            createFloatingText(Color.green, $"+{this.fuelRecover}");
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Unsafe")
        {
            if (this.timeToNextCollisionCount <= 0)
            {
                createFloatingText(Color.red, $"-{this.healtLoseByCollision}");
                this.audioSourceManager.GetComponent<AudioSource>().Play();
                this.Score -= this.scoreLoseByCollision;
                this.Health -= this.healtLoseByCollision;
                this.timeToNextCollisionCount = this.timeToNextCollision;
            }
        }
        else if (collision.gameObject.tag == "Arrival")
        {
            this.audioSourceManager.GetComponents<AudioSource>()[4].Play();
            Time.timeScale = 0;
        }
    }

    private void createFloatingText(Color color, string text) {
        GameObject floatingTextContainer = Instantiate(this.floatingText, this.transform.position, Quaternion.identity);
        floatingTextContainer.GetComponent<FloatingText>().parentTransformPosition = this.transform;
        floatingTextContainer.GetComponent<FloatingText>().text = text;
        floatingTextContainer.GetComponent<FloatingText>().color = color;
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
