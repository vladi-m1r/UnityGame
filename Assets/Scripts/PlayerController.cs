using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public int nivel;
    private int healthLoseByCollision;
    private int scoreLoseByCollision;
    public float timeToNextCollision = 1;
    private float timeToNextCollisionCount;
    private Rigidbody rigidBody;
    private int _score = 1000;
    public AudioSource audioSourceManager;
    public GameObject floatingText;
    public TrackingController tc;
    public Ship ship;
    public ShipAudioClips clips;
    public ShipVFX shipVfx;

    void Start()
    {
        this.ship.initSettings();
        this.timeToNextCollisionCount = timeToNextCollision;
        this.healthLoseByCollision = this.ship.healthMax / (5 - nivel + 2);
        this.scoreLoseByCollision = 100;
        this.rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        detectInput();
        this.ship.consumeFuel();
        updateUI();
        updateCountToNexCollision();
    }

    void updateUI ()
    {
        updateUIFuel();
        updateUIHealth();
        updateUIScore();
    }

    void detectInput()
    {
        // hand rotation opction
        if (tc != null && tc.leftHandActive()){
            print("Hand controller active");
            tc.rotate();
            tc.propel(this.ship.Fuel, this.ship.propelForce);
        }else{
            //this.rigidBody.isKinematic = false;
            print("Keyboard controller active");
            propel();
        }
        rotate();
    }

    void propel()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!this.ship.fuelIsZero())
            {
                rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * this.ship.propelForce);

                if (!this.clips.propulse.isPlaying)
                {
                    this.clips.propulse.Play();
                }
            }
        }
        else
        {
            this.clips.propulse.Stop();
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
        Vector3 eulerAngleVelocity = new Vector3(this.ship.rotationSpeed, 0, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }

    void rotateLeft()
    {
        Vector3 eulerAngleVelocity = new Vector3(-this.ship.rotationSpeed, 0, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }

    void rotateBack()
    {
        Vector3 eulerAngleVelocity = new Vector3(0, 0, this.ship.rotationSpeed);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }

    void rotateFront()
    {
        Vector3 eulerAngleVelocity = new Vector3(0, 0, -this.ship.rotationSpeed);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
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
        StatsManager.fuel = this.ship.Fuel;
        if (this.ship.Fuel <= 0.5 && this.ship.Fuel > 0 && !this.clips.lowFuel.isPlaying) {
            this.clips.lowFuel.Play();
        }
    }

    void updateUIScore()
    {
        StatsManager.score = this.Score;
    }

    void updateUIHealth()
    {
        StatsManager.health = this.ship.Health;
        if (this.ship.isDead()) 
        {
            Instantiate(this.shipVfx.death, this.transform.position, Quaternion.identity);
            this.audioSourceManager.PlayOneShot(this.clips.deadSong);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Fuel")
        {
            this.audioSourceManager.PlayOneShot(this.clips.recoverFuel);
            this.ship.recoverFuel();
            createFloatingText(Color.green, $"+{this.ship.fuelRecover}");
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Unsafe")
        {
            if (this.timeToNextCollisionCount <= 0)
            {
                createFloatingText(Color.red, $"-{this.healthLoseByCollision}");
                this.audioSourceManager.PlayOneShot(this.clips.collision);
                this.Score -= this.scoreLoseByCollision;
                this.ship.takeDamage(this.healthLoseByCollision);
                this.timeToNextCollisionCount = this.timeToNextCollision;
                // VFX instantiate in contact point
                Instantiate(this.shipVfx.collision, collision.GetContact(0).point, Quaternion.identity);
            }
        }
        else if (collision.gameObject.tag == "Arrival")
        {
            this.audioSourceManager.PlayOneShot(this.clips.win);
            Time.timeScale = 0;
        }
    }

    private void createFloatingText(Color color, string text) {
        GameObject floatingTextContainer = Instantiate(this.floatingText, this.transform.position, Quaternion.identity);
        FloatingText floatingText = floatingTextContainer.GetComponent<FloatingText>();
        floatingText.parentTransformPosition = this.transform;
        floatingText.text = text;
        floatingText.color = color;
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
