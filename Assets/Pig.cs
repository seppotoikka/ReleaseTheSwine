using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public PigColliders pigColliders;
    public int pigLayer;
    public bool touchingOtherPig;
    public Rigidbody rb;

    public bool isStopped;

    public enum State { idle, inAir }
    public State PigState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PigState == State.idle && !rb.isKinematic)
        {
            PigState = State.inAir;
        }            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == pigLayer)
        {
            touchingOtherPig = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == pigLayer)
        {
            touchingOtherPig = false;
        }
    }

    [System.Serializable]
    public struct PigColliders
    {
        public PigGroundCollider snout;
        public PigGroundCollider leftEar;
        public PigGroundCollider rightEar;
        public PigGroundCollider leftForeLeg;
        public PigGroundCollider rightForeLeg;
        public PigGroundCollider leftHindLeg;
        public PigGroundCollider rightHindLeg;
        public PigGroundCollider leftSide;
        public PigGroundCollider rightSide;
        public PigGroundCollider backside;

        public enum Positions { standing, back }

        public Positions GetPosition()
        {
            return Positions.standing;
        }
    }

}
