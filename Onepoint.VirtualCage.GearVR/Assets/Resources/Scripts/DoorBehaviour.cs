using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour {

    void Start () {

	}
	

	void Update () {
		
	}
    public void BehaviourOnGazeEnter()
    {
        gameObject.transform.localScale = gameObject.transform.localScale + new Vector3(.001f, .001f, 0);
    }

    public void BehaviourOnGazeLeave()
    {
        gameObject.transform.localScale = gameObject.transform.localScale - new Vector3(.001f, .001f, 0);
    }
}
