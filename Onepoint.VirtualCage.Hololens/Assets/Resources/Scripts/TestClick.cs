using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.VR.WSA.Input;

public class TestClick : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GestureRecognizer test = new GestureRecognizer();
        test.TappedEvent += Test_TappedEvent;
        test.StartCapturingGestures();
    }

    private void Test_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        this.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
