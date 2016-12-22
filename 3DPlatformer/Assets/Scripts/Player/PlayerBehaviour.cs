using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour, TRInterface
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
    public Transform SpawnPoint;

    #endregion

    #region Private Variables

    private Rigidbody playerRigidbody;
    private Vector3 velocity;
    private Quaternion targetRotation;
    private float forwardInput;
    private float sidewaysInput;
    private float turnInput;
    private int jumpInput;

    private TimeReverse timeReverse;

    #endregion

    void Awake()
    {
        transform.position = SpawnPoint.position;
        transform.rotation = SpawnPoint.rotation;

        velocity = Vector3.zero;
        forwardInput = sidewaysInput = turnInput = jumpInput = 0;
        targetRotation = transform.rotation;
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        timeReverse = GetComponent<TimeReverse>();
    }

    void Update()
    {
        if (GameData.Instance.Paused && timeReverse != null)
            return;

        GetInput();
        Turn();
    }

    void FixedUpdate()
    {
        if (GameData.Instance.Paused && timeReverse != null)
            return;

        if (!playerRigidbody.isKinematic)
        {
            Run();
            Jump();
        }
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

    void Spawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameData.Instance.Score = 0;
        GameData.Instance.Lives = 3;
    }

    void OnDeath()
    {
        if (GameData.Instance.Lives > 0)
        {
            GameData.Instance.Lives--;
            return;
        }

        Spawn();
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Deathzone")
        {
            OnDeath();
        }
    }

    void JumpedOnEnemy(float f)
    {
        playerRigidbody.AddForce(Vector3.up * f, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Enemy")
        {
            Enemy enemy = c.gameObject.GetComponent<Enemy>();
            Collider col = c.gameObject.GetComponent<Collider>();
            Collider mycol = this.gameObject.GetComponent<Collider>();

            if (enemy.Invincible)
            {
                OnDeath();
            }
            else if (mycol.bounds.center.y - mycol.bounds.extents.y > col.bounds.center.y + 0.5f * col.bounds.extents.y)
            {
                GameData.Instance.Score++;
                JumpedOnEnemy(enemy.BumpSpeed);
                enemy.OnDeath();
            }
            else
            {
                OnDeath();
            }
        }
    }

    public void SaveTRObject()
    {
        timeReverse.PushTRObject(new Status
        {
            position = transform.position,
            rotation = transform.rotation,
            targetRotation = this.targetRotation
        });

        playerRigidbody.isKinematic = false;
    }

    public void LoadTROBject(TRObject trObject)
    {
        Status status = trObject as Status;
        transform.position = status.position;
        transform.rotation = status.rotation;
        targetRotation = status.targetRotation;

        playerRigidbody.isKinematic = true;
    }

    private class Status : TRObject
    {
        public Quaternion rotation;
        public Quaternion targetRotation;
        public Vector3 position;
    }
}