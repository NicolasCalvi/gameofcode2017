using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadBigSceneClick : MonoBehaviour 
{
    void OnClick()
    {
        // load the big scene level
        SceneManager.LoadScene("Demo - Big Scene - Loaded by Script");
    }
}
