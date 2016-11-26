using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    #region Public Variables

    public MoveSettings MoveSettings;
    public InputSettings InputSettings;

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
    }

    void FixedUpdate()
    {
        Run();
        Jump();
    }

    void GetInput()
    {
        if (InputSettings.FORWARD_AXIS.Length != 0)
            forwardInput = Input.GetAxis(InputSettings.FORWARD_AXIS);

        if (InputSettings.SIDEWAYS_AXIS.Length != 0)
            sidewaysInput = Input.GetAxis(InputSettings.SIDEWAYS_AXIS);

        if (InputSettings.TURN_AXIS.Length != 0)
            turnInput = Input.GetAxis(InputSettings.TURN_AXIS);

        if (InputSettings.JUMP_AXIS.Length != 0)
            jumpInput = (int) Input.GetAxisRaw(InputSettings.JUMP_AXIS);
    }

    void Run()
    {
        velocity.z = forwardInput * MoveSettings.RunVelocity;
        velocity.x = sidewaysInput * MoveSettings.RunVelocity;
        velocity.y = playerRigidbody.velocity.y;
        playerRigidbody.velocity = transform.TransformDirection(velocity);
    }

    void Turn()
    {
        if (Mathf.Abs(turnInput) > 0)
        {
            targetRotation *= Quaternion.AngleAxis(MoveSettings.RotateVelocity * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }

    void Jump()
    {
        if (jumpInput != 0 && Grounded())
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, MoveSettings.JumpVelocity,
                playerRigidbody.velocity.z);
        }
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, MoveSettings.DistanceToGround, MoveSettings.Ground);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -MoveSettings.DistanceToGround, 0));
    }
}