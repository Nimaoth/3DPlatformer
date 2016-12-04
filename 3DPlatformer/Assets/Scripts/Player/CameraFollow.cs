using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public float distanceAway;
    public float distanceUp;
    public float smooth;
    public Transform followedObject;
    private Vector3 toPosition;

    private float maxDistance = 3;
    private float minDistance = 1;

    void LateUpdate()
    {
        toPosition = followedObject.position + Vector3.up * distanceUp - followedObject.forward * distanceAway;
        //Vector3 dist = transform.position - toPosition;

        //float mag = dist.magnitude;
        //if (mag < minDistance)
        //{
        //    dist *= minDistance / mag;
        //    toPosition = toPosition + dist;
        //}
        //else if (mag > maxDistance)
        //{
        //    dist *= maxDistance / mag;
        //    toPosition = toPosition + dist;
        //}
        //else
        //    toPosition = transform.position;

        transform.position = Vector3.Lerp(transform.position, toPosition, Time.deltaTime * smooth);
        transform.LookAt(followedObject);
    }
}