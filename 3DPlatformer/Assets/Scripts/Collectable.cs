using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {

    public float RotationSpeed = 30;
    public float WaveSpeed = 1;
    public float WaveLength = 1;

    public AnimationCurve CollectMovement;

    private Vector3 startPosition;

    private bool moving;

    void Start()
    {
        startPosition = transform.position;
        moving = true;
    }

	void Update() {
        if (moving)
        {
            transform.position = startPosition + Vector3.up * WaveLength * Mathf.Sin(WaveSpeed * Time.time);
            transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
        }
	}

    IEnumerator OnCollect()
    {
        moving = false;

        float time = 0;

        while (time <= 1)
        {
            float y = CollectMovement.Evaluate(time);
            transform.position = new Vector3(transform.position.x, startPosition.y + y, transform.position.z);
            time += 0.9f * Time.deltaTime;

            yield return null;
        }

        Destroy(transform.parent.gameObject);
        yield return null;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            StartCoroutine(OnCollect());
        }
    }
}
