using System;

/// <summary>
/// Classe d'argument pour la reconnaissance vocale d'un menu
/// </summary>
public class VoiceServiceEventArgs: EventArgs
{
    #region Propriétés

    /// <summary>
    /// Obtient le menu sélectionné
    /// </summary>
    public string Menu
    {
        get;
        private set;
    }

    #endregion

    #region Constructeurs

    /// <summary>
    /// Constructeur de la classe
    /// </summary>
    /// <param name="menu">Menu sélectionnée</param>
    public VoiceServiceEventArgs(string menu) 
        : base()
    {
        this.Menu = menu;
    }

    #endregion
}
