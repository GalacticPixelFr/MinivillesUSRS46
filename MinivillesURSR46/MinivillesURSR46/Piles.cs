using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MinivillesURSR46
{
    public class Piles
    {
        public List<CardsInfo> pile;
        private Random rnd = new Random();

        public Piles()
        {
            pile = new List<CardsInfo>();
        }
        public Piles(List<CardsInfo> liste)
        {
            pile = liste;
        }

        /// <summary>
        /// Méthode permettant de mélanger les cartes du paquet
        /// </summary>
        public void Shuffle()
        {
            pile = pile.OrderBy(item => rnd.Next()).ToList();
        }

        /// <summary>
        /// Méthode permettant d'ajouter une carte dans la pile
        /// place la carte en dernière position
        /// </summary>
        /// <param name="c">carte a ajouté à la pile</param>
        public void AddCard(CardsInfo c)
        {
            pile.Add(c);
        }

        /// <summary>
        /// Méthode permettant d'obtenir la première cartes du paquet/de la pile
        /// </summary>
        /// <returns>Renvoi la carte en première position de la pile</returns>
        public CardsInfo GetCard()
        {
            // vérification pile non vide
            if (pile.Count == 0)
                return null;

            CardsInfo c = pile[0];
            pile.RemoveAt(0);
            return c;

        }
        /// <summary>
        /// Surcharge permettant d'obtenir la première carte avec l'ID souhaité
        /// </summary>
        /// <param name="ID">un entier étant l'ID de la carte souhaité</param>
        /// <returns>Renvoi la carte en première position de la pile</returns>
        public CardsInfo GetCard(int ID)
        {
            // on vérifie que la carte recherché existe
            int index = -1;

            for (int i = 0; i < pile.Count; i++)
            {
                if (pile[i].Id == ID)
                {
                    index = i;
                    break;
                }
            }

            // s'il n'y a pas de cartes comme celle recherché
            if (index == -1)
                return null;

            CardsInfo c = pile[index];
            pile.RemoveAt(index);
            return c;
        }

        public int GetNumberCard(int ID)
        {
            int cpt = 0;

            foreach(CardsInfo c in pile)
            {
                if (c.Id == ID){ cpt++; }
            }

            return cpt;
        }
    }
}