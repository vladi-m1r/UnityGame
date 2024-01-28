using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private int healthLoseByCollision;
    private int scoreLoseByCollision;
    public float timeToNextCollision = 1;
    private float timeToNextCollisionCount;
    private Rigidbody rigidBody;
    private int _score = 1000;
    private AudioManager audioManager;
    public GameObject floatingText;
    public TrackingController tc;
    public Ship ship;
    public ShipVFX shipVfx;
    public BarStat healthBar;
    public BarStat fuelBar;
    public ScoreUI scoreUI;
    public GameObject propelVfx;
    public LevelSettings levelSettings;
    public UnityEvent deathEvent;

    void Start()
    {
        this.ship.initSettings();
        this.timeToNextCollisionCount = timeToNextCollision;
        this.healthLoseByCollision = this.ship.healthMax / (5 - levelSettings.level + 2);
        this.scoreLoseByCollision = 100;
        this.rigidBody = GetComponent<Rigidbody>();

        // Health, Fuel Bar init
        this.healthBar.setMaxValue(this.ship.healthMax);
        this.fuelBar.setMaxValue(this.ship.fuelMax);

        // Audio
        this.audioManager = AudioManager.audioManager;
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
        // hand propel and rotation option
        if (tc != null && tc.leftHandActive()){
            tc.rotate();
            tc.propel(this.ship.Fuel, this.ship.propelForce, this.propelVfx, this.audioManager);
        }else{
            propel();
            rotate();
        }
    }

    void propel()
    {
        if (Input.GetKey(KeyCode.Space) && !this.ship.fuelIsZero())
        {
            rigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * this.ship.propelForce);

            if (!this.audioManager.propulse.isPlaying)
            {
                this.audioManager.propulse.Play();
            }
            // vfx propel show effect
            if (!this.propelVfx.activeSelf)
            {
                this.propelVfx.SetActive(true);
            }
        }
        else
        {
            this.propelVfx.SetActive(false);
            this.audioManager.propulse.Stop();
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
        IEnumerator delay(int seconds) {
            yield return new WaitForSeconds(seconds);
            this.deathEvent.Invoke();
        }

        this.fuelBar.updateValue(this.ship.Fuel);
        if (this.ship.Fuel <= 0.5 && this.ship.Fuel > 0 && !this.audioManager.lowFuel.isPlaying) {
            this.audioManager.lowFuel.Play();
            StartCoroutine(delay(2));
        }
    }

    void updateUIScore()
    {
        this.scoreUI.updateValue(this.Score);
    }

    void updateUIHealth()
    {
        this.healthBar.updateValue(this.ship.Health);
        if (this.ship.isDead()) 
        {
            Instantiate(this.shipVfx.death, this.transform.position, Quaternion.identity);
            this.audioManager.playAudio(this.audioManager.death);
            this.deathEvent.Invoke();
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Fuel")
        {
            this.audioManager.playAudio(this.audioManager.obtainedFuel);
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
                this.audioManager.playAudio(this.audioManager.collision);
                this.Score -= this.scoreLoseByCollision;
                this.ship.takeDamage(this.healthLoseByCollision);
                this.timeToNextCollisionCount = this.timeToNextCollision;
                // VFX instantiate in contact point
                Instantiate(this.shipVfx.collision, collision.GetContact(0).point, Quaternion.identity);
            }
        }
        else if (collision.gameObject.tag == "Arrival")
        {
            this.audioManager.playAudio(this.audioManager.win);
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
