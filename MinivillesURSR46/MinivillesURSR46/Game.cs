using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

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
            screen = new Screen(200, 50);
        }

        public void DisplayMoney()
        {
            screen.Add(new Element(new string[] {playerIA.UserMoney+" pièces", }
                , new Coordinates(1, 1),
                Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black), 1);

            screen.Add(new Element(new string[] {playerH.UserMoney+" pièces", }
                , new Coordinates(1, screen.height-2),
                Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black), 1);
            
            screen.Display();
        }
      
        public void DisplayHands()
        {
            screen.DeleteLayer(3);
            List<int> cards = new List<int>();
            for (int i = 0; i < playerIA.UserHand.Count; i++)
            {
                if (cards.Contains(playerIA.UserHand[i].Id)) continue;
                cards.Add(playerIA.UserHand[i].Id);
                Coordinates coordinates = new Coordinates(screen.width/2-(playerIA.UserHand.Count/2*13) + i*20, +3);
                Element[] elements = playerIA.UserHand[i].ToElementSemi(false, playerIA.GetNumberCard(playerIA.UserHand[i].Id), coordinates);//TODO le nombre de carte
                screen.Add(elements[0], 3);
                screen.Add(elements[1], 3);
            }
            
            cards = new List<int>();
            for (int i = 0; i < playerH.UserHand.Count; i++)
            {
                if (cards.Contains(playerH.UserHand[i].Id)) continue;
                cards.Add(playerH.UserHand[i].Id);
                Coordinates coordinates = new Coordinates(screen.width/2-(playerH.UserHand.Count/2*13) + i*20, screen.height-3);
                Element[] elements = playerH.UserHand[i].ToElementSemi(true,playerH.GetNumberCard(playerH.UserHand[i].Id), coordinates);//TODO le nombre de carte
                screen.Add(elements[0], 3);
                screen.Add(elements[1], 3);
            }
            screen.Display();
        }

        public void Run()
        {
            Element credits = new Element(new string[6] {
                "Jeu Minivilles",
                "",
                "Créée par :",
                "Jordan BURNET",
                "Mathias DIDIER",
                "Camille PELE"
            }, new Coordinates(screen.width/2, screen.height/2), Animation.LetterByLetter, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
            screen.Add(credits, 1);
            screen.Display();

            Thread.Sleep(500);
            screen.Clear();
            
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
                DisplayHands();
                DisplayMoney();
                int resultDie;
                // tour joueur humain
                while (true)
                {
                    screen.Add(new Element(new string[] {"Tapez Enter pour Lancer le dé", }
                        , new Coordinates(screen.width/2, screen.height/2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black), 2);
                    screen.Display();
                    screen.DeleteLayer(2);
                    
                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Enter || key == ConsoleKey.Spacebar)
                    {
                        resultDie = die.Lancer();
                        break;
                    }
                }
                // Animation du Dé
                for (int i = 0; i < 5; i++)
                {
                    screen.Add(new Element(Die.ToStrings(rnd.Next(1, 7))
                        , new Coordinates(screen.width/2, screen.height/2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black), 2);
                    Thread.Sleep(300);
                    screen.Display();
                }
                screen.Add(new Element(Die.ToStrings(resultDie)
                    , new Coordinates(screen.width/2, screen.height/2),
                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black), 2);
                screen.Display();
                screen.DeleteLayer(2);
                Thread.Sleep(3000);

                
                CardsActivation(playerH, playerIA, resultDie);
                DisplayMoney();
                //IA bleue et rouge
                //H bleue et vert

                // choix action joueur
                bool action = false;
                while (!action)
                {
                    screen.Add(new Element(new string[] {"Voulez-vous acheter ?", }
                        , new Coordinates(screen.width/2, screen.height/2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black), 2);

                    int choix = screen.Choice(new string[2]{"NON", "OUI"}, screen.height/2+2);
                    if (choix == 1)
                    {
                        // TODO choix entre les différentes cartes
                        List<Element> cardsElements = new List<Element>();

                        int index = 0;
                        for (int i = 0; i <= 7; i++)
                        {
                            Coordinates coordinates = new Coordinates(screen.width/2 - 21*2 + i%4*21, screen.height/2 - 11 + (i >= 4 ? 11 : 0));
                            Element[] card = CardChoice(index).ToElementFull(coordinates);
                            screen.Add(card[0], 2);
                            screen.Add(card[1], 2);
                            cardsElements.Add(card[1]);

                            index++;
                        }
                        screen.Display();
                        choix = screen.Select(cardsElements.ToArray(), 2);

                        

                        // selection carte choisi
                        CardsInfo c = CardChoice(choix); 

                        // on vérifie que la carte est encore disponible
                        if (pile.GetNumberCard(choix) == 0)
                        {
                            // TODO "la carte n'est plus disponible"
                            screen.DeleteLayer(2);
                            screen.Add(new Element(new string[] {"La carte n'est plus disponible"}
                                                    , new Coordinates(screen.width/2, screen.height/2+1),
                                                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black), 2);
                        }
                        // on vérifie que le joueur a assez d'argent
                        else if (c.Cost <= playerH.UserMoney)
                        {
                            playerH.BuyCard(c, pile);
                            action = true;
                        }
                        else
                        {
                            // TODO "vous n'avez pas assez d'argent"
                            screen.DeleteLayer(2);
                            screen.Add(new Element(new string[] {"Vous n'avez pas assez d'argent"}
                                                    , new Coordinates(screen.width/2, screen.height/2+1),
                                                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black), 2);
                        }
                        screen.Display();
                    }
                    else { action = true; }
                }

                // verification condition de fin

                if(playerH.UserMoney >= gainFinish || playerIA.UserMoney >= gainFinish) { break; }


                // tour joueur IA
                resultDie = die.Lancer();
                CardsActivation(playerIA, playerH, resultDie);
                //H bleue et rouge
                //IA bleue et vert

                // cette méthode pose probleme, l'IA va de moins en moins acheter de batiments car il y aura moins de cartes disponbles au fur et a mesure de la partie

                // action au hasard
                if (rnd.Next(0, 2) == 0 && playerIA.UserMoney > 0)
                {
                    // choix d'une carte a acheter au hasard
                    int choix = rnd.Next(0, 8);

                    // selection carte choisi
                    CardsInfo c = CardChoice(choix); 

                    // on vérifie que la carte est encore disponible et qu'elle est encore achetable

                    if (c.Cost < playerIA.UserMoney && pile.GetNumberCard(choix) == 0)
                    {
                        playerIA.BuyCard(c, pile);

                    }
                }

            }

            // TODO nettoyer screen
            screen.Clear();
            if (playerH.UserMoney > playerIA.UserMoney)
            {
                // TODO "vous gagnez car vous avez le plus d'argent"
                screen.Add(new Element(new string[] {"Vous gagnez car vous avez le plus d'argent"}
                                                    , new Coordinates(screen.width/2, screen.height/2+1),
                                                    Animation.LetterByLetter, Placement.mid, ConsoleColor.White, ConsoleColor.Black), 2);
            }
            else if (playerIA.UserMoney > playerH.UserMoney)
            {
                // TODO "vous perdez car l'adversaire est le plus riche"
                screen.Add(new Element(new string[] {"Vous perdez car l'adversaire est le plus riche"}
                                    , new Coordinates(screen.width/2, screen.height/2+1),
                                    Animation.LetterByLetter, Placement.mid, ConsoleColor.White, ConsoleColor.Black), 2);
            }
            else // egalité des sommes d'argent
            {
                // TODO "vous êtes à égalité car vous avez autant d'argent que votre adversaire
                screen.Add(new Element(new string[] {"Vous êtes à égalité car vous avez autant d'argent que votre adversaire"}
                            , new Coordinates(screen.width/2, screen.height/2+1),
                            Animation.LetterByLetter, Placement.mid, ConsoleColor.White, ConsoleColor.Black), 2);
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