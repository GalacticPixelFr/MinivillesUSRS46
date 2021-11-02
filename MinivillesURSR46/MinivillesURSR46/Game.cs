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
        private Chat chat;

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
                Animation.None, Placement.topLeft, playerH.UserMoney<0 ? ConsoleColor.Red : ConsoleColor.White, ConsoleColor.Black));

            money.Add(new Element(new string[] {playerH.UserMoney+" pièces", }
                , new Coordinates(1, screen.height-2),
                Animation.None, Placement.topLeft, playerH.UserMoney<0 ? ConsoleColor.Red : ConsoleColor.White, ConsoleColor.Black));
            
            screen.DisplayLayer(money);
            
            
        }
      
        public void DisplayHands()
        {
            screen.HideLayer(hands);
            hands.Clear();
            
            List<string> cards = new List<string>();
            int index = -1;
            for (int i = 0; i < playerIA.UserHand.Count; i++)
            {
                index += 1;
                if (cards.Contains(playerIA.UserHand[i].Name)){
                    index -= 1;
                    continue;
                } 
                    
                cards.Add(playerIA.UserHand[i].Name);
                Coordinates coordinates = new Coordinates((screen.width-34)/2 - playerIA.GetNumberCardType()*(18+2)/2 + index*(18+2)+9, +3);
                Element[] elements = playerIA.UserHand[i].ToElementSemi(false, playerIA.GetNumberCard(playerIA.UserHand[i].Id), coordinates);
                Element amount = new Element(new string[1] {"x" + playerIA.GetNumberCard(playerIA.UserHand[i].Id)},
                    new Coordinates((screen.width - 34) / 2 - playerIA.GetNumberCardType() * (18 + 2) / 2 + index * (18 + 2) + 9, 5),
                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
                
                hands.Add(amount);
                hands.Add(elements[0]);
                hands.Add(elements[1]);
            }
            
            cards.Clear();
            index = -1;
            for (int i = 0; i < playerH.UserHand.Count; i++)
            {
                index += 1;
                if (cards.Contains(playerH.UserHand[i].Name)){
                    index -= 1;
                    continue;
                }
                
                cards.Add(playerH.UserHand[i].Name);
                Coordinates coordinates = new Coordinates((screen.width-34)/2 - playerH.GetNumberCardType()*(18+2)/2 + index*(18+2)+9, screen.height-3);
                Element[] elements = playerH.UserHand[i].ToElementSemi(true, playerH.GetNumberCard(playerH.UserHand[i].Id), coordinates);
                Element amount = new Element(new string[1] {"x" + playerH.GetNumberCard(playerH.UserHand[i].Id)},
                    new Coordinates((screen.width - 34) / 2 - playerH.GetNumberCardType() * (18 + 2) / 2 + index * (18 + 2) + 9, screen.height-6),
                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
                
                hands.Add(amount);
                hands.Add(elements[0]);
                hands.Add(elements[1]);
            }
            screen.DisplayLayer(hands);
        }

        public void StartGame()
        {
            while (!Menu.Display(screen, this))
            {
                
            }
        }
        
        public void Run(GameOption gameOption)
        {
            gainFinish = 10 * (1 + gameOption.duree);
            
            bool Urss = false;
            if (gameOption.modeDeJeu == 0)
            {
                pile.nameChange();
                playerH.nameChange();
                playerIA.nameChange();
                Urss = true;
            }
            
            
            chat = new Chat(screen, new Coordinates(screen.width-34, screen.height), 34, screen.height);
            /*
            Layer creditsLayer = new Layer(1);
            creditsLayer.Add(new Element(TextManagement.GetData("Accueil"), new Coordinates((screen.width-34) / 2, screen.height / 2),
                Animation.None, Placement.mid,
                ConsoleColor.White,
                ConsoleColor.Black));
            screen.DisplayLayer(creditsLayer);

            Thread.Sleep(500);
            screen.HideLayer(creditsLayer);

            Element titleOptions = new Element(new string[] {"Quelle partie voulez-vous faire ?",}
                        , new Coordinates((screen.width-34)/2, screen.height / 2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
            screen.DisplayElement(titleOptions);

            Layer choiceP = new Layer(1);
            Element facile = new Element(new string[] {"Rapide",},
                new Coordinates((screen.width-34)/4 * 1, screen.height/2+2),
                Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
            Element normal = new Element(new string[] {"Normal",},
                new Coordinates((screen.width-34)/4 * 2, screen.height/2+2),
                Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
            Element difficile = new Element(new string[] {"Difficile",},
                new Coordinates((screen.width-34)/4 * 3, screen.height/2+2),
                Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
            Element expert = new Element(new Coordinates((screen.width-34)/5 * 4, screen.height/2+2), "Expert");
            // TODO add expert
            choiceP.Add(facile);
            choiceP.Add(normal);
            choiceP.Add(difficile);
            screen.DisplayLayer(choiceP);
            int choixP = screen.Select(new Element[3] {facile, normal, difficile});
            screen.HideLayer(choiceP);
            choiceP.Clear();
            screen.HideElement(titleOptions);

            if (choixP == 3)
            {
                gainFinish = 30;
            }
            else
            {
                gainFinish = 10 * (choixP + 1);
            }
            
            Element deckOptions = new Element(new string[] {"Quel deck voulez vous jouer avec ?",}
                , new Coordinates((screen.width-34) / 2, screen.height / 2),
                Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
            background.Add(deckOptions);
            int choixDeck = screen.Choice(new string[] {"USA", "URSS"}, screen.height / 2 + 2, background);
            if (choixDeck == 1)
            {
                pile.nameChange();
                playerH.nameChange();
                playerIA.nameChange();
                Urss = true;
            }
            screen.HideLayer(background);
            background.Clear();*/

            /*
            1. Le joueur A lance le dé.
            2. Le joueur B regarde s’il a des cartes bleues ou rouges qui s’activent et il en applique les effets
            3. Le joueur A regarde s’il a des cartes bleues ou vertes qui s’activent et il en applique les effets
            4. Le joueur A peut acheter une nouvelle carte et l’ajouter à sa ville. Il est possible d’avoir plusieurs fois la même carte, les effets s’additionnent.
            Une fois le tour du joueur A terminé, c’est au tour du joueur B de réaliser les mêmes actions.
            */

            // condition fin
            while (true)
            {
                DisplayHands();
                DisplayMoney();
                int resultDie;
                // tour joueur humain
                while (true)
                {
                    Element pressEnter = new Element(TextManagement.GetData("EnterDé")
                        , new Coordinates((screen.width-34) / 2, screen.height / 2),
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
                        , new Coordinates((screen.width-34)/2, screen.height/2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                    Thread.Sleep(300);
                    screen.DisplayLayer(dieLayer);
                }
                dieLayer.Add(new Element(Die.ToStrings(resultDie)
                    , new Coordinates((screen.width-34)/2, screen.height/2),
                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                screen.DisplayLayer(dieLayer);
                Thread.Sleep(3000);
                screen.HideLayer(dieLayer);
                chat.AddText(TextManagement.GetDataString("NombreDé", resultDie.ToString()));

                int incomePlayer = playerH.UserMoney;
                int incomeIa = playerIA.UserMoney;
                CardsActivation(playerH, playerIA, resultDie);
                incomePlayer = playerH.UserMoney - incomePlayer;
                incomeIa = playerIA.UserMoney - incomeIa;
                DisplayMoney();
                chat.AddText(TextManagement.GetDataString("Revenu", incomePlayer.ToString()));
                if (incomeIa < 0)
                {
                    chat.AddText(TextManagement.GetDataString("NegativeRevenuIa", incomeIa.ToString()));
                }
                else
                {
                    chat.AddText(TextManagement.GetDataString("RevenuIa", incomeIa.ToString()));
                }
                //IA bleue et rouge
                //H bleue et vert

                // choix action joueur
                bool action = false;
                while (!action)
                {
                    Element title = new Element(TextManagement.GetData("Achat")
                        , new Coordinates((screen.width-34)/2, screen.height / 2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
                    screen.DisplayElement(title);

                    Layer choice = new Layer(1); 
                    Element oui = new Element(new Coordinates((screen.width-34)/3 *1, screen.height/2+2), "OUI");
                    Element non = new Element(new Coordinates((screen.width-34)/3 *2, screen.height/2+2), "NON");
                    choice.Add(oui);
                    choice.Add(non);
                    screen.DisplayLayer(choice);
                    int choix = screen.Select(new Element[2] {non, oui});
                    screen.HideLayer(choice);
                    choice.Clear();
                    screen.HideElement(title);
                    if (choix == 1)
                    {
                        List<Element> cardsElements = new List<Element>();
                        
                        for (int i = 0; i <= 7; i++) //En faire une fonction
                        {
                            Coordinates coordinates = new Coordinates((screen.width-34)/2 - 4*(18+2)/2+i%4*(18+2)+9, 
                                screen.height/2 - 2*(9+2)/2 + (i >= 4 ? 11 : 0)+4);
                            Element amount = new Element(new string[1] {"x " + pile.GetNumberCard(i)},
                                new Coordinates(
                                    (screen.width - 34) / 2 - 4 * (18 + 2) / 2 + i % 4 * (18 + 2) + 9,
                                    screen.height / 2 - 2 * (9 + 2) / 2 + (i >= 4 ? 16 : -5) + 4), Animation.None, Placement.mid,
                                ConsoleColor.White, ConsoleColor.Black);
                            
                            Element[] card = Urss ? CardChoice(i).ToElementFull(coordinates, true) : CardChoice(i).ToElementFull(coordinates, false);
                            
                            middle.Add(amount);
                            middle.Add(card[0]);
                            middle.Add(card[1]);
                            cardsElements.Add(card[1]);
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
                            background.Add(new Element(TextManagement.GetData("Indisponible")
                                                    , new Coordinates((screen.width-34)/2, screen.height/2+1),
                                                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                        }
                        // on vérifie que le joueur a assez d'argent
                        else if (c.Cost <= playerH.UserMoney)
                        {
                            playerH.BuyCard(c, pile);
                            chat.AddText(TextManagement.GetDataString("CarteAchat", Urss ? c.NameURSS : c.Name));
                            DisplayHands();
                            DisplayMoney();
                            action = true;
                        }
                        else
                        {
                            background.Add(new Element(TextManagement.GetData("ZeroArgent")
                                                    , new Coordinates((screen.width-34)/2, screen.height/2+1),
                                                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                        }
                        screen.DisplayLayer(background);
                        Thread.Sleep(1000);
                        screen.HideLayer(background);

                    }
                    else { action = true; }

                    if (choix == 0)
                    {
                        chat.AddText(TextManagement.GetDataString("NoAchat"));
                    }
                    
                }

                // verification condition de fin
                if (playerH.UserMoney >= gainFinish || playerIA.UserMoney >= gainFinish)
                {
                    if (gameOption.difficultee != 2) { break; }
                    else if ((playerH.UserMoney >= gainFinish && playerH.GetNumberCardType() == 8) || (playerIA.UserMoney >= gainFinish && playerIA.GetNumberCardType() == 8)) { break; }
                }
                DisplayHands();
                DisplayMoney();

                // tour joueur IA
                resultDie = die.Lancer();
                
                dieLayer = new Layer(1);
                for (int i = 0; i < 5; i++)
                {
                    dieLayer.Add(new Element(Die.ToStrings(rnd.Next(1, 7))
                        , new Coordinates((screen.width-34)/2, screen.height/2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                    Thread.Sleep(300);
                    screen.DisplayLayer(dieLayer);
                }
                dieLayer.Add(new Element(Die.ToStrings(resultDie)
                    , new Coordinates((screen.width-34)/2, screen.height/2),
                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                screen.DisplayLayer(dieLayer);
                Thread.Sleep(3000);
                screen.HideLayer(dieLayer);
                
                chat.AddText(TextManagement.GetDataString("IaDé", resultDie.ToString()));
                
                incomePlayer = playerH.UserMoney;
                incomeIa = playerIA.UserMoney;
                CardsActivation(playerIA, playerH, resultDie);
                incomePlayer = playerH.UserMoney - incomePlayer;
                incomeIa = playerIA.UserMoney - incomeIa;
                chat.AddText(TextManagement.GetDataString("RevenuIa", incomeIa.ToString()));
                if (incomePlayer < 0)
                {
                    chat.AddText(TextManagement.GetDataString("NegativeRevenu", incomePlayer.ToString()));
                }
                else
                {
                    chat.AddText(TextManagement.GetDataString("Revenu", incomePlayer.ToString()));
                }
                
                //H bleue et rouge
                //IA bleue et vert

                // difficulté de l'IA et action
                actionIA(gameOption.niveauIA, Urss);


                // verification condition de fin
                if (playerH.UserMoney >= gainFinish || playerIA.UserMoney >= gainFinish)
                {
                    if (gameOption.difficultee != 2) { break; }
                    else if ((playerH.UserMoney >= gainFinish && playerH.GetNumberCardType() == 8) || (playerIA.UserMoney >= gainFinish && playerIA.GetNumberCardType() == 8)) { break; }
                }

            }
            
            screen.Clear();
            if (playerH.UserMoney > playerIA.UserMoney)
            {
                background.Add(new Element(TextManagement.GetData("Gagné")
                                                    , new Coordinates((screen.width-34)/2, screen.height/2+1),
                                                    Animation.Typing, Placement.mid, ConsoleColor.White, ConsoleColor.Black, true));
            }
            else if (playerIA.UserMoney > playerH.UserMoney)
            {
                background.Add(new Element(TextManagement.GetData("Perdu")
                                    , new Coordinates((screen.width-34)/2, screen.height/2+1),
                                    Animation.Typing, Placement.mid, ConsoleColor.White, ConsoleColor.Black, true));
            }
            else // egalité des sommes d'argent
            {
                background.Add(new Element(TextManagement.GetData("Egalité")
                            , new Coordinates((screen.width-34)/2, screen.height/2+1),
                            Animation.Typing, Placement.mid, ConsoleColor.White, ConsoleColor.Black, true));
            }
            screen.DisplayLayer(background);
        }

        private CardsInfo CardChoice(int i)
        {
            CardsInfo c;

            if (i == 0) { c = new Cards().CreateChampDeBle(); }
            else if (i == 1) { c = new Cards().CreateFerme(); }
            else if (i == 2) { c = new Cards().CreateBoulangerie(); }
            else if (i == 3) { c = new Cards().CreateCafe(); }
            else if (i == 4) { c = new Cards().CreateSuperette(); }
            else if (i == 5) { c = new Cards().CreateForet(); }
            else if (i == 6) { c = new Cards().CreateRestaurant(); }
            else { c = new Cards().CreateStade(); }

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

        public void actionIA(int difficulty, bool Urss)
        {
            if (difficulty == 0) // IA choix au hasard
            {
                // action au hasard
                if (rnd.Next(0, 2) == 0 && playerIA.UserMoney > 0)
                {
                    // choix d'une carte a acheter au hasard
                    int choix = rnd.Next(0, 8);

                    // selection carte choisi
                    CardsInfo c = CardChoice(choix); 

                    // on vérifie que la carte est encore disponible et qu'elle est encore achetable
                    if (c.Cost < playerIA.UserMoney - 1 && pile.GetNumberCard(choix) > 0)
                    {
                        playerIA.BuyCard(c, pile);
                        chat.AddText(TextManagement.GetDataString("IaCarteAchat", Urss ? c.NameURSS : c.Name));
                    }
                    else { chat.AddText(TextManagement.GetDataString("NoIaAchat")); }
                }
                else { chat.AddText(TextManagement.GetDataString("NoIaAchat")); }
            }
            else if (difficulty == 1) // agit selon état de la partie (argent du joueur par rapport à l'argent maximal)
            {
                bool tranquille = true; // détermine l'état de l'IA (entre tranquille et panique)
                /*
                condition pour être en panique :
                - 1/3 si argent joueur dans 2e quart de gain finish
                - 2/3 si argent joueur dans 3e quart de gain finish
                - si argent joueur dans 4e quart de gain finish
                */
                if ((playerH.UserMoney > gainFinish/4 && playerH.UserMoney <= gainFinish/2 && rnd.Next(0, 4) == 0) || 
                    (playerH.UserMoney > gainFinish/2 && playerH.UserMoney < (gainFinish*3)/4 && rnd.Next(0, 4) > 0) || 
                    (playerH.UserMoney >= (gainFinish*3)/4)){ tranquille = false; }

                CardsInfo c = null; // futur carte acheté

                // tranquille signifie qu'elle va osciller entre acheter vert et bleu afin d'augmenter ses gains (priorité au bleu car plus rentable)
                if (tranquille){
                    /*
                    1/5 de ne rien faire
                    1/5 de faire vert
                    3/5 de faire bleu
                    */
                    int choix = rnd.Next(0,6);
                    if (choix == 0){ //vert
                        if (playerIA.UserMoney >= 3) { // si choix entre ferme et foret possible (argent suffisant)
                            if (rnd.Next(0, 2) == 0 && pile.GetNumberCard(2) > 0) { c = CardChoice(2); } // 1/2 achat boulangerie
                            else if (pile.GetNumberCard(4) > 0) { c = CardChoice(4); } // 1/2 achat superette
                            }
                        else if (playerIA.UserMoney >= 2 && pile.GetNumberCard(2) > 0) { c = CardChoice(2); }
                        }
                    else if (choix < 5){ //bleu
                        if (playerIA.UserMoney >= 1 && pile.GetNumberCard(0) > 0) { c = CardChoice(0); } // si achat champs possible
                        else if (playerIA.UserMoney >= 2 && (pile.GetNumberCard(1) > 0 || pile.GetNumberCard(5) > 0)) {
                            if (pile.GetNumberCard(1) == 0){ c = CardChoice(5); } // si plus de ferme, achat foret
                            else if (pile.GetNumberCard(5) == 0) { c = CardChoice(1); } // si plus de foret, achat ferme
                            else if (rnd.Next(0, 2) == 0) { c = CardChoice(1); } // sinon achat aleatoire, 1/2 pour chaque
                            else { c = CardChoice(5); }
                            }
                        else if (playerIA.UserMoney >= 4 && pile.GetNumberCard(7) > 0) { c = CardChoice(7); }
                        }
                    }
                else
                {
                    // quand on panique, on essaye d'acheter des cartes rouges pour diminuer l'argent de l'adversaire
                    if (playerIA.UserMoney >= 4 && pile.GetNumberCard(6) > 0) { c = CardChoice(6); } // achat de restaurant en priorité (valeur plus grande)
                    else if (playerIA.UserMoney >= 2 && pile.GetNumberCard(3) > 0) { c = CardChoice(3); }
                }

                // si on a décidé d'acheter
                if (c == null)
                {
                    playerIA.BuyCard(c, pile);
                    chat.AddText(TextManagement.GetDataString("IaCarteAchat", Urss ? c.NameURSS : c.Name));
                }	
                else
                {
                    chat.AddText(TextManagement.GetDataString("NoIaAchat"));
                }
            }
        }
    }
}