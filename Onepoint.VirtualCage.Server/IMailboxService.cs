using System.ServiceModel;
using System.ServiceModel.Web;

namespace Onepoint.VirtualCage.Server
{
    /// <summary>
    /// Interface des services
    /// </summary>
    [ServiceContract]
    public interface IMailboxService
    {
        /// <summary>
        /// Efface les liste
        /// </summary>
        [OperationContract]
        [WebGet(UriTemplate = "Clear", ResponseFormat = WebMessageFormat.Xml)]
        void Clear();

        /// <summary>
        /// Récupère la pile de message GearVR
        /// </summary>
        /// <returns>Pile de message</returns>
        [OperationContract]
        [WebGet(UriTemplate = "FlushGVR", ResponseFormat = WebMessageFormat.Xml)]
        string FlushGVR();

        /// <summary>
        /// Récupère la pile de message Hololens
        /// </summary>
        /// <returns>Pile de message</returns>
        [OperationContract]
        [WebGet(UriTemplate = "FlushHololens", ResponseFormat = WebMessageFormat.Xml)]
        string FlushHololens();

        /// <summary>
        /// Envoi un message au GearVR
        /// </summary>
        /// <param name="message">Message a envoyer</param>
        [OperationContract]
        [WebGet(UriTemplate = "SendMessageToGVR/{message}", ResponseFormat = WebMessageFormat.Xml)]
        void SendMessageToGVR(string message);

        /// <summary>
        /// Envoi un message a l'hololens
        /// </summary>
        /// <param name="message">Message a envoyer</param>
        [OperationContract]
        [WebGet(UriTemplate = "SendMessageToHololens/{message}", ResponseFormat = WebMessageFormat.Xml)]
        void SendMessageToHololens(string message);
    }
}
