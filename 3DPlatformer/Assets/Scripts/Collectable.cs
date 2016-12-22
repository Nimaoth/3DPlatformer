using UnityEngine;
using System.Collections;
using System;

public class Collectable : MonoBehaviour, TRInterface
{

    public float RotationSpeed = 30;
    public float WaveSpeed = 1;
    public float WaveLength = 1;

    public AnimationCurve CollectMovement;

    private Vector3 startPosition;

    private bool moving;
    private TimeReverse timeReverse;

    private new Collider collider;
    private new Renderer renderer;

    void Start()
    {
        startPosition = transform.position;
        moving = true;

        timeReverse = GetComponent<TimeReverse>();

        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();
    }

	void Update() {
        if (GameData.Instance.Paused && timeReverse != null)
            return;

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

        //Destroy(transform.parent.gameObject);
        renderer.enabled = false;
        collider.enabled = false;

        yield return null;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            StartCoroutine(OnCollect());
        }
    }

    public void SaveTRObject()
    {
        timeReverse.PushTRObject(new Status
        {
            moving = this.moving,
            position = transform.position,
            rotation = transform.rotation,
            enabled = collider.enabled
        });
    }

    public void LoadTROBject(TRObject trObject)
    {
        Status status = trObject as Status;

        this.moving = status.moving;
        transform.position = status.position;
        transform.rotation = status.rotation;
        collider.enabled = status.enabled;
        renderer.enabled = status.enabled;
    }

    private class Status : TRObject
    {
        public bool moving;
        public Vector3 position;
        public Quaternion rotation;
        public bool enabled;
    }
}
