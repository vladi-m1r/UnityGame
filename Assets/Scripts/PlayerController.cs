using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody;
    private float rotationSpeed = 1.25f;
    private int score;
    private float fuel = 100;
    private float speedConsumingFuel = 1.5f;

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
    }

    void detectInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            propel();
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotateRight();
        }
    }

    void propel()
    {
        rigidbody.AddRelativeForce(Vector3.up);
    }

    void rotateRight()
    {
        var rotarDerecha = transform.rotation;
        rotarDerecha.z -= Time.deltaTime * rotationSpeed;
        transform.rotation = rotarDerecha;
    }

    void rotateLeft()
    {
        var rotarIzquierda = transform.rotation;
        rotarIzquierda.z += Time.deltaTime * rotationSpeed;
        transform.rotation = rotarIzquierda;
    }

    void consumeFuel()
    {
        this.fuel -= Time.deltaTime * this.speedConsumingFuel;
    }

    void updateUIFuel()
    {
        StatsManager.fuel = this.fuel;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Fuel")
        {
            this.fuel += 10;
            Destroy(collision.gameObject);
        }       
    }


}
