using UnityEngine;
using System.Collections.Generic;

public class TimeReverse : MonoBehaviour
{
    private Stack<TRObject> trObjects = new Stack<TRObject>();
    private TRInterface trInterface;

    void Start()
    {
        trInterface = (TRInterface) gameObject.GetComponent(typeof(TRInterface));
    }

    void FixedUpdate()
    {
        if (Input.GetButton("TimeControl"))
        {
            if (trObjects.Count > 0)
            {
                trInterface.LoadTROBject(trObjects.Pop());
            }
        }
        else
        {
            trInterface.SaveTRObject();
        }
    }

    public void PushTRObject(TRObject trObject)
    {
        trObjects.Push(trObject);
    }
}