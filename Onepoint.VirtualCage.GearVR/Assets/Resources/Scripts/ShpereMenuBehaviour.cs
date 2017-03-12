using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShpereMenuBehaviour : MonoBehaviour {

    public bool Menu;
    public bool Blue;
    public bool Yellow;

    private Component _halo;

    public void BehaviourOnGazeEnter()
    {
        _halo.GetType().GetProperty("enabled").SetValue(_halo, true, null);
    }

    public void BehaviourOnGazeLeave()
    {
        _halo.GetType().GetProperty("enabled").SetValue(_halo, false, null);
    }

    void Start () {
        _halo = GetComponent("Halo");
        _halo.GetType().GetProperty("enabled").SetValue(_halo, false, null);
    }

    public void SendColorMessage()
    {
        if (Menu)
        {
            GameNetworkManager.Instance.SendToHololens(GameNetworkParser.CreateMessageSendObjectInMailbox(MailboxItems.Menu));
            Debug.Log("Menu Envoyé");
        }
        if (Blue)
        {
            GameNetworkManager.Instance.SendToHololens(GameNetworkParser.CreateMessageSendObjectInMailbox(MailboxItems.Key1));
            Debug.Log("Key1 Envoyé");
        }
        if (Yellow)
        {
            GameNetworkManager.Instance.SendToHololens(GameNetworkParser.CreateMessageSendObjectInMailbox(MailboxItems.Key2));
            Debug.Log("Key2 Envoyé");
        }
    }
}
