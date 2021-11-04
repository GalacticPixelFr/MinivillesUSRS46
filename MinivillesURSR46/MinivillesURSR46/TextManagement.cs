using System;
using System.Collections.Generic;

namespace MinivillesURSR46
{
    //Classe de stockage des textes du jeu
    public class TextManagement
    {
        static Dictionary<string, string[]> chatData = new Dictionary<string, string[]>()
        {
            {"Accueil", new []
                {
                "Jeu Minivilles",
                "",
                "Créée par :",
                "Jordan BURNET",
                "Mathias DIDIER",
                "Camille PELE"
                }
            },
            {"ChoixDeck", new []{"Quel Deck souhaitez vous prendre ?"}},
            {"booleen", new []
            {
                "NON",
                "OUI"
            }},
            {"EnterDé", new []{"Tapez Enter pour Lancer le dé."}},
            {"NombreDé", new []{"- Vous avez fait &."}},
            {"Revenu", new []{"- Vous gagnez & pièces(s)."}},
            {"RevenuIa", new []{"- Votre adversaire gagne & pièces."}},
            {"Achat", new []{"Voulez vous acheter ?"}},
            {"CarteAchat", new []{"- Vous avez acheté &."}},
            {"Indisponible", new []{"La carte n'est plus disponible."}},
            {"IaDé", new []{"- Votre adversaire lance le dé, il a fait &."}},
            {"IaAchat", new []{"- Votre adversaire à décidé d'acheter une carte."}},
            {"NoIaAchat", new []{"- Votre adversaire n'a rien acheté."}},
            {"IaCarteAchat", new []{"- Votre adversaire à acheté la carte &."}},
            {"ZeroArgent", new []{"Vous n'avez pas assez d'argent"}},
            {"Gagné", new []{"Vous gagnez car vous avez le plus d'argent"}},
            {"Perdu", new []{"Vous perdez car l'adversaire est le plus riche"}},
            {"Egalité", new []{"Vous êtes à égalité car vous avez autant d'argent que votre adversaire"}},
            {"NoAchat", new []{"- Vous avez décidé de ne pas acheter de cartes."}},
            {"TourJ", new []{"                                Tour & - Joueur                                "}},
            {"TourIa", new []{"                                Tour & - IA                                "}},
        };

        /// <summary>
        /// Fonction permettant de récupéré le string[] à partir d'un mot clé
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] GetData(string key)
        {
            return chatData[key];
        }
        
        /// <summary>
        /// Fonction permettant de récupéré le string[] à partir d'un mot clé et d'y changer & par une variable
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string[] GetData(string key, string data)
        {
            List<string> result = new List<string>();
            foreach (string text in chatData[key])
            {
                result.Add(text.Replace("&", data));
            }

            return result.ToArray();
        }

        /// <summary>
        /// Fonction permettant de récupéré le string à partir d'un mot clé
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDataString(string key)
        {
            return chatData[key][0];
        }
        
        /// <summary>
        /// Fonction permettant de récupéré le string à partir d'un mot clé et d'y changer & par une variable
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetDataString(string key, string data)
        {
            List<string> result = new List<string>();
            foreach (string text in chatData[key])
            {
                result.Add(text.Replace("&", data));
            }
            
            return result[0];
        }
    }
}