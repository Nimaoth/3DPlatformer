using UnityEngine;
using System.Collections;
using System;

public class CameraFollow : MonoBehaviour
{
    public float distanceAway;
    public float distanceUp;
    public float smooth;
    public Transform followedObject;

    private Vector3 toPosition;

    void LateUpdate()
    {
        toPosition = followedObject.position + Vector3.up * distanceUp - followedObject.forward * distanceAway;
        transform.position = Vector3.Lerp(transform.position, toPosition, Time.deltaTime * smooth);
        transform.LookAt(followedObject);
    }
}