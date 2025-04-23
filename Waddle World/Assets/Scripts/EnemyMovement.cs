// Evan Stark - April 17th 2025 - ITCS 4231 001
// This script implements simple enemy movement and 
// calls to a pathfinding algorithm if the enemy is in a radius
// close enough to the player.

/*
SOURCES USED
https://docs.unity3d.com/Manual/class-random.html (Unity's Random class).
https://www.w3schools.com/cs/cs_switch.php (C# Switch loop basics).
https://docs.unity3d.com/Manual/collider-interactions-ontrigger.html (OnTrigger Event documentation).
https://www.youtube.com/watch?v=BJzYGsMcy8Q (Rotation )
Other code provided by Jerell Bell.
*/

using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    // Movement fields for how fast enemy moves.
    [SerializeField] private float moveSpeed;

    // Target and collision fields to set target to go after; target's collision.
    [SerializeField] private Transform trans;
    [SerializeField] private Transform target;

    // [SerializeField] private Rigidbody rb;

    private bool inRange = false;
    private Vector3 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       movement = InitialRandomMove();
    }

    // Update is called once per frame
    void Update()
    {

        if (!inRange)
        {
            transform.position += movement;
        }

        // Move enemy towards target if they are in trigger's range.
        else
        {
            movement = (trans.position - target.position) * moveSpeed;
            movement.y = 0.0f;
            transform.position += movement;
        }

    }
    
    // Picking a random direction for the enemy to move in.
    private Vector3 InitialRandomMove()
    {
        Vector3 movement = new Vector3();
        
        // Pick a random direction to initially move the enemy.
        int rng = Random.Range(1, 5);
        switch (rng)
        {
            case 1:
                //positive x and positive z.
                movement.x = moveSpeed;
                movement.z = moveSpeed;
                //Vector3 newMove = new Vector3(moveSpeed, 0, moveSpeed);
                break;
            
            case 2:
                // positive x, negative z.
                movement.x = moveSpeed;
                movement.z = -(moveSpeed);
                break;
            
            case 3:
                // negative x, positive z.
                movement.x = -(moveSpeed);
                movement.z = moveSpeed;
                break;
            
            // Case 4 and other numbers if for some reason if over the range.
            default:
                // negative x and negative z.
                movement.x = -(moveSpeed);
                movement.z = -(moveSpeed);
                break;
        } 
        
        return movement;
    }

    // If target's position is in the enemy's trigger, set inRange to true.
    private void OnTriggerEnter(Collider other) 
    {
        inRange = true;
    }

    // When the player leaves the trigger, set inRange back to false.
    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }

}