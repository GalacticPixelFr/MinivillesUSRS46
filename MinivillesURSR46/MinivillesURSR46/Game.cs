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
            while (playerH.UserMoney < gainFinish && playerIA.UserMoney < gainFinish)
            {
                int resultDie;
                // tour joueur humain
                while (true)
                {
                    Console.WriteLine("Tapez Enter pour Lancer le dé");
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Enter || key == ConsoleKey.Spacebar)
                    {
                        resultDie = die.Lancer();
                        break;
                    }
                }
                CardsActivation(playerH, playerIA, resultDie);
                //IA bleue et rouge
                //H bleue et vert

                // choix action joueur
                bool action = false;
                while (!action)
                {
                    int choix = 0;
                    // TODO choix achat/rien
                    if (choix == 1)
                    {
                        choix = 0;
                        // TODO choix entre les différentes cartes

                        // selection carte choisi
                        CardsInfo c = CardChoice(choix); 

                        // on vérifie que la carte est encore disponible
                        if (pile.GetNumberCard(choix) == 0)
                        {
                            // TODO "la carte n'est plus disponible"
                        }
                        // on vérifie que le joueur a assez d'argent
                        else if (c.Cost < playerH.UserMoney)
                        {
                            playerH.BuyCard(c, pile);
                            action = true;
                        }
                        else
                        {
                            // TODO "vous n'avez pas assez d'argent"
                        }
                    }
                    else { action = true; }
                }

                // verification condition de fin
                if(playerH.UserMoney < gainFinish && playerIA.UserMoney < gainFinish) { break; }

                // tour joueur IA
                resultDie = die.Lancer();
                CardsActivation(playerIA, playerH, resultDie);
                //H bleue et rouge
                //IA bleue et vert

                // cette méthode pose probleme, l'IA va de moins en moins acheter de batiments car il y aura moins de cartes disponbles au fur et a mesure de la partie

                // action au hasard
                if (rnd.Next(0, 1) == 0 && playerIA.UserMoney > 0)
                {
                    // choix d'une carte a acheter au hasard
                    int choix = rnd.Next(0, 8);

                    // selection carte choisi
                    CardsInfo c = CardChoice(choix); 

                    // on vérifie que la carte est encore disponible et qu'elle est encore achetable
                    if (c.Cost < playerH.UserMoney && pile.GetNumberCard(choix) == 0)
                    {
                        playerH.BuyCard(c, pile);
                    }
                }

            }

            // TODO nettoyer screen
            if (playerH.UserMoney > playerIA.UserMoney)
            {
                // TODO "vous gagnez car vous avez le plus d'argent
            }
            else if (playerIA.UserMoney > playerH.UserMoney)
            {
                // TODO "vous perdez car l'adversaire est le plus riche"
            }
            else // egalité des sommes d'argent
            {
                // TODO "vous êtes à égalité car vous avez autant d'argent que votre adversaire
            }
        }

        private CardsInfo CardChoice(int i)
        {
            CardsInfo c;

            if (i == 0) { c = new Cards().CreateBoulangerie(); }
            else if (i == 1) { c = new Cards().CreateCafe(); }
            else if (i == 2) { c = new Cards().CreateChampDeBle(); }
            else if (i == 3) { c = new Cards().CreateFerme(); }
            else if (i == 4) { c = new Cards().CreateForet(); }
            else if (i == 5) { c = new Cards().CreateRestaurant(); }
            else if (i == 6) { c = new Cards().CreateStade(); }
            else { c = new Cards().CreateSuperette(); }

            return c;
        }

        public void CardsActivation(Player userPlayer, Player opponentPlayer, int dice)
        {
            foreach (CardsInfo card in userPlayer.UserHand)
            {
                if (card.Dice == dice)
                {
                    if (card.Color == Color.Bleu || card.Color == Color.Vert )
                    {
                        userPlayer.UserMoney += card.Gain;
                    }
                }
            }
            foreach (CardsInfo card in opponentPlayer.UserHand)
            {
                if (card.Dice == dice)
                {
                    if (card.Color == Color.Bleu)
                    {
                        opponentPlayer.UserMoney += card.Gain;
                    }
                    else if (card.Color == Color.Rouge)
                    {
                        opponentPlayer.UserMoney += card.Gain;
                        userPlayer.UserMoney -= card.Gain;

                    }
                }
            }
        }
    }
}