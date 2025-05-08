// Evan Stark - April 17th 2025 - ITCS 4231 001
// This script implements simple enemy movement and 
// calls to a pathfinding algorithm if the enemy is in a radius
// close enough to the player.

/*
SOURCES USED
https://docs.unity3d.com/Manual/class-random.html (Unity's Random class).
https://www.w3schools.com/cs/cs_switch.php (C# Switch loop basics).
https://docs.unity3d.com/Manual/collider-interactions-ontrigger.html (OnTrigger Event documentation).
https://www.youtube.com/watch?v=BJzYGsMcy8Q (Rotation)
https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Vector3-magnitude.html (Vector3.magnitude field)
https://www.youtube.com/watch?v=rKGsELBgpQY (Basics about rotating objects in Unity).
https://www.youtube.com/watch?v=tveRasxUabo (Basic Animator tutorial.)
https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Animator.GetCurrentAnimatorStateInfo.html
https://docs.unity3d.com/6000.0/Documentation/ScriptReference/AnimatorStateInfo.html
Portions of code based off of other code by Jerell Bell.
*/

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Movement fields for how fast enemy moves.
    // Keep numbers very small!
    [SerializeField] private float moveSpeed;

    // Set transform fields to assign the transform of the enemy and the transform of the target (player).
    [SerializeField] private Transform trans;
    [SerializeField] private Transform target;

    // Checking to see if the player/target is in the radius trigger.
    [SerializeField] private float chaseRadius;
    private bool inRange = false;
    
    // Vectors to define the enemy's movement.
    private Vector3 movement;
    private Vector3 newMovement; // Used to initialize new movement only once if player leaves the enemy's range.

    // Rotation fields.
    private float rotVel = 100.0f;  // How fast to rotate the enemy.
    private float smoothing = 0.05f;    // The smoothing to add to SmoothDampAngle (keep VERY small!)
    private Vector3 applyRot;   // The vector to apply to the enemy's euler angles.

    // Animator to use to get the enemy's proper animations.
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // newMovement to be initialized as zero; basically starting as "deactivating" it.
       newMovement.x = 0;
       // newMovement.y = 0;
       newMovement.z = 0;

       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        CheckInRadius();

        // Despawning the enemy if the player attacks it and after death animation plays.
        DespawnEnemy();

        // If the player is not in the enemy's chaseRadius, set newMovement to a random direction and movement
        // and assign that to movement.
        // newMovement is already initialized, simply just move the enemy by its already assigned movement.
        if (!inRange)
        {
            if (newMovement.magnitude == 0)
            {
                newMovement = InitialRandomMove();
                movement = newMovement;
            }
            movement.y = 0.0f;
            anim.SetBool("chaseRadius", false);

            // Moving the player by its movement vector.
            trans.position += movement;            
        }

        // Move enemy towards target if they are in trigger's range.
        // Set newMovement's magnitude back to zero since the enemy's movement will
        // now be a Vector heading towards the player.
        else
        {
            if (newMovement.magnitude != 0) 
            {
                newMovement.x = 0;
                newMovement.y = 0;
                newMovement.z = 0;
            }
            
            anim.SetBool("chaseRadius", true);

            movement = (target.position - trans.position) * moveSpeed;
            movement.y = 0.0f;
            trans.position += movement;
        }

    }

    // Using late update to apply rotations to the object.
    private void LateUpdate()
    {
        // Setting the rotation of the enemy via these steps.

        // First, initialize new Vector3 that uses the euler angles of the movement.
        Vector3 rot = Quaternion.LookRotation(movement).eulerAngles;

        // Then create a new Vector3 using SmoothDampAngle function to smoothly rotate the enemy 
        // towards the player.
        // In this case, only the y axis needs to be rotated.
        applyRot = new Vector3
        (
            Mathf.SmoothDampAngle(0.0f, 0.0f, ref rotVel, smoothing),
            Mathf.SmoothDampAngle(applyRot.y, rot.y, ref rotVel, smoothing),
            Mathf.SmoothDampAngle(0.0f, 0.0f, ref rotVel, smoothing)
        );

        // Finally apply the new rotation onto the enemy.
        trans.eulerAngles = applyRot;
    }
    
    // Picking a random direction for the enemy to move in.
    // Moving only in the x and z axes.
    private Vector3 InitialRandomMove()
    {
        Vector3 movement = new Vector3();
        
        // Picks a random number between 1 and 4.
        int rng = Random.Range(1, 5);
        switch (rng)
        {
            case 1:
                // positive x and positive z.
                movement.x = moveSpeed;
                movement.z = moveSpeed;
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

    // If target's position is in the enemy's trigger, set inRange to true
    // to signal the enemy moving directly towards the target.
    private void CheckInRadius() 
    {
        Vector3 diff = new Vector3();
        diff = target.position - trans.position;
        float diffMagnitude = Mathf.Abs(diff.magnitude);
        if (diffMagnitude <= chaseRadius) 
        {
            inRange = true;
        }

        else
        {
            inRange = false;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        anim.SetBool("attackRadius", true);
    }

    // When the player leaves the trigger, set inRange back to false; signalling that
    // the enemy to stop pursuit and picking another random direction to move it via
    // InitialRandomMove()

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("attackRadius", false);
    }

    // Despawning the enemy object after its hit by an attack.
    private void DespawnEnemy()
    {
        AnimatorStateInfo asi = anim.GetCurrentAnimatorStateInfo(0);
        if (asi.IsName("die") == true)
        {
            float timeToLive = 2.0f;
            timeToLive -= Time.deltaTime;
            if (timeToLive == 0.0f)
            {
                Destroy(this);
            }
        }
    }

}