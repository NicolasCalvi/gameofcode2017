/// <summary>
/// Permet de décoder les messages envoyées
/// </summary>
public static class GameNetworkParser
{
    #region Fonctions publiques

    /// <summary>
    /// Génère un message de détail endpoint
    /// </summary>
    /// <param name="endpoint">Endpoint a envoyer</param>
    /// <returns>Message construit</returns>
    public static string CreateMessageClientServerDetail(GameNetworkEndpoint endpoint)
    {
        if (endpoint == null)
            return (string.Empty);

        return (string.Format("{0}|{1}|{2}", (int)GameMessageTypes.ClientServerDetail, endpoint.Server, endpoint.Port));
    }

    /// <summary>
    /// Génère un message pour le passage d'objet dans la boit aux lettres
    /// </summary>
    /// <param name="item">Objet a passer</param>
    /// <returns>Message construit</returns>
    public static string CreateMessageSendObjectInMailbox(MailboxItems item)
    {
        if (item == MailboxItems.None)
            return (string.Empty);

        return (string.Format("{0}|{1}", (int)GameMessageTypes.SendObjectInMailbox, (int)item));
    }

    /// <summary>
    /// Décode le message ClientServerDetail
    /// </summary>
    /// <param name="message">Message a décoder</param>
    /// <returns>Instance de l'endpoint</returns>
    public static GameNetworkEndpoint DecodeClientServerDetail(string message)
    {
        // On décode la chaine

        string[] data = GameNetworkParser.Parse(message);

        if (data == null || data.Length != 3)
            return (null);

        // Décodage

        int port;

        if (!int.TryParse(data[2], out port))
            return (null);

        GameNetworkEndpoint endpoint = new GameNetworkEndpoint(data[1], port);

        // On renvoi l'endpoint

        return (endpoint);
    }

    /// <summary>
    /// Décode le message SendObjectInMailbox
    /// </summary>
    /// <param name="message">Message a décoder</param>
    /// <returns>Objet en transit</returns>
    public static MailboxItems DecodeSendObjectInMailbox(string message)
    {
        // On décode la chaine

        string[] data = GameNetworkParser.Parse(message);

        if (data == null || data.Length != 2)
            return (MailboxItems.None);

        // Décodage

        int itemId;

        if (!int.TryParse(data[1], out itemId))
            return (MailboxItems.None);

        // On renvoi l'item

        if (itemId == 1)
            return (MailboxItems.Menu);
        else if (itemId == 2)
            return (MailboxItems.Screeen);
        else if (itemId == 3)
            return (MailboxItems.Key1);
        else if (itemId == 4)
            return (MailboxItems.UsbKey);
        else if (itemId == 5)
            return (MailboxItems.Key2);
        else if (itemId == 6)
            return (MailboxItems.DoorHandle);

        return (MailboxItems.None);
    }

    /// <summary>
    /// Récupère le type du message
    /// </summary>
    /// <param name="message">Message a décoder</param>
    /// <returns>Type du message</returns>
    public static GameMessageTypes FindType(string message)
    {
        // On décode la chaine

        string[] data = GameNetworkParser.Parse(message);

        if (data == null)
            return (GameMessageTypes.Unknow);

        // On détermine le type

        int type;

        if (!int.TryParse(data[0], out type))
            return (GameMessageTypes.Unknow);

        // On filtre le type

        if (type == 1)
            return (GameMessageTypes.ClientServerDetail);
        else if (type == 2)
            return (GameMessageTypes.SendObjectInMailbox);

        // Message inconnu

        return (GameMessageTypes.Unknow);
    }

    #endregion

    #region Fonctions privées

    /// <summary>
    /// Décode la chaine
    /// </summary>
    /// <param name="message">Message d'origine</param>
    /// <returns>Chaine décodé, NULL sinon</returns>
    private static string[] Parse(string message)
    {
        if (string.IsNullOrEmpty(message))
            return (null);

        string[] parsed = message.Split('|');

        if (parsed.Length < 2)
            return (null);

        return (parsed);
    }

    #endregion
}