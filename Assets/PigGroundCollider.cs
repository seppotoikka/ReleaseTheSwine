using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigGroundCollider : MonoBehaviour
{

    public bool IsTouchingGround { get; private set; }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        IsTouchingGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsTouchingGround = false;
    }
}
