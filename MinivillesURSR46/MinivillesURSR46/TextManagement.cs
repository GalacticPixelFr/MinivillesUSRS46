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
                "OUI",
                "NON"
            }},
            {"EnterDé", new []{"Tapez Enter pour Lancer le dé"}},
            {"NombreDé", new []{"Vous avez fait &."}}
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