namespace Onepoint.VirtualCage.Server
{
    /// <summary>
    /// Comptoir pour Virtual Cage
    /// </summary>
    public class MailboxService : IMailboxService
    {
        /// <summary>
        /// Efface les liste
        /// </summary>
        public void Clear()
        {
            GameStates.Clear();
        }

        /// <summary>
        /// Récupère la pile de message GearVR
        /// </summary>
        /// <returns>Pile de message</returns>
        public string FlushGVR()
        {
            return (GameStates.DumpGVRList());
        }

        /// <summary>
        /// Récupère la pile de message Hololens
        /// </summary>
        /// <returns>Pile de message</returns>
        public string FlushHololens()
        {
            return (GameStates.DumpHololensList());
        }

        /// <summary>
        /// Envoi un message au GearVR
        /// </summary>
        /// <param name="message">Message a envoyer</param>
        public void SendMessageToGVR(string message)
        {
            GameStates.AddMessageToGVR(message);
        }

        /// <summary>
        /// Envoi un message a l'hololens
        /// </summary>
        /// <param name="message">Message a envoyer</param>
        public void SendMessageToHololens(string message)
        {
            GameStates.AddMessageToHololens(message);
        }
    }
}
