using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class TrackingController : MonoBehaviour
{
    private Rigidbody rb;
    public Transform target;
    private Quaternion initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool leftHandActive(){
        return Hands.Provider.GetHand(Chirality.Left) != null;
    }
    public bool rightHandActive(){
        return Hands.Provider.GetHand(Chirality.Right) != null;
    }
    public void rotate(){
        Hand h1 = Hands.Provider.GetHand(Chirality.Left);
        if(rb == null){
            // Log
        }else if(h1 == null){
            // Log
        }else{
            rotateHand(h1);
        }
    }
    private void rotateHand(Hand hand1){
        Quaternion handRotation = hand1.Rotation;
        target.transform.rotation = handRotation;
    }
    public void propel(float fuel, float propelForce, GameObject propelVfx, AudioManager audioManager){
        Hand hand = Hands.Provider.GetHand(Chirality.Left);
        if (hand.IsPinching() && fuel > 0){
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * propelForce);

            if(!audioManager.propulse.isPlaying){
                audioManager.propulse.Play();
            }
            // vfx propel show effect
            if(!propelVfx.activeSelf){
                propelVfx.SetActive(true);
            }
        }else{
            propelVfx.SetActive(false);
            audioManager.propulse.Stop();
        }
    }
}