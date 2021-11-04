using System.Collections.Generic;

namespace MinivillesURSR46
{
    //Classe qui instancie et gère la main d'un joueur
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

        /// <summary>
        /// Fonction qui permet de changer le nom des cartes par la version URSS
        /// </summary>
        public void nameChange()
        {
            foreach (CardsInfo card in UserHand)
            {
                card.Name = card.NameURSS;
            }
        }

        /// <summary>
        /// Fonction qui gère l'achat d'une carte par un joueur
        /// </summary>
        /// <param name="card"></param>
        /// <param name="pile"></param>
        public void BuyCard(CardsInfo card, Piles pile)
        {
            UserMoney -= card.Cost;
            UserHand.Add(pile.GetCard(card.Id));
        }
        
        /// <summary>
        /// Fonction qui retourne le nombre de carte similaire dans une main
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int GetNumberCard(int ID)
        {
            int cpt = 0;

            foreach(CardsInfo c in UserHand)
            {
                if (c.Id == ID){ cpt++; }
            }

            return cpt;
        }

        /// <summary>
        /// Fonction qui retourne le nombre de cartes différentes dans une main
        /// </summary>
        /// <returns></returns>
        public int GetNumberCardType()
        {
            int cpt = 0;
            List<int> IdCards = new List<int>();

            foreach(CardsInfo c in UserHand)
            {
                if (!IdCards.Contains(c.Id))
                {
                    cpt++;
                    IdCards.Add(c.Id);
                }
            }

            return cpt;
        }
    }
}