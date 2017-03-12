using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBehaviour : MonoBehaviour {

    public void BehaviourOnGazeEnter()
    {
        gameObject.transform.localScale = gameObject.transform.localScale + new Vector3(1f, 1f, 0);
    }

    public void BehaviourOnGazeLeave()
    {
        gameObject.transform.localScale = gameObject.transform.localScale - new Vector3(1f, 1f, 0);
    }
    void Start () {
		
	}
	
	void Update () {
		
	}
}
