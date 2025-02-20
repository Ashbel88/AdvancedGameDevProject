using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement(){
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
        

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
