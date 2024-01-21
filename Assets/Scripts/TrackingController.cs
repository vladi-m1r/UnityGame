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
        }else if(h1 == null){
            //rb.isKinematic = false;
        }else{
            rotateHand(h1);
        }
    }
    private void rotateHand(Hand hand1){
        //rb.isKinematic = true;
        Quaternion handRotation = hand1.Rotation;
        target.transform.rotation = handRotation;
    }
    public void propel(float fuel, float propelForce){
        Hand h1 = Hands.Provider.GetHand(Chirality.Left);
        if (h1.IsPinching()){
            rb.isKinematic = false;
            print("IS KINE FALSE");
            if (fuel > 0){
                rb.AddRelativeForce(Vector3.up * Time.deltaTime * propelForce);
            }
        }
    }
}