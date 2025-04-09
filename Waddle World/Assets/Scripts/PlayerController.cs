using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    [SerializeField] private CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale;

    [SerializeField] public Animator anim;

    // Knockback Stuff

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    // Rotating the Player during Movement

    [SerializeField] private GameObject playerModel;
    [SerializeField] private Transform pivot;
    public float rotateSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Animations();
        
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

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed*Time.deltaTime);
        }
    }

    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }

    public void SpringLaunch(Vector3 direction, int springForce)
    {
        moveDirection = direction * springForce;
        moveDirection.y = springForce;
    }

    private void Animations()
    {
        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical"))+Mathf.Abs(Input.GetAxis("Horizontal"))));
    }
}
