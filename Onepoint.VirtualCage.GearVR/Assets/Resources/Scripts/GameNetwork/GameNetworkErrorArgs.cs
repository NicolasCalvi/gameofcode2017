using System;

/// <summary>
/// Classe d'argument pour la réception d'une erreur réseau
/// </summary>
public class GameNetworkErrorArgs : EventArgs
{
    #region Propriétés

    /// <summary>
    /// Obtient l'erreur reçue
    /// </summary>
    public Exception Error
    {
        get;
        private set;
    }

    #endregion

    #region Constructeur

    /// <summary>
    /// Constructeur de la classe
    /// </summary>
    /// <param name="error">Erreur reçue</param>
    public GameNetworkErrorArgs(Exception error)
    {
        this.Error = error;
    }

    #endregion
}
