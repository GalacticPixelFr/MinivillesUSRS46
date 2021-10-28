using System;
using System.Collections.Generic;

namespace MinivillesURSR46
{
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
            {"NombreDé", new []{"Vous avez fait &."}},
            {"Revenu", new []{"Vos cartes vous rapportent & d’argent."}},
            {"RevenuIa", new []{"Les cartes de votre adversaire rapportent & d'argent."}},
            {"Achat", new []{"Voulez vous acheter ?"}},
            {"CarteAchat", new []{"Vous avez acheté la carte &."}},
            {"Indisponible", new []{"La carte n'est plus disponible."}},
            {"IaDé", new []{"Votre adversaire lance le dé, il a fait &."}},
            {"IaAchat", new []{"Votre adversaire à décidé d'acheter une carte."}},
            {"IaCarteAchat", new []{"Votre adversaire à acheté la carte &."}},
            {"ZeroArgent", new []{"Vous n'avez pas assez d'argent"}},
            {"Gagné", new []{"Vous gagnez car vous avez le plus d'argent"}},
            {"Perdu", new []{"Vous perdez car l'adversaire est le plus riche"}},
            {"Egalité", new []{"Vous êtes à égalité car vous avez autant d'argent que votre adversaire"}},
            {"", new []{""}},
        };

        public static string[] GetData(string key)
        {
            return chatData[key];
        }
        
        public static string[] GetData(string key, string data)
        {
            string[] array = chatData[key];
            foreach (string text in array)
            {
                text.Replace("&", data);
            }

            return array;
        }
    }
}