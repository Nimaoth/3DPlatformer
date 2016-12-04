using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class MoveSettings
    {
        public float RunVelocity = 12;
        public float RotateVelocity = 100;
        public float JumpVelocity = 8;
        public float DistanceToGround = 1.3f;
        public LayerMask Ground;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string FORWARD_AXIS = "Vertical";
        public string SIDEWAYS_AXIS = "Horizontal";
        public string TURN_AXIS = "Mouse X";
        public string JUMP_AXIS = "Jump";
    }

    #region Public Variables

    public MoveSettings moveSettings;
    public InputSettings inputSettings;

    #endregion

    #region Private Variables

    private Rigidbody playerRigidbody;
    private Vector3 velocity;
    private Quaternion targetRotation;
    private float forwardInput;
    private float sidewaysInput;
    private float turnInput;
    private int jumpInput;

    #endregion

    void Awake()
    {
        velocity = Vector3.zero;
        forwardInput = sidewaysInput = turnInput = jumpInput = 0;
        targetRotation = transform.rotation;
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Turn();

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        float t = (transform.position.y - camRay.origin.y) / camRay.direction.y;
        Vector3 lookAt = camRay.GetPoint(t);
        //transform.LookAt(lookAt);

        //RaycastHit hitInfo;
        //if (Physics.Raycast(camRay, out hitInfo, float.MaxValue, LayerMask.GetMask("PlayerMoveDirection")))
        //{
        //    transform.LookAt(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
        //}

        /*

        oy + t * dy = ty
        t * dy = ty - oy
        t = (ty - oy) / dy

        */
    }

    void FixedUpdate()
    {
        Run();
        Jump();
    }

    void GetInput()
    {
        if (inputSettings.FORWARD_AXIS.Length != 0)
            forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS);

        if (inputSettings.SIDEWAYS_AXIS.Length != 0)
            sidewaysInput = Input.GetAxis(inputSettings.SIDEWAYS_AXIS);

        if (inputSettings.TURN_AXIS.Length != 0)
            turnInput = Input.GetAxis(inputSettings.TURN_AXIS);

        if (inputSettings.JUMP_AXIS.Length != 0)
            jumpInput = (int) Input.GetAxisRaw(inputSettings.JUMP_AXIS);
    }

    void Run()
    {                
        velocity.z = forwardInput * moveSettings.RunVelocity;
        velocity.x = sidewaysInput * moveSettings.RunVelocity;
        velocity.y = playerRigidbody.velocity.y;

        velocity = transform.TransformDirection(velocity);

        playerRigidbody.velocity = velocity;
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > 0)
        {
            targetRotation *= Quaternion.AngleAxis(moveSettings.RotateVelocity * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

    void Jump()
    {
        if (jumpInput != 0 && Grounded())
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, moveSettings.JumpVelocity,
                playerRigidbody.velocity.z);
        }
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, moveSettings.DistanceToGround, moveSettings.Ground);
    }
}