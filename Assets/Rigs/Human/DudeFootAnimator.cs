using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script animates the foot/legs by changing
/// the local position
/// </summary>
public class DudeFootAnimator : MonoBehaviour
{

    /// <summary>
    /// The local-space starting position of this object
    /// </summary>
    private Vector3 startingPos;

    /// <summary>
    /// The local-space starting rotation of this object
    /// </summary>
    private Quaternion startingRot;

    /// <summary>
    /// An offset value to use for timing of the sin wave 
    /// that controls the walk anim. Measured in radians.
    /// 
    /// A value of Mathf.PI would be half-a-period.
    /// </summary>
    public float stepOffset = 0;

    DudeController dude;

    private Vector3 targetPos;
    private Quaternion targetRot;

    void Start()
    {
        startingPos = transform.localPosition;
        startingRot = transform.localRotation;
        dude = GetComponentInParent<DudeController>();
    }

    void Update()
    {

        switch (dude.state)
        {
            case DudeController.States.Idle:
                AnimateIdle();
                break;
            case DudeController.States.Walk:
                AnimateWalk();
                break;
        }

        // ease position and rotation towards their targets
        transform.position = AnimMath.Slide(transform.position, targetPos, .01f);
        transform.rotation = AnimMath.Slide(transform.rotation, targetRot, .01f);

    }

    void AnimateWalk()
    {
        Vector3 finalPos = startingPos;
        float time = (Time.time + stepOffset) * dude.stepSpeed;

        // lateral movement: (z + x)
        float frontToBack = Mathf.Sin(time);
        finalPos += dude.moveDir * frontToBack * dude.walkScale.z;

        // vertical movement: (y)
        finalPos.y += Mathf.Cos(time) * dude.walkScale.y;
        //finalPos.x *= dude.walkScale.x;

        bool isOnGround = (finalPos.y < startingPos.y);

        if (isOnGround) finalPos.y = startingPos.y;

        // convert from z (-1 to 1) to p (0 to 1 to 0)
        float p = 1 - Mathf.Abs(frontToBack);

        float anklePitch = isOnGround ? 0 : -p * 20;

        //transform.localPosition = finalPos;
        //transform.localRotation = Quaternion.Euler(0, 0, anklePitch);

        targetPos = transform.parent.TransformPoint(finalPos);
        targetRot = transform.parent.rotation * startingRot * Quaternion.Euler(0, 0, anklePitch);


    }

    void AnimateIdle()
    {
        transform.localPosition = startingPos;
        transform.localRotation = startingRot;

        //targetPos = transform.TransformPoint(startingPos);
        //targetRot = transform.parent.rotation * startingRot;

        FindGround();
    }

    void FindGround()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, .5f, 0), Vector3.down * 2);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //transform.position = hit.point;
            //transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            targetPos = hit.point;
            targetRot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        }
        else
        {

        }
    }
}
