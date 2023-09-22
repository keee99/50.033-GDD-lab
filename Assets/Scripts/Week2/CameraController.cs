using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates end of map
    private float offset; // initial x-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float viewportHalfWidth;

    private void Start()
    {
        // Get coords of the bottomleft of viewport to find width of camera viewport
        // 0,0,0 is the camera viewport's local bottom left corner 
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - transform.position.x);
        offset = transform.position.x - player.position.x;
        startX = transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;
    }

    private void Update()
    {
        float desiredX = player.position.x + offset;

        // Desired X coord within acceptable limit
        if (desiredX > startX && desiredX < endX)
        {
            transform.position = new Vector3(desiredX, transform.position.y, transform.position.z);
        }
    }
}
