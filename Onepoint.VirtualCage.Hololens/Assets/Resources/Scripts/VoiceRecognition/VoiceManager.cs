using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// Manager pour la reconnaissance vocale
/// </summary>
public class VoiceManager : MonoBehaviour
{
    #region Evénement

    public event EventHandler<VoiceServiceEventArgs> MenuSelected;

    #endregion

    #region Singletons

    /// <summary>
    /// Obtient le singleton
    /// </summary>
    public static VoiceManager Instance
    {
        get;
        private set;
    }

    #endregion

    #region Constantes

    private static readonly string WAKE_UP_KEYWORD = "Menu ";
    private static readonly string FILTER_KEYWORD = " Filter";

    #endregion

    #region Variables Unity

    [Header("Text Speech")]
    public string MenuRed = "Red";
    public string MenuBlue = "Blue";
    public string MenuYellow = "Yellow";
    public string MenuReset = "Reset";

    #endregion

    #region Variables d'instances

    private KeywordRecognizer _keywordRecognizer;
    private Dictionary<string, string> _keywordsList;


    #endregion

    #region Fonctions Unity

    /// <summary>
    /// Initialise le script
    /// </summary>
    private void Start()
    {
        // Affectation singleton

        VoiceManager.Instance = this;

        // Création des mots a chercher

        this._keywordsList = new Dictionary<string, string>();
        this._keywordsList.Add(VoiceManager.WAKE_UP_KEYWORD + this.MenuRed , this.MenuRed);
        this._keywordsList.Add(VoiceManager.WAKE_UP_KEYWORD + this.MenuBlue , this.MenuBlue);
        this._keywordsList.Add(VoiceManager.WAKE_UP_KEYWORD + this.MenuYellow , this.MenuYellow);
        this._keywordsList.Add(VoiceManager.WAKE_UP_KEYWORD + this.MenuReset, this.MenuReset);

        // Transfert des référence

        string[] keywordListString = new string[this._keywordsList.Count];

        this._keywordsList.Keys.CopyTo(keywordListString, 0);

        // Création du moteur de reconnaissance vocale

        this._keywordRecognizer = new KeywordRecognizer(keywordListString);
        this._keywordRecognizer.OnPhraseRecognized += this.OnPhraseRecognized;
        this._keywordRecognizer.Start();
    }

    #endregion

    #region Fonctions privées

    /// <summary>
    /// Callback de la détection vocale
    /// </summary>
    /// <param name="args">Argument de la détection</param>
    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (this._keywordsList.ContainsKey(args.text))
        {
            if (this.MenuSelected != null)
                this.MenuSelected(this, new VoiceServiceEventArgs(this._keywordsList[args.text]));
        }
    }

    #endregion
}