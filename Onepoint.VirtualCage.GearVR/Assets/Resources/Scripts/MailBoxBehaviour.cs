using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe pour gérer le comportement de la boite aux lettre
public class MailBoxBehaviour : MonoBehaviour {

    #region Public Variables

    public PlayMakerFSM ScreenFSM;
    public PlayMakerFSM Usb1FSM;
    public PlayMakerFSM DoorKeyFSM;

    #endregion

    #region Private Variables

    private PlayMakerFSM _fsm;
    private int _currentFSMState;
    private bool _initialization;

    private MailboxItems _item;

    #endregion

    #region Behaviour On Gaze Functions

    public void BehaviourOnGazeClick()
    {
        _currentFSMState = _fsm.FsmVariables.GetFsmInt("state").Value;
        if (_currentFSMState == 1)
        {
            _fsm.Fsm.Event("LetterBoxClicked");
            ScreenFSM.Fsm.Event("LetterBoxClicked");
        }
        else if (_currentFSMState == 2)
        { 
            _fsm.Fsm.Event("LetterBoxClicked");
            Usb1FSM.Fsm.Event("LetterBoxClicked");
        }
        else if (_currentFSMState == 3)
        {
            _fsm.Fsm.Event("LetterBoxClicked");
            DoorKeyFSM.Fsm.Event("LetterBoxClicked");
        }
    }

    public void BehaviourOnGazeEnter()
    {
        gameObject.transform.localScale = gameObject.transform.localScale + new Vector3(.2f, .2f, 0);
    }

    public void BehaviourOnGazeLeave()
    {
        gameObject.transform.localScale = gameObject.transform.localScale - new Vector3(.2f, .2f, 0);
    }

    #endregion

    void Start () {
        _fsm = GetComponent<PlayMakerFSM>();
        _initialization = false;
        _item = MailboxItems.None;
    }
    private void Update()
    {
        if (!_initialization && GameNetworkManager.Instance != null)
        {
            this._initialization = true;
            GameNetworkManager.Instance.MessageReceived += OnMessageReceived;
            GameNetworkManager.Instance.ErrorRaised += OnErrorRaised;
        }

        if (_item == MailboxItems.Screeen)
        {
            _fsm.Fsm.Event("ScreenSentByHL");
            ScreenFSM.Fsm.Event("ScreenSentByHL");
            _item = MailboxItems.None;
        }

        if (_item == MailboxItems.UsbKey)
        {
            _fsm.Fsm.Event("Usb1SentByHL");
            Usb1FSM.Fsm.Event("Usb1SentByHL");
            _item = MailboxItems.None;
        }

        if (_item == MailboxItems.DoorHandle)
        {
            _fsm.Fsm.Event("DoorKeySentByHL");
            DoorKeyFSM.Fsm.Event("DoorKeySentByHL");
            _item = MailboxItems.None;
        }
    }

    private void OnErrorRaised(object sender, GameNetworkErrorArgs e)
    {
        Debug.LogError("Network : " + e.Error.Message + "\n\r" + e.Error.StackTrace);
    }

    private void OnMessageReceived(object sender, GameNetworkMessageArgs e)
    {
        if (GameNetworkParser.FindType(e.Message) == GameMessageTypes.SendObjectInMailbox)
        {
            _item = GameNetworkParser.DecodeSendObjectInMailbox(e.Message);
        }
        
    }
}

//GameNetworkManager.Instance.SendToHololens(GameNetworkParser.CreateMessageSendObjectInMailbox(MailboxItems.Key1));
