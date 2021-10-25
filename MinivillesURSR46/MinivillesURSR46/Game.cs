using System;
using System.Collections;
using System.Collections.Generic;

namespace MinivillesURSR46
{
    public class Game
    {
        public Player playerH;
        public Player playerIA;
        public Die die;
        public Piles pile;
        public int gainFinish; // en prévision du bonus changer condition fin pour partie rapide/normal/lente

        // screen
        public Screen screen;

        private Random rnd;

        public Game(int gain)
        {
            //TODO initialisation pile de cartes
            pile = new Piles();
            for(int i = 0; i < 6; i++)
            {
                foreach(CardsInfo c in new Cards().EachCards)
                {
                    pile.AddCard(c);
                }
            }

            gainFinish = gain;

            playerH = new Player(new List<CardsInfo>(), pile);
            playerIA = new Player(new List<CardsInfo>(), pile);

            die = new Die();
            rnd = new Random();
        } 

        public void DisplayGame()
        {
            // TODO
        }

        public void Run()
        {
            //TODO affichage de base : credits, noms du jeu, etc.

            /*
            1. Le joueur A lance le dé.
            2. Le joueur B regarde s’il a des cartes bleues ou rouges qui s’activent et il en applique les effets
            3. Le joueur A regarde s’il a des cartes bleues ou vertes qui s’activent et il en applique les effets
            4. Le joueur A peut acheter une nouvelle carte et l’ajouter à sa ville. Il est possible d’avoir plusieurs fois la même carte, les effets s’additionnent.
            Une fois le tour du joueur A terminé, c’est au tour du joueur B de réaliser les mêmes actions.
            */

            // condition fin
            while (playerH.argent < gainFinish && playerIA.argent < gainFinish)
            {
                // tour joueur humain
                // TODO attendre pression touche ?
                die.Lancer();
                // TODO corriger fonction activation cartes joueur selon couleur
                //IA bleue et rouge
                //H bleue et vert

                // choix action joueur
                int choix = 0;
                // TODO choix achat/rien
                if (choix == 1)
                {
                    choix = 0;
                    // TODO choix entre les différentes cartes
                    // TODO condition (playerH.argent > cartes.cost)
                    playerH.Achat(choix);
                }

                // verification condition de fin
                if(playerH.argent < gainFinish && playerIA.argent < gainFinish){ break;}

                // tour joueur IA
                die.Lancer();
                // TODO corriger fonction activation cartes joueur selon couleur
                //H bleue et rouge
                //IA bleue et vert

                // achat de cartes au hasard
                if (rnd.Next(0, 1) == 0 && playerIA.argent > 0)
                {
                    // TODO chois d'une carte a acheter au hasard
                    // TODO verifier que l'on peut acheter la carte
                }

            }
            // TODO nettoyer screen
            // TODO afficher défaite ou vistoire selon situation
        }
    }
}