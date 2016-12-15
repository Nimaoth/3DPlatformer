using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public virtual void OnCollisionEnter(Collision c)
    {
        if (c.transform.tag == "Player")
        {
            c.transform.parent = transform;
        }
    }

    public virtual void OnCollisionExit(Collision c)
    {
        if (c.transform.tag == "Player")
        {
            c.transform.parent = null;
        }
    }
}
