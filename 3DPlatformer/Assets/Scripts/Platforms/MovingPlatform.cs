using UnityEngine;
using System.Collections;

public class MovingPlatform : Platform {

    public enum PositionType { Vector, Transform };

    public PositionType Type;

    public bool Smooth;
    public float Speed;

    public Vector3 PointA;
    public Vector3 PointB;
    public Transform TransformA;
    public Transform TransformB;

    private float direction;
    private float position;

    void Start()
    {
        if (Type == PositionType.Transform && (TransformA == null || TransformB == null))
        {
            Debug.LogError("No Transform defined!");
            Type = PositionType.Vector;
        }

        direction = 1;
    }
	
	// Update is called once per frame
	void Update () {
        position += direction * Speed * Time.deltaTime;
        float distance = 1;

	    switch (Type)
        {
            case PositionType.Vector:
                distance = (PointB - PointA).magnitude;
                transform.position = Lerp(PointA, PointB, position / distance, Smooth);
                break;
            case PositionType.Transform:
                distance = (TransformA.position - TransformB.position).magnitude;
                transform.position = Lerp(TransformA.position, TransformB.position, position / distance, Smooth);
                break;
        }

        if (direction > 0 && position > distance)
        {
            direction = -direction;
            position = distance;
        }
        else if (direction < 0 && position < 0)
        {
            direction = -direction;
            position = 0;
        }
	}

    private Vector3 Lerp(Vector3 a, Vector3 b, float f, bool smooth)
    {
        if (smooth)
        {
            f = 1 - (Mathf.Cos(f * 2.0f * Mathf.PI) * 0.5f + 0.5f);
        }
        return Vector3.Lerp(a, b, f);
    }

    
}
