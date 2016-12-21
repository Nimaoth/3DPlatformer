using UnityEngine;
using System.Collections;

public class RotatingPlatform : Platform, TRInterface
{

    public Vector3 selfRotationSpeed;
    public Vector2 rotationSpeed;
    public Transform rotationCenter;

    private TimeReverse timeReverse;

	// Use this for initialization
	void Start ()
	{
        if (rotationCenter == null)
            rotationCenter = transform;

	    timeReverse = GetComponent<TimeReverse>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (GameData.Instance.Paused && timeReverse != null)
	        return;

	    if (rotationCenter != transform)
	    {
            Vector3 axis = new Vector3(rotationSpeed.x, rotationSpeed.y, 0);
            Quaternion rot = Quaternion.Euler(axis * Time.deltaTime);

            //Debug.DrawLine(rotationCenter.position, rotationCenter.position + 10 * axis);

            transform.position = rot * (transform.position - rotationCenter.position) + rotationCenter.position;
        }

        Quaternion selfRotation = Quaternion.Euler(selfRotationSpeed * Time.deltaTime);
        transform.rotation *= selfRotation;
	}

    public void SaveTRObject()
    {
        MyStatus status = new MyStatus
        {
            rotation = transform.rotation,
            position = transform.position
        };

        timeReverse.PushTRObject(status);
    }

    public void LoadTROBject(TRObject trObject)
    {
        MyStatus status = trObject as MyStatus;
        transform.rotation = status.rotation;
        transform.position = status.position;
    }

    private class MyStatus : TRObject
    {
        public Quaternion rotation;
        public Vector3 position;
    }
}
