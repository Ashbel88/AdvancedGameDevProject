using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    [SerializeField] private CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale;

    // Knockback Stuff

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement(){

        if(knockBackCounter <= 0)
        {
            float storeY = moveDirection.y;
            float movementX = Input.GetAxis("Horizontal");
            float movementZ = Input.GetAxis("Vertical");
            moveDirection = (transform.forward * movementZ) + (transform.right * movementX);
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = storeY;

            if(controller.isGrounded){
                moveDirection.y = 0f;
                if(Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }
        } 
    
        else
        {
          knockBackCounter -= Time.deltaTime;
        }
       
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void Knockback(Vector3 direction)
    {
        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }
}
