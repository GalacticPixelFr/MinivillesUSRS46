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
        private Screen screen;
        private Layer hands;
        private Layer middle;
        private Layer money;
        private Layer background;

        private Random rnd;

        public Game(int gain)
        {
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
            hands = new Layer(2);
            middle = new Layer(3);
            money = new Layer(2);
            background = new Layer(1);
        }

        public void DisplayMoney()
        {
            money.Add(new Element(new string[] {playerIA.UserMoney+" pièces", }
                , new Coordinates(1, 1),
                Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black));

            money.Add(new Element(new string[] {playerH.UserMoney+" pièces", }
                , new Coordinates(1, screen.height-2),
                Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black));
            
            screen.DisplayLayer(money);
        }
      
        public void DisplayHands()
        {
            screen.HideLayer(hands);
            List<int> cards = new List<int>();
            for (int i = 0; i < playerIA.UserHand.Count; i++)
            {
                if (cards.Contains(playerIA.UserHand[i].Id)) continue;
                cards.Add(playerIA.UserHand[i].Id);
                Coordinates coordinates = new Coordinates(screen.width/2 - playerIA.UserHand.Count*(18+2)/2 + i*(18+2)+9, +3);
                Element[] elements = playerIA.UserHand[i].ToElementSemi(false, playerIA.GetNumberCard(playerIA.UserHand[i].Id), coordinates);//TODO le nombre de carte
                hands.Add(elements[0]);
                hands.Add(elements[1]);
            }
            
            cards = new List<int>();
            for (int i = 0; i < playerH.UserHand.Count; i++)
            {
                if (cards.Contains(playerH.UserHand[i].Id)) continue;
                cards.Add(playerH.UserHand[i].Id);
                Coordinates coordinates = new Coordinates(screen.width/2 - playerH.UserHand.Count*(18+2)/2 + i*(18+2)+9, screen.height-3);
                Element[] elements = playerH.UserHand[i].ToElementSemi(true,playerH.GetNumberCard(playerH.UserHand[i].Id), coordinates);//TODO le nombre de carte
                hands.Add(elements[0]);
                hands.Add(elements[1]);
            }
            screen.DisplayLayer(hands);
        }

        public void Run()
        {
            Layer creditsLayer = new Layer(1);
            creditsLayer.Add(new Element(TextManagement.GetData("Accueil"), new Coordinates(screen.width / 2, screen.height / 2), Animation.None, Placement.mid,
                ConsoleColor.White,
                ConsoleColor.Black));
            screen.DisplayLayer(creditsLayer);

            Thread.Sleep(500);
            screen.HideLayer(creditsLayer);

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
                    Element pressEnter = new Element(new string[] {"Tapez Enter pour Lancer le dé",}
                        , new Coordinates(screen.width / 2, screen.height / 2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
                    screen.DisplayElement(pressEnter);

                    ConsoleKey key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Enter || key == ConsoleKey.Spacebar)
                    {
                        resultDie = die.Lancer();
                        screen.HideElement(pressEnter);
                        break;
                    }
                }
                // Animation du Dé
                Layer dieLayer = new Layer(1);
                for (int i = 0; i < 5; i++)
                {
                    dieLayer.Add(new Element(Die.ToStrings(rnd.Next(1, 7))
                        , new Coordinates(screen.width/2, screen.height/2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                    Thread.Sleep(300);
                    screen.DisplayLayer(dieLayer);
                }
                dieLayer.Add(new Element(Die.ToStrings(resultDie)
                    , new Coordinates(screen.width/2, screen.height/2),
                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                screen.DisplayLayer(dieLayer);
                Thread.Sleep(3000);
                screen.HideLayer(dieLayer);

                
                CardsActivation(playerH, playerIA, resultDie);
                DisplayMoney();
                //IA bleue et rouge
                //H bleue et vert

                // choix action joueur
                bool action = false;
                while (!action)
                {
                    Element title = new Element(new string[] {"Voulez-vous acheter ?",}
                        , new Coordinates(screen.width / 2, screen.height / 2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
                    screen.DisplayElement(title);

                    Layer choice = new Layer(1);
                    int choix = screen.Choice(new string[2]{"NON", "OUI"}, screen.height/2+2, choice);
                    screen.HideLayer(choice);
                    choice.Clear();
                    screen.HideElement(title);
                    if (choix == 1)
                    {
                        List<Element> cardsElements = new List<Element>();

                        int index = 0;
                        for (int i = 0; i <= 7; i++)
                        {
                            Coordinates coordinates = new Coordinates(screen.width/2 - 4*(18+2)/2+i%4*(18+2)+9, 
                                                                        screen.height/2 - 2*(9+2)/2 + (i >= 4 ? 11 : 0)+4);
                            Element[] card = CardChoice(index).ToElementFull(coordinates);
                            middle.Add(card[0]);
                            middle.Add(card[1]);
                            cardsElements.Add(card[1]);

                            index++;
                        }
                        screen.DisplayLayer(middle);
                        choix = screen.Select(cardsElements.ToArray());
                        screen.HideLayer(middle);
                        middle.Clear();

                        

                        // selection carte choisi
                        CardsInfo c = CardChoice(choix); 

                        // on vérifie que la carte est encore disponible
                        if (pile.GetNumberCard(choix) == 0)
                        {
                            background.Add(new Element(new string[] {"La carte n'est plus disponible"}
                                                    , new Coordinates(screen.width/2, screen.height/2+1),
                                                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black, true));
                        }
                        // on vérifie que le joueur a assez d'argent
                        else if (c.Cost <= playerH.UserMoney)
                        {
                            playerH.BuyCard(c, pile);
                            action = true;
                        }
                        else
                        {
                            background.Add(new Element(new string[] {"Vous n'avez pas assez d'argent"}
                                                    , new Coordinates(screen.width/2, screen.height/2+1),
                                                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black, true));
                        }
                        screen.DisplayLayer(background);
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
            
            screen.Clear();
            if (playerH.UserMoney > playerIA.UserMoney)
            {
                background.Add(new Element(new string[] {"Vous gagnez car vous avez le plus d'argent"}
                                                    , new Coordinates(screen.width/2, screen.height/2+1),
                                                    Animation.LetterByLetter, Placement.mid, ConsoleColor.White, ConsoleColor.Black, true));
            }
            else if (playerIA.UserMoney > playerH.UserMoney)
            {
                background.Add(new Element(new string[] {"Vous perdez car l'adversaire est le plus riche"}
                                    , new Coordinates(screen.width/2, screen.height/2+1),
                                    Animation.LetterByLetter, Placement.mid, ConsoleColor.White, ConsoleColor.Black, true));
            }
            else // egalité des sommes d'argent
            {
                background.Add(new Element(new string[] {"Vous êtes à égalité car vous avez autant d'argent que votre adversaire"}
                            , new Coordinates(screen.width/2, screen.height/2+1),
                            Animation.LetterByLetter, Placement.mid, ConsoleColor.White, ConsoleColor.Black, true));
            }
            screen.DisplayLayer(background);
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