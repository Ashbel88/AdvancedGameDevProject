/* Evan Stark - March 13th 2025 - ITCS 4231
This script will assign Waypoint objects to platform to move it 
around in the scene. 

SOURCE USED: https://www.youtube.com/watch?v=ly9mK0TGJJo*/

using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] 
    private WaypointPath waypointPath;
    
    [SerializeField] 
    private float speed;

    private int targetIndex;
    
    private Transform prevPoint;
    private Transform targetPoint;

    private float timeToPoint;
    private float elapsedTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NextWaypoint();
    }

    // Using FixedUpdate over Update due to the more complex physics of tracking
    // the player on moving platform.
    void FixedUpdate()
    {
        elapsedTime = elapsedTime + Time.deltaTime;
        // Tracking how far the platform is in journey via percentage.
        float percentage = elapsedTime / timeToPoint;
        // SmoothStep allows for more smooth/natural animation.
        percentage = Mathf.SmoothStep(0, 1, percentage);

        transform.position = Vector3.Lerp(prevPoint.position, targetPoint.position, percentage);

        // If percentage >= 1 (path has ender) --> find next point.
        if (percentage >= 1)
        {
            NextWaypoint();
        }
    }

    // Get the next waypoint in the path via the WaypointPath methods.
    private void NextWaypoint()
    {
        prevPoint = waypointPath.GetWaypoint(targetIndex);
        targetIndex = waypointPath.GetNextIndex(targetIndex);
        targetPoint = waypointPath.GetWaypoint(targetIndex);
        
        // Reset elapsedTime and re-calculate distance and timeToPoint.
        elapsedTime = 0;
        float pointDistance = Vector3.Distance(prevPoint.position, targetPoint.position);
        timeToPoint = pointDistance / speed;
    }

    // Ensure smooth player movement on platform by making it a child of the platform.
    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    // Exit the trigger once not on the platform.
    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }

}
