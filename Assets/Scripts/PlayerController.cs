using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody;
    private float rotationSpeed = 1.25f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        detectInput();
    }

    void detectInput() {
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

    void propel() {
        rigidbody.AddRelativeForce(Vector3.up);
    }

    void rotateRight() {
        var rotarDerecha = transform.rotation;
        rotarDerecha.z -= Time.deltaTime * rotationSpeed;
        transform.rotation = rotarDerecha;
    }

    void rotateLeft() {
        var rotarIzquierda = transform.rotation;
        rotarIzquierda.z += Time.deltaTime * rotationSpeed;
        transform.rotation = rotarIzquierda;
    }

}
