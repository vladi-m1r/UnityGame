using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovable : MonoBehaviour
{
    public float speed;
    public float waitDuration;
    public GameObject ways;
    private Transform[] wayPoints;
    private Vector3 targetPosition;
    private int pointIndex;
    private int pointCount;
    private int direction = 1;
    private int speedMultiplier = 1;
    private bool collisionChange = false;

    // Start is called before the first frame update
    private void Awake() 
    {
        this.wayPoints = new Transform[this.ways.transform.childCount];        
        for (int i = 0; i < this.ways.transform.childCount; i++)
        {
            this.wayPoints[i] = this.ways.transform.GetChild(i).gameObject.transform;
        }
    }

    void Start()
    {
        this.pointCount = this.wayPoints.Length;
        this.pointIndex = 1;
        this.targetPosition = this.wayPoints[this.pointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = this.speedMultiplier * this.speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, this.targetPosition, step);

        if (transform.position == this.targetPosition)
        {
            nextPoint();
        }
    }

    private void nextPoint()
    {
        if (this.pointIndex == this.pointCount - 1)
        {
            this.direction = -1;
        }
        else if (this.pointIndex == 0)
        {
            this.direction = 1;
        }

        this.pointIndex += this.direction;
        this.targetPosition = this.wayPoints[this.pointIndex].transform.position;
        if (!this.collisionChange) {
            StartCoroutine(WaitNextPoint());
        } else {
            this.collisionChange = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.collisionChange = true;
            this.nextPoint();
        }
    }

    IEnumerator WaitNextPoint()
    {
        this.speedMultiplier = 0;
        yield return new WaitForSeconds(this.waitDuration);
        this.speedMultiplier = 1;
    }
}