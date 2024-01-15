using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public Vector3 targetPoint;
    public float speed = 50f;
    private Rigidbody rigidBody;
    public float lifeTime = 10f;

    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody>();
        this.transform.LookAt(targetPoint);
        this.rigidBody.AddForce(this.transform.forward * 10000 * this.speed * Time.deltaTime);
        Destroy(this.gameObject, this.lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MeteorMovement>() == null)
        {
            Destroy(this.gameObject);
        }
    }

}
