using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControllerScript : MonoBehaviour
{
    #region UNITY ATTRIBUTES
    public PlayMakerFSM PlayMaker;
    public bool IsMenuInside = false;
    public bool IsKey1Inside = false;
    public bool IsKey2Inside = false;
    #endregion

    private bool isMenuVisible;
    private bool _initialized;
    // Use this for initialization
    void Start()
    {
        this._initialized = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this._initialized && VoiceManager.Instance != null)
        {
            VoiceManager.Instance.MenuSelected += this.OnMenuSelected;
            this._initialized = true;
        }

        if (!isMenuVisible)
        {
            //this.PlayMaker.Fsm.Event("TurnMenuOn");
            isMenuVisible = true;
        }
    }

    #region Fonctions privées

    private void OnMenuSelected(object sender, VoiceServiceEventArgs e)
    {
        if (VoiceManager.Instance.MenuRed == e.Menu && this.IsMenuInside)
            this.PlayMaker.Fsm.Event("MenuRed");
        else if (VoiceManager.Instance.MenuBlue == e.Menu && this.IsKey1Inside)
            this.PlayMaker.Fsm.Event("MenuBlue");
        else if (VoiceManager.Instance.MenuYellow == e.Menu && this.IsKey2Inside)
            this.PlayMaker.Fsm.Event("MenuYellow");
        else if (VoiceManager.Instance.MenuReset == e.Menu)
            this.PlayMaker.Fsm.Event("MenuReset");
    }

    #endregion

}
