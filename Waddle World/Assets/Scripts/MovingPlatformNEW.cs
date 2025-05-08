/* Evan Stark - March 23rd 2025 - ITCS 4231 001
Remake of the moving platform script so it only requires user defined points
instead of relying on other objects

SOURCES
https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Vector3.html 
https://www.youtube.com/watch?v=ly9mK0TGJJo */

using UnityEngine;

public class MovingPlatformNEW : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float startX;
    
    [SerializeField]
    private float startY;
    
    [SerializeField]
    private float startZ;

    [SerializeField]
    private float endX;
    
    [SerializeField]
    private float endY;

    [SerializeField]
    private float endZ;
    
    private Vector3 startPosition;
    private Vector3 endPosition;

    private float travelDistance;
    private float travelTime;
    private float elapsedTime;

    // Boolean to check if the platform needs to go in reverse.
    private bool isReverse = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        startPosition = new Vector3(startX, startY, startZ);
        transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
        CalculateDistanceTime(endX, endY, endZ);
    }

    // Using FixedUpdate to move platform each frame and to apply transformations
    // on the player too.
    void FixedUpdate() {
        elapsedTime += Time.deltaTime;
        float percentage = elapsedTime / travelTime;
        percentage = Mathf.SmoothStep(0, 1, percentage);

        // Swap the end and start position if the platform needs to reverse.
        transform.position = Vector3.Lerp(startPosition, endPosition, percentage);

        // If done traversing and transform is at the start endpoint, travel back
        // to the end position.
        if (percentage >= 1) {
            SwitchStart();
            isReverse = !isReverse;
        }

    }

    void SwitchStart()
    {
        if (isReverse)
        {
            startPosition = new Vector3(endX, endY, endZ);
            endPosition = new Vector3(startX, startY, startZ);
        }

        else if (!isReverse) {
            startPosition = new Vector3(startX, startY, startZ);
            endPosition = new Vector3(endX, endY, endZ);
        }
        
        CalculateDistanceTime(endPosition.x, endPosition.y, endPosition.z);
    }

    // Calculating the distance to travel and the time it takes to get there.
    void CalculateDistanceTime(float x, float y, float z) {
        elapsedTime = 0;
        //travelTime = 0;
        
        endPosition = new Vector3(x, y, z);
        travelDistance = Vector3.Distance(transform.position, endPosition);
        travelTime = travelDistance / moveSpeed;
    }

    // Trigger functions to apply transforms to player.
    private void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }

}
