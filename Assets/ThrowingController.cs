using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingController : MonoBehaviour
{
    public FollowCam cam;
    public Transform pigHolder;
    public Pig pig1;
    public Pig pig2;

    public Transform throwStartPos;
    public Transform throwEndPos;
    public float maxThrowForce;

    [Range(0, 1f)]
    public float forceRandomModifier;
    [Range(0, 1f)]
    public float torqueRandomModifier;

    //x is spin, y is forward
    Vector2 throwVelocity;
    Vector2 previousFramePosition;

    enum State { idle, throwing, done }
    State currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentState == State.idle)
        {
            currentState = State.throwing;
            previousFramePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && currentState == State.throwing)
        {
            print(throwVelocity);
            currentState = State.done;
            CastPig(pig1);
            CastPig(pig2);
            cam.StartFollowing();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && currentState == State.throwing)
        {
            Vector2 delta = (Vector2)Input.mousePosition - previousFramePosition;
            throwVelocity = Vector2.Lerp(throwVelocity, delta, 0.05f);
            previousFramePosition = Input.mousePosition;
            pigHolder.position = Vector3.Lerp(throwStartPos.position, throwEndPos.position, throwVelocity.y / maxThrowForce);
        }
    }

    void CastPig(Pig pig)
    {
        pig.rb.isKinematic = false;
        pig.rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        float forceRandomMod = 1f + Random.Range(-forceRandomModifier, forceRandomModifier);
        float torqueRandomModX = 1f + Random.Range(-torqueRandomModifier, torqueRandomModifier);
        float torqueRandomModZ = 1f + Random.Range(-torqueRandomModifier, torqueRandomModifier);
        pig.rb.AddForce(pig1.transform.forward * throwVelocity.y * forceRandomMod * 0.5f
                    + pig1.transform.right * throwVelocity.x * forceRandomMod * 0.3f, ForceMode.Impulse);
        pig.rb.angularVelocity = new Vector3(throwVelocity.y * torqueRandomModX, 0, throwVelocity.x * -2f * torqueRandomModZ);
    }
}
