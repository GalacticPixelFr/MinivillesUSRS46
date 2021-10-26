using System.Collections.Generic;

namespace MinivillesURSR46
{
    public class Player
    {
        public List<CardsInfo> UserHand = new List<CardsInfo>();

        public int UserMoney = 3;

        public Player(List<CardsInfo> userHand, Piles pile)
        {
            UserHand = userHand;
            UserHand.Add(pile.GetCard(0));
            UserHand.Add(pile.GetCard(2));
        }

        public void BuyCard(CardsInfo card, Piles pile)
        {
            UserMoney -= card.Cost;
            UserHand.Add(pile.GetCard(card.Id));
        }
        
        public int GetNumberCard(int ID)
        {
            int cpt = 0;

            foreach(CardsInfo c in UserHand)
            {
                if (c.Id == ID){ cpt++; }
            }

            return cpt;
        }
    }
}