using System;
using System.Collections;
using System.Text;
using UnityEngine;

/// <summary>
/// Gère la connexion réseau
/// </summary>
public class GameNetworkManager : MonoBehaviour
{
    #region Singleton

    public static GameNetworkManager Instance = null;

    #endregion

    #region Evénements

    public event EventHandler<GameNetworkErrorArgs> ErrorRaised;
    public event EventHandler<GameNetworkMessageArgs> MessageReceived;

    #endregion

    #region Variables Unity

    [Header("Paramétrage")]
    public string Url = "http://localhost:83/";
    public float TimeRefresh = 0.5f;

    [Header("Modes")]
    public bool IsGearVR = false;
    public bool IsHololens = false;

    #endregion

    #region Variables d'instances

    private bool _inProgressGVR;
    private bool _inProgressHololens;
    private float _timeGVR;
    private float _timeHololens;

    #endregion

    #region Fonction publiques

    /// <summary>
    /// Envoi un message pour le GearVR
    /// </summary>
    /// <param name="message">Message a envoyer</param>
    public void SendToGVR(string message)
    {
        this.StartCoroutine(this.SendMessageToGVR(message));
    }

    /// <summary>
    /// Envoi un message pour Hololens
    /// </summary>
    /// <param name="message">Message a envoyer</param>
    public void SendToHololens(string message)
    {
        this.StartCoroutine(this.SendMessageToHololens(message));
    }

    #endregion

    #region Fonctions Unity

    /// <summary>
    /// Initialise le script
    /// </summary>
    private void Start()
    {
        GameNetworkManager.Instance = this;

        this._inProgressGVR = false;
        this._inProgressHololens = false;
        this._timeGVR = 0f;
        this._timeHololens = 0f;
    }

    /// <summary>
    /// Met à jour la logique
    /// </summary>
    private void Update()
    {
        // Gestion pour le GearVR

        if (this.IsGearVR && !this._inProgressGVR)
        {
            this._timeGVR += Time.deltaTime;

            if (this._timeGVR > this.TimeRefresh)
            {
                this._timeGVR = 0f;
                this._inProgressGVR = true;
                this.StartCoroutine(this.FlushGVR());
            }
        }

        // Gestion pour l'Hololens

        if (this.IsHololens && !this._inProgressHololens)
        {
            this._timeHololens += Time.deltaTime;

            if (this._timeHololens > this.TimeRefresh)
            {
                this._timeHololens = 0f;
                this._inProgressHololens = true;
                this.StartCoroutine(this.FlushHololens());
            }
        }
    }

    #endregion

    #region Fonction privées

    /// <summary>
    /// Filtre le retour http
    /// </summary>
    /// <param name="message">Message a filtrer</param>
    /// <returns>Message filtré</returns>
    private string FilterResponse(string message)
    {
        int index = message.IndexOf(">");
        int indexNext = message.IndexOf("<", index);

        if ((indexNext - index - 1) < 0)
            return (string.Empty);

        if (index < 0 || indexNext < 0 || indexNext > (index + 1))
            message = message.Substring(index + 1, indexNext - index - 1);
        else
            return (string.Empty);

        return (message);
    }

    /// <summary>
    /// récupère les infos pour le GearVR
    /// </summary>
    /// <returns>Enumérateur de position</returns>
    private IEnumerator FlushGVR()
    {
        // Création service et accès

        WWW www = new WWW(this.Url + "MailboxService.svc/FlushGVR");
        yield return www;

        try
        {
            // Récupérationt de la réponse

            string response = Encoding.UTF8.GetString(www.bytes);
            response = this.FilterResponse(response);

            // On envoi les message

            this.ParseAndSend(response);
        }
        catch (Exception ex)
        {
            if (this.ErrorRaised != null)
                this.ErrorRaised(this, new GameNetworkErrorArgs(ex));
        }
        finally
        {
            this._inProgressGVR = false;
        }
    }

    /// <summary>
    /// récupère les infos pour le GearVR
    /// </summary>
    /// <returns>Enumérateur de position</returns>
    private IEnumerator FlushHololens()
    {

        // Création service et accès

        WWW www = new WWW(this.Url + "MailboxService.svc/FlushHololens");
        yield return www;

        try
        {
            // Récupérationt de la réponse

            string response = Encoding.UTF8.GetString(www.bytes);
            response = this.FilterResponse(response);

            // On envoi les message

            this.ParseAndSend(response);
        }
        catch (Exception ex)
        {
            if (this.ErrorRaised != null)
                this.ErrorRaised(this, new GameNetworkErrorArgs(ex));
        }
        finally
        {
            this._inProgressHololens = false;
        }
    }

    /// <summary>
    /// Détermine le message et envoi l'information
    /// </summary>
    /// <param name="response">Réponse à traiter</param>
    private void ParseAndSend(string response)
    {
        try
        {
            // Vérification

            if (string.IsNullOrEmpty(response))
                return;

            // Extraction des différents message

            string[] split = response.Split('#');

            if (split.Length > 0)
            {
                foreach (string item in split)
                {
                    if (GameNetworkParser.FindType(item) != GameMessageTypes.Unknow)
                    {
                        if (this.MessageReceived != null)
                            this.MessageReceived(this, new GameNetworkMessageArgs(item));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (this.ErrorRaised != null)
                this.ErrorRaised(this, new GameNetworkErrorArgs(ex));
        }
    }

    /// <summary>
    /// Envoi un message au GearVR
    /// </summary>
    /// <param name="message">Message a envoyer</param>
    private IEnumerator SendMessageToGVR(string message)
    {
        // Création service et accès

        WWW www = new WWW(this.Url + "MailboxService.svc/SendMessageToGVR/" + message);
        yield return www;
    }

    /// <summary>
    /// Envoi un message a l'Hololens
    /// </summary>
    /// <param name="message">Message a envoyer</param>
    private IEnumerator SendMessageToHololens(string message)
    {
        // Création service et accès

        WWW www = new WWW(this.Url + "MailboxService.svc/SendMessageToHololens/" + message);
        yield return www;
    }

    #endregion
}
