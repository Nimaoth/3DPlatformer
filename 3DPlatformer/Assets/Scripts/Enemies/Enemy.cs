using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, TRInterface
{

    public float Speed = 3;
    public bool Invincible;
    public float BumpSpeed = 5;

    private new Rigidbody rigidbody;
    private TimeReverse timeReverse;

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();	
	}

    void Start()
    {
        timeReverse = GetComponent<TimeReverse>();
    }

	void FixedUpdate()
	{
	    if (GameData.Instance.Paused && timeReverse != null)
	        return;

        if (!rigidbody.isKinematic)
            rigidbody.velocity = new Vector3(Speed, rigidbody.velocity.y, 0);
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "End")
        {
            Speed *= -1;
        }
    }

    public void OnDeath()
    {
        GetComponent<Collider>().enabled = false;
        rigidbody.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
    }

    public void SaveTRObject()
    {
        timeReverse.PushTRObject(new Status
        {
            position = transform.position,
            rotation = transform.rotation,
            speed = Speed,
            bumpSpeed = BumpSpeed,
            enabled = gameObject.GetComponent<Collider>().enabled,
            invincible = Invincible,
        });
        rigidbody.isKinematic = false;
    }

    public void LoadTROBject(TRObject trObject)
    {
        Status status = trObject as Status;
        transform.position = status.position;
        transform.rotation = status.rotation;
        BumpSpeed = status.bumpSpeed;
        gameObject.GetComponent<Collider>().enabled = status.enabled;
        Invincible = status.invincible;
        Speed = status.speed;

        rigidbody.isKinematic = true;
    }

    private class Status : TRObject
    {
        public Vector3 position;
        public Quaternion rotation;
        public float speed;
        public float bumpSpeed;
        public bool enabled;
        public bool invincible;
    }
}
