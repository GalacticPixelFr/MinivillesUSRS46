using System;
using System.Collections.Generic;
using System.Resources;

namespace MinivillesURSR46
{
    public class Cards
    {
        public List<CardsInfo> EachCards = new List<CardsInfo>();

        public Cards()
        {
            EachCards.Add(CreateChampDeBle());
            EachCards.Add(CreateFerme());
            EachCards.Add(CreateBoulangerie());
            EachCards.Add(CreateCafe());
            EachCards.Add(CreateSuperette());
            EachCards.Add(CreateForet());
            EachCards.Add(CreateRestaurant());
            EachCards.Add(CreateStade());

        }

        public CardsInfo CreateChampDeBle()
        {
            return new CardsInfo(0, Color.Bleu, 1, "Champ de Blé", "Recevez 1 pièce", 1, 1);
        }
        
        public CardsInfo CreateFerme()
        {
            return new CardsInfo(1, Color.Bleu, 2, "Ferme", "Recevez 1 pièce", 1, 1);
        }
        
        public CardsInfo CreateBoulangerie()
        {
            return new CardsInfo(2, Color.Vert, 1, "Boulangerie", "Recevez 2 pièces", 2, 2);
        }
        
        public CardsInfo CreateCafe()
        {
            return new CardsInfo(3, Color.Rouge, 2, "Café", "Recevez 1 pièce du joueur qui à lancé le dé", 3, 1);
        }
        
        public CardsInfo CreateSuperette()
        {
            return new CardsInfo(4, Color.Vert, 2, "Superette", "Recevez 3 pièces", 4, 3);
        }

        public CardsInfo CreateForet()
        {
            return new CardsInfo(5, Color.Bleu, 2, "Forêt", "Recevez 1 pièces", 5, 1);
        }
        
        public CardsInfo CreateRestaurant()
        {
            return new CardsInfo(6, Color.Rouge, 4, "Restaurant", "Recevez 2 pièce du joueur qui à lancé le dé", 5, 2);
        }
        
        public CardsInfo CreateStade()
        {
            return new CardsInfo(7, Color.Bleu, 6, "Stade", "Recevez 4 pièces", 6, 4);
        }

        /// <summary>
        /// Permet de renvoyer une liste d'element pour afficher une carte
        /// </summary>
        /// <param name="cardsInfo">Les infos de la carte</param>
        /// <param name="coordinates">les coordonnées de la carte</param>
        /// <returns>liste d'element pour afficher une carte</returns>
        public Element[] ToElementFull(CardsInfo cardsInfo, Coordinates coordinates)
        {
            string[] stringBackground = new string[7]{
                "+---------+",
                "|         |",
                "|         |",
                "|         |",
                "|         |",
                "|         |",
                "+---------+"
            };
            
            ConsoleColor color = ConsoleColor.White;
            switch (cardsInfo.Color)
            {
                case Color.Bleu:
                    color = ConsoleColor.Blue;
                    break;
                case Color.Rouge:
                    color = ConsoleColor.Red;
                    break;
                case Color.Vert:
                    color = ConsoleColor.Green;
                    break;
            }
            Element background = new Element(stringBackground, coordinates, Animation.None, Placement.topLeft, color, ConsoleColor.Black);

            string[] stringInfos = new string[5]{
                cardsInfo.Name,
                "",
                cardsInfo.Cost+" €",
                "",
                cardsInfo.Dice.ToString()
            };
            Element infos = new Element(stringInfos, new Coordinates(coordinates.x+5, coordinates.y+1), Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);

            return new Element[2]{background, infos};
        }

        /// <summary>
        /// Permet d'afficher la moitier d'un carte (pour afficher celle du joueur)
        /// </summary>
        /// <param name="amount">Le nombre de cartes</param>
        /// <param name="cardsInfo">Les infos de la carte</param>
        /// <param name="coordinates">les coordonnées de la carte</param>
        /// <returns></returns>
        public Element[] ToElementSemiTop(bool top, int amount, CardsInfo cardsInfo, Coordinates coordinates)
        {
            string[] stringBackground = new string[4]{
                "|         |",
                "|         |",
                "|         |",
                "+---------+"
            };

            if (top) 
            {
                stringBackground = new string[4]{
                    "+---------+",
                    "|         |",
                    "|         |",
                    "|         |"
                };
            }

            ConsoleColor color = ConsoleColor.White;
            switch (cardsInfo.Color)
            {
                case Color.Bleu:
                    color = ConsoleColor.Blue;
                    break;
                case Color.Rouge:
                    color = ConsoleColor.Red;
                    break;
                case Color.Vert:
                    color = ConsoleColor.Green;
                    break;
            }
            Element background = new Element(stringBackground, coordinates, Animation.None, Placement.topLeft, color, ConsoleColor.Black);

            string[] stringInfos = new string[3]{
                cardsInfo.Name,
                cardsInfo.Dice.ToString(),
                "+"+amount
            };
            Element infos = new Element(stringInfos, new Coordinates(coordinates.x+5, coordinates.y), Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);

            return new Element[2]{background, infos};
        }
    }
}