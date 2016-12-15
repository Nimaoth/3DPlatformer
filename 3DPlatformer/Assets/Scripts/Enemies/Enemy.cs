using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float Speed = 3;
    public bool Invincible;
    public float BumpSpeed = 5;

    private new Rigidbody rigidbody;


    void Awake() {
        rigidbody = GetComponent<Rigidbody>();	
	}
	
	void FixedUpdate() {
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
}
