using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController3D : MonoBehaviour
{
    public Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates end of map
    private float offset; // initial x-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private Vector3 startPos;
    private Quaternion startRot;


    private float offsetY;
    private float startY;

    private void Start()
    {
        // // Get coords of the bottomleft of viewport to find width of camera viewport
        // // 0,0,0 is the camera viewport's local bottom left corner 
        // // the z-component is the distance of the resulting plane from the camera 
        // Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        // viewportHalfWidth = Mathf.Abs(bottomLeft.x - transform.position.x);
        // offset = transform.position.x - player.position.x;
        // startX = transform.position.x;
        // endX = endLimit.transform.position.x - viewportHalfWidth;

        startPos = transform.position;
        startX = transform.position.x;
        endX = endLimit.transform.position.x;
        offset = transform.position.x - player.position.x;

        startPos = transform.position;
        startY = transform.position.y;

        startRot = transform.rotation;
    }

    private void Update()
    {
        float desiredX = player.position.x + offset;


        // Desired X coord within acceptable limit
        if (desiredX > startX && desiredX < endX)
        {
            transform.position = new Vector3(
                desiredX,
                transform.position.y,
                transform.position.z
            );
        }

        // Rotate to look at player
        Vector3 target = player.position;
        target.y += 1.5f;
        transform.LookAt(target);

    }

    public void Reset()
    {
        transform.position = startPos;
        transform.rotation = startRot;
    }
}
