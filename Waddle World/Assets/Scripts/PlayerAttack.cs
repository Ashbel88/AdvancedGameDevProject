// Evan Stark - May 8th 2025 - ITCS 4231 001
// Player attack scripting.

/*
SOURCES USED
https://docs.unity3d.com/ScriptReference/Input.GetButtonDown.html
https://docs.unity3d.com/6000.0/Documentation/ScriptReference/KeyCode.html
https://www.youtube.com/watch?v=IwzLvJtlJxI
*/

using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attackCooldown = 1.0f;
    private bool canAttack;
    [SerializeField] public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If the left mouse button is pressed and the player can attack, 
        // implement attacking logic. 
        if (Input.GetKeyDown(KeyCode.E) && canAttack == true)
        {
            anim.SetBool("attacking", true);
            canAttack = false;
        }

        if (canAttack == false)
        {
            attackCooldown -= (attackCooldown * Time.deltaTime);
            anim.SetBool("attacking", false);
            if (attackCooldown <= 0)
            {
                Debug.Log("cooldown = 0");
                canAttack = true;
                attackCooldown = 1.0f;
            }
        }
    }

    // Attacking logic.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && canAttack == true)
        {
            Animator enemyAnim = other.GetComponent<Animator>();
            enemyAnim.SetTrigger("die");
        }
    }
}
