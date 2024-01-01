using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSmooth : MonoBehaviour
{
    private Vector3 offset;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float smoothTime;
    private Vector3 currentVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        this.offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = this.target.position + this.offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
