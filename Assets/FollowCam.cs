using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Transform pig1;
    public Transform pig2;

    Vector3 offset;
    bool following;

    private void Start()
    {
        Vector3 target = (pig1.position + pig2.position) / 2;
        offset = target - transform.position;
    }

    public void StartFollowing()
    {
        following = true;
    }

    private void LateUpdate()
    {
        if (following)
        {
            Vector3 target = (pig1.position + pig2.position) / 2;
            transform.position = Vector3.Lerp(transform.position, target - offset, Time.deltaTime * 2);
            Vector3 toTarget = target - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(toTarget, Vector3.up), Time.deltaTime * 2);
        }
    }
}
