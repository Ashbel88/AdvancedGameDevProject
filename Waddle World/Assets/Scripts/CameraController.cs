using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    public float maxViewAngle;

    public float minViewAngle;

    public bool invertY;


    void Awake()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z - 10);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!useOffsetValues){
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = null;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame

    void LateUpdate()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        pivot.transform.position = target.transform.position;
        //Get the x position of the mouse & rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0);

        //Get the Y position of the mouse & rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        //pivot.Rotate(vertical, 0, 0);
        if(invertY){
            pivot.Rotate(vertical, 0, 0);
        } else{
            pivot.Rotate(-vertical, 0, 0);
        }

        //Limit up/down camera rotation
        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f){
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0,0);
        }

        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle){
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0,0);
        }

        //Move the camera based on the current rotation of the target & the original offset
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

       // transform.position = target.position - offset;

       if(transform.position.y < target.position.y){
           transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);

       }

        transform.LookAt(target);
    }
    
}
