using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, transform.localScale);
        Gizmos.DrawIcon(transform.position, "deathzone", false);
    }
}
