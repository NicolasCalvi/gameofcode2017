using System;

/// <summary>
/// Classe d'argument pour la réception d'un message réseau
/// </summary>
public class GameNetworkMessageArgs : EventArgs
{
    #region Propriétés

    /// <summary>
    /// Obtient le message reçu
    /// </summary>
    public string Message
    {
        get;
        private set;
    }

    #endregion

    #region Constructeur

    /// <summary>
    /// Constructeur de la classe
    /// </summary>
    /// <param name="message">Message reçu</param>
    public GameNetworkMessageArgs(string message)
    {
        this.Message = message;
    }

    #endregion
}