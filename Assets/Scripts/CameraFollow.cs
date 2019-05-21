using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //what is the camera following
    public float smoothTime = .15f;//time to follow target
    public float minXPos = 0, maxXPos = 0, minYPos = 0, maxYPos = 0; //min X/Y and max X/Y camera positions
    public bool bounds; //bounds on or off

    Vector3 velocity = Vector3.zero; //resets the velocity to zero
    
    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position; //target position

        if (bounds == true) //if bounds are on
        {
            targetPosition.y = Mathf.Clamp(target.position.y, minYPos, maxYPos);
            targetPosition.x = Mathf.Clamp(target.position.x, minXPos, maxXPos);
        }

        targetPosition.z = transform.position.z; //align the camera and the target z position

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
