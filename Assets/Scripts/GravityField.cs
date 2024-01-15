using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public float gravity;
    public float duration;
    public float cooldown;
    public GameObject pointAttractor;
    private float durationCount;
    private float cooldownCount;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        this.durationCount = this.duration;
        this.cooldownCount = 0;
        this.defaultColor = this.gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.cooldownCount <= 0)
        {
            this.durationCount -= Time.deltaTime;
            if (this.durationCount <= 0)
            {
                Color tmpColor = this.defaultColor;
                tmpColor.a = 0;
                this.gameObject.GetComponent<Renderer>().material.color = tmpColor;
                this.cooldownCount = this.cooldown;
            }
        }

        if (this.durationCount <= 0)
        {
            this.cooldownCount -= Time.deltaTime;
            if (this.cooldownCount <= 0)
            {
                this.gameObject.GetComponent<Renderer>().material.color = this.defaultColor;
                this.durationCount = this.duration;
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (this.durationCount >= 0)
        {
            if (other.attachedRigidbody != null)
            {
                Rigidbody attractor = this.GetComponent<Rigidbody>();
                Rigidbody target = other.attachedRigidbody;
                addGravityForce(attractor, target);
            }
        }

    }

    void addGravityForce(Rigidbody attractor, Rigidbody target)
    {
        Vector3 difference = this.pointAttractor.transform.position - target.position;
        Vector3 forceDirection = difference.normalized;
        Vector3 forceVector = forceDirection * gravity;
        target.AddForce(forceVector);
    }
}
