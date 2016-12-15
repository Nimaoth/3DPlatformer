using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Transform PlayerTransform;
    public float GameOverHeight = -10;

	// Use this for initialization
	void Start () {
        GameData.Instance.LivesChanged += (lives, old) =>
        {
            Debug.Log("Lives: " + old + " -> " + lives);
        };

        GameData.Instance.ScoreChanged += (score, old) =>
        {
            Debug.Log("Score: " + old + " -> " + score);
        };
    }
	
	// Update is called once per frame
	void Update () {
	}
}
