using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorFall : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject targetArea; 
    public GameObject meteor;
    public int meteorsPerIteration;
    public float timeBetweenInteration;
    public float minScale;
    public float maxScale;
    public float meteorSpeed;
    private float timeBetweenInterationCount;

    void Update()
    { 
        if (timeBetweenInterationCount <= 0)
        {
            invokeMeteors();
        }
        this.timeBetweenInterationCount -= Time.deltaTime;
    }

    void invokeMeteors()
    {
        Vector3 randomizeXZ()
        {
            float limitX = this.targetArea.transform.localScale.x / 2;
            float rndmX = Random.Range(-limitX, limitX);

            float limitZ = this.targetArea.transform.localScale.z / 2;
            float rndmZ = Random.Range(-limitZ, limitZ);

            return new Vector3(rndmX, 0, rndmZ);
        }

        for (int i = 0; i < this.meteorsPerIteration; i++)
        {
            Vector3 rndmVector = randomizeXZ();
            Vector3 initialPosition = rndmVector + this.spawnPoint.transform.position;
            Vector3 finalPosition = rndmVector + this.targetArea.transform.position;

            GameObject meteor = Instantiate(this.meteor, initialPosition, Quaternion.identity);
            meteor.transform.localScale *= Random.Range(this.minScale, this.maxScale);
            meteor.GetComponent<MeteorMovement>().targetPoint = finalPosition;
            meteor.GetComponent<MeteorMovement>().speed = this.meteorSpeed;
        }
        this.timeBetweenInterationCount = this.timeBetweenInteration;
    }
    
}
