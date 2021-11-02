using System;
using System.Collections.Generic;
using System.Resources;

namespace MinivillesURSR46
{
    public class Cards
    {
        public List<CardsInfo> EachCards = new List<CardsInfo>();
        public bool Urss = false;

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
            return new CardsInfo(5, Color.Bleu, 2, "DisneyLand", "Taiga", "Recevez 1 pièces", 5, 1);
        }
        
        public CardsInfo CreateRestaurant()
        {
            return new CardsInfo(6, Color.Rouge, 4, "McDonald", "Le Goum", "Recevez 2 pièce du joueur qui à lancé le dé", 5, 2);
        }
        
        public CardsInfo CreateStade()
        {
            return new CardsInfo(7, Color.Bleu, 6, "Wall Street", "Moscow Exchange", "Recevez 4 pièces", 6, 4);
        }
    }
}