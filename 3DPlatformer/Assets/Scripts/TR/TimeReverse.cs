using UnityEngine;
using System.Collections.Generic;

public class TimeReverse : MonoBehaviour
{
    private CircularBuffer<TRObject> trObjects;
    private TRInterface trInterface;

    public float Seconds = 1;

    void Start()
    {
        float fixedUpdatesPerSecond = 1 / Time.fixedDeltaTime;
        trObjects = new CircularBuffer<TRObject>((int) (Seconds * fixedUpdatesPerSecond));

        trInterface = (TRInterface) gameObject.GetComponent(typeof(TRInterface));
    }

    void FixedUpdate()
    {
        if (Input.GetButton("TimeControl"))
        {
            if (!trObjects.IsEmpty)
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

    private class CircularBuffer <TType>
    {
        const int DEFAULT_BUFFER_SIZE = 128;

        private TType[] objects;
        private int start;
        private int end;

        public bool IsEmpty
        {
            get
            {
                return start == end;
            }
        }

        public int Count
        {
            get
            {
                return (end - start + objects.Length) % objects.Length;
            }
        }

        public CircularBuffer(int size = DEFAULT_BUFFER_SIZE)
        {
            start = end = 0;
            objects = new TType[size];
        }

        public void Push(TType obj)
        {
            objects[end] = obj;
            end = (end + 1) % objects.Length;
            if (end == start)
            {
                start = (start + 1) % objects.Length;
            }
        }

        public TType Pop()
        {
            if (IsEmpty)
                return default(TType);

            end = (end - 1 + objects.Length) % objects.Length;
            return objects[end];
        }

        public void Clear()
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i] = default(TType);
            }

            start = end = 0;
        }
    }
}