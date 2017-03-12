using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyBehaviour : MonoBehaviour {

    private Component _halo;

    public void BehaviourOnGazeEnter()
    {
        _halo.GetType().GetProperty("enabled").SetValue(_halo, true, null);
    }
    public void BehaviourOnGazeLeave()
    {
        _halo.GetType().GetProperty("enabled").SetValue(_halo, false, null);
    }

    void Start()
    {
        _halo = GetComponent("Halo");
        _halo.GetType().GetProperty("enabled").SetValue(_halo, false, null);
    }

    void Update()
    {

    }
}
