using UnityEngine;
using System.Collections;

public class FallingPlatform : Platform
{

    public float Delay = 0;

    private bool isFalling;
    private Vector3 velocity;

    void Start()
    {
        isFalling = false;
        velocity = Vector3.zero;
    }

    new void OnCollisionEnter(Collision c)
    {
        base.OnCollisionEnter(c);
        if (c.transform.tag == "Player")
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(Delay);
        isFalling = true;
        yield return null;
    }

    void Update()
    {
        if (isFalling)
        {
            velocity += Physics.gravity * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
    }
}
