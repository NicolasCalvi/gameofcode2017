using System.Collections.Generic;

namespace Onepoint.VirtualCage.Server
{
    /// <summary>
    /// Définit les états de jeu
    /// </summary>
    public static class GameStates
    {
        #region Variables statiques

        private static List<string> _GVRList;
        private static List<string> _HololensList;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de la classe statique
        /// </summary>
        static GameStates()
        {
            GameStates._GVRList = new List<string>();
            GameStates._HololensList = new List<string>();
        }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Ajoute un message à la liste du GearVR
        /// </summary>
        /// <param name="message">message a ajouter</param>
        public static void AddMessageToGVR(string message)
        {
            GameStates._GVRList.Add(message);
        }

        /// <summary>
        /// Ajoute un message à la liste de l'Hololens
        /// </summary>
        /// <param name="message">message a ajouter</param>
        public static void AddMessageToHololens(string message)
        {
            GameStates._HololensList.Add(message);
        }

        /// <summary>
        /// Efface les listes de message
        /// </summary>
        public static void Clear()
        {
            GameStates._GVRList.Clear();
            GameStates._HololensList.Clear();
        }

        /// <summary>
        /// Vide la liste du GearVR
        /// </summary>
        /// <returns>Liste concaténé</returns>
        public static string DumpGVRList()
        {
            string result = string.Empty;

            // Construction liste

            foreach (string item in GameStates._GVRList)
            {
                if (!string.IsNullOrEmpty(result))
                    result += "#";

                result += item;
            }

            // On vide la liste

            GameStates._GVRList.Clear();

            // On retourne la liste

            return (result);
        }

        /// <summary>
        /// Vide la liste de l'Hololens
        /// </summary>
        /// <returns>Liste concaténé</returns>
        public static string DumpHololensList()
        {
            string result = string.Empty;

            // Construction liste

            foreach (string item in GameStates._HololensList)
            {
                if (!string.IsNullOrEmpty(result))
                    result += "#";

                result += item;
            }

            // On vide la liste

            GameStates._HololensList.Clear();

            // On retourne la liste

            return (result);
        }

        #endregion
    }
}