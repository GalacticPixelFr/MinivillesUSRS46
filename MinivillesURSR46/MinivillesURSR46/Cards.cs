using System;
using System.Collections.Generic;
using System.Resources;

namespace MinivillesURSR46
{
    //Classe qui créé les différentes cartes et les instancies dans une liste
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
            EachCards.Add(CreateStation());
            EachCards.Add(CreateCinema());
            EachCards.Add(CreateLegend1());
            EachCards.Add(CreateLegend2());
            
        }

        /// <summary>
        /// Chaque fonctions suivantes permettent la création d'une carte spécifique.
        /// </summary>
        /// <returns></returns>
        public CardsInfo CreateChampDeBle()
        {
            return new CardsInfo(0, Color.Bleu, 1, "Champ de maïs", "Kolkhoze", "Recevez 1 pièce", 1, 1);
        }
        
        public CardsInfo CreateFerme()
        {
            return new CardsInfo(1, Color.Bleu, 2, "Elevage de boeufs", "Sovkhoze", "Recevez 1 pièce", 1, 1);
        }
        
        public CardsInfo CreateBoulangerie()
        {
            return new CardsInfo(2, Color.Vert, 1, "Dough donuts", "Mine de cuivre", "Recevez 2 pièces", 2, 2);
        }
        
        public CardsInfo CreateCafe()
        {
            return new CardsInfo(3, Color.Rouge, 2, "Starbuck", "Perlov Tea House", "Recevez 1 pièce du joueur qui à lancé le dé", 3, 1);
        }
        
        public CardsInfo CreateSuperette()
        {
            return new CardsInfo(4, Color.Vert, 2, "Walmart", "Marché Kolkhoziens", "Recevez 3 pièces", 4, 3);
        }

        public CardsInfo CreateForet()
        {
            return new CardsInfo(5, Color.Bleu, 2, "Parc Forestier", "Taiga", "Recevez 1 pièces", 5, 1);
        }
        
        public CardsInfo CreateRestaurant()
        {
            return new CardsInfo(6, Color.Rouge, 4, "McDonald", "Le Goum", "Recevez 2 pièce du joueur qui à lancé le dé", 5, 2);
        }
        
        public CardsInfo CreateStade()
        {
            return new CardsInfo(7, Color.Bleu, 6, "Wall Street", "Moscow Exchange", "Recevez 4 pièces", 6, 4);
        }

        public CardsInfo CreateStation()
        {
            return new CardsInfo(8, Color.Vert, 4, "Station Nasa", "Station Spoutnik", "Recevez 4 pièces", 4, 4);
        }

        public CardsInfo CreateCinema()
        {
            return new CardsInfo(9, Color.Rouge, 6, "Cinéma", "Opéra", "Recevez 3 pièces du joueur qui à lancé le dé",
                6, 3);
        }

        public CardsInfo CreateLegend1()
        {
            return new CardsInfo(10, Color.Jaune, 10, "Mont Rushmore", "Palais d'Hiver",
                "Recevez 5 pièces dont 3 du joueur qui à lancé le dé", 2, 5);
        }

        public CardsInfo CreateLegend2()
        {
            return new CardsInfo(11, Color.Jaune, 10, "Liberty Statue", "Puit Kola", "Recevez 2 pièces", 1, 2);
        }
    }
}