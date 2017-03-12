/// <summary>
/// Défint l'adresse d'un serveur
/// </summary>
public class GameNetworkEndpoint
{
    #region Propriétés

    /// <summary>
    /// Obtient le port a contacter
    /// </summary>
    public int Port
    {
        get;
        private set;
    }

    /// <summary>
    /// Obtient le serveur a contacter
    /// </summary>
    public string Server
    {
        get;
        private set;
    }

    #endregion

    #region Constructeur

    /// <summary>
    /// Constructeur de la classe
    /// </summary>
    /// <param name="serveur">Addresse du serveur</param>
    /// <param name="port">Port du serveur</param>
    public GameNetworkEndpoint(string server, int port)
    {
        this.Server = server;
        this.Port = port;
    }

    #endregion
}
