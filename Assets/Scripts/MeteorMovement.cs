using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public Vector3 targetPoint;
    public float speed = 50f;
    private Rigidbody rigidBody;
    public float lifeTime = 10f;
    public GameObject deathVFX;

    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody>();
        this.transform.LookAt(targetPoint);
        Destroy(this.gameObject, this.lifeTime);
    }

    void Update()
    {
        Vector3 difference = this.targetPoint - this.rigidBody.position; 
        Vector3 forceDirection = difference.normalized;
        Vector3 forceVector = forceDirection * this.speed;
        this.rigidBody.AddForce(forceVector);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MeteorMovement>() == null)
        {
            invokeCollisionVFX();
            Destroy(this.gameObject);
        }
    }

    void invokeCollisionVFX()
    {
        GameObject vfx = Instantiate(this.deathVFX, this.transform.position, Quaternion.identity);
        vfx.transform.localScale = this.gameObject.transform.localScale * 14;
    }

}