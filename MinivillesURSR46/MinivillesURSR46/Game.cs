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
        public int gainFinish;

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
            //Ajout des cartes dans la pile selon le nombre de carte possible.
            Cards card = new Cards();
            for (int j = 0; j < card.EachCards.Count - 2; j++)
            {
                for (int h = 0; h < 4; h++)
                {
                    pile.AddCard(card.EachCards[j]);
                }
            }
            pile.AddCard(card.EachCards[10]);
            pile.AddCard(card.EachCards[11]);

            gainFinish = gain;

            die = new Die();
            rnd = new Random();
            screen = new Screen(200, 50);
            hands = new Layer(2);
            middle = new Layer(3);
            money = new Layer(2);
            background = new Layer(1);
        }

        /// <summary>
        /// Permet d'afficher l'argent des joueurs
        /// </summary>
        public void DisplayMoney()
        {
            money.Add(new Element(new string[] {playerIA.UserMoney+" pièces", }
                , new Coordinates(1, 1),
                Animation.None, Placement.topLeft, playerIA.UserMoney<0 ? ConsoleColor.Red : ConsoleColor.White, ConsoleColor.Black));

            money.Add(new Element(new string[] {playerH.UserMoney+" pièces", }
                , new Coordinates(1, screen.height-2),
                Animation.None, Placement.topLeft, playerH.UserMoney<0 ? ConsoleColor.Red : ConsoleColor.White, ConsoleColor.Black));
            
            screen.DisplayLayer(money);
        }
      
        /// <summary>
        /// Permet d'afficher les cartes possédé par les joueur
        /// </summary>
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
                Coordinates coordinates = new Coordinates((screen.width-34)/2 - playerIA.GetNumberCardType()*(11+2)/2 + index*(11+2)+9, +3);
                Element[] elements = playerIA.UserHand[i].ToElementSemi(false, coordinates);
                Element amount = new Element(new string[1] {"x" + playerIA.GetNumberCard(playerIA.UserHand[i].Id)},
                    new Coordinates((screen.width - 34) / 2 - playerIA.GetNumberCardType() * (11 + 2) / 2 + index * (11 + 2) + 9, 6),
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
                Coordinates coordinates = new Coordinates((screen.width-34)/2 - playerH.GetNumberCardType()*(11+2)/2 + index*(11+2)+9, screen.height-3);
                Element[] elements = playerH.UserHand[i].ToElementSemi(true, coordinates);
                Element amount = new Element(new string[1] {"x" + playerH.GetNumberCard(playerH.UserHand[i].Id)},
                    new Coordinates((screen.width - 34) / 2 - playerH.GetNumberCardType() * (11 + 2) / 2 + index * (11 + 2) + 9, screen.height-7),
                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
                
                hands.Add(amount);
                hands.Add(elements[0]);
                hands.Add(elements[1]);
            }
            screen.DisplayLayer(hands);
        }

        /// <summary>
        /// Permet de lancer le jeu
        /// </summary>
        public void StartGame()
        {
            while (!Menu.Display(screen, this))
            {
                screen = new Screen(screen.width, screen.height); //Pour reset l'écran
            }
        }
        
        /// <summary>
        /// Permet de lancer une partie
        /// </summary>
        /// <param name="gameOption">les différentes options de jeu</param>
        public void Run(GameOption gameOption)
        {
            // On instantie les joueurs
            playerH = new Player(new List<CardsInfo>(), pile);
            playerIA = new Player(new List<CardsInfo>(), pile);
            pile = new Piles();
            int nbTurn = 0;

            // Variables de compte pour les stats de fin
            int buyCardIA = 0;
            int buyCardPlayer = 0;
            int gainMoneyIA = 0;
            int gainMoneyPlayer = 0;
            int lossMoneyIA = 0;
            int lossMoneyPlayer = 0;
            
            gainFinish = 10 * (1 + gameOption.duree);
            
            //Changement des noms des cartes si choix URSS
            bool Urss = false;
            if (gameOption.modeDeJeu == 0)
            {
                pile.nameChange();
                playerH.nameChange();
                playerIA.nameChange();
                Urss = true;
            }
            
            chat = new Chat(screen, new Coordinates(screen.width-34, screen.height), 34, screen.height);

            // condition fin
            while (true)
            {
                nbTurn++; //On incrémente le nombre de tours
                DisplayHands();
                DisplayMoney();
                int resultDie;
                // tour joueur humain
                chat.AddText(TextManagement.GetDataString("TourJ", nbTurn.ToString()));
                while (true)
                {
                    // On créer un élément
                    Element pressEnter = new Element(TextManagement.GetData("EnterDé")
                        , new Coordinates((screen.width-34) / 2, screen.height / 2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
                    screen.DisplayElement(pressEnter); // Puis on l'affiche
                    
                    ConsoleKey key = Console.ReadKey().Key; // On attend que le joueur appui sur une touche
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
                    // On creer un élément de dé aléatoire
                    dieLayer.Add(new Element(Die.ToStrings(rnd.Next(1, 7))
                        , new Coordinates((screen.width-34)/2, screen.height/2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                    Thread.Sleep(300);
                    screen.DisplayLayer(dieLayer);
                }
                // Puis on creer un élément dé avec la valeur qu'on a obtenue
                dieLayer.Add(new Element(Die.ToStrings(resultDie)
                    , new Coordinates((screen.width-34)/2, screen.height/2),
                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                screen.DisplayLayer(dieLayer);
                Thread.Sleep(3000);
                screen.HideLayer(dieLayer);
                chat.AddText(TextManagement.GetDataString("NombreDé", resultDie.ToString())); // On affiche dans le chat la valeur qu'on a obtenue

                // On instantie des variables pour les connaitre les révenues des joueurs 
                int incomePlayer = playerH.UserMoney;
                int incomeIA = playerIA.UserMoney;
                
                CardsActivation(playerH, playerIA, resultDie); // On actives les bonnes cartes
                
                // Puis on calcule la différence
                incomePlayer = playerH.UserMoney - incomePlayer;
                incomeIA = playerIA.UserMoney - incomeIA;
                DisplayMoney(); // On actualise leur argent dans l'écran
                chat.AddText(TextManagement.GetDataString("Revenu", incomePlayer.ToString())); // Puis on le dit dans le chat pour plus de clarté
                gainMoneyPlayer += incomePlayer; // On ajoute la différence aux gains totaux du joueur
                if (incomeIA < 0)
                {
                    lossMoneyIA += Math.Abs(incomeIA); // On ajoute la valeur absolue des pertes aux pertes total
                    chat.AddText(TextManagement.GetDataString("NegativeRevenuIa", incomeIA.ToString()));
                }
                else
                {
                    gainMoneyIA += incomeIA; // On ajoute la valeur absolue des pertes aux pertes total
                    chat.AddText(TextManagement.GetDataString("RevenuIa", incomeIA.ToString()));
                }

                // Phase d'achat du joueur
                bool action = false; // Pour savoir si le joueur a fait une action
                while (!action) //S'il n'a rien fait on boucle
                {
                    // On creer un élément pour demander si le joueur veut acheter
                    Element title = new Element(TextManagement.GetData("Achat")
                        , new Coordinates((screen.width-34)/2, screen.height / 2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
                    screen.DisplayElement(title); // On affiche l'élément
                    
                    // Permet de faire faire un choix au joueur entre oui et non
                    Layer choice = new Layer(1); // On créer un layer sur lequel afficher les éléments du choix
                    Element oui = new Element(new Coordinates((screen.width-34)/3 *1, screen.height/2+2), "OUI");
                    Element non = new Element(new Coordinates((screen.width-34)/3 *2, screen.height/2+2), "NON");
                    // On ajoute les éléments créé plus tôt dans le layer
                    choice.Add(oui);
                    choice.Add(non);
                    screen.DisplayLayer(choice); // On affiche le layer
                    int choix = screen.Select(new Element[2] {non, oui}); // On demande au joueur s'il veut acheter
                    screen.HideLayer(choice);
                    choice.Clear();
                    screen.HideElement(title);
                    
                    // On gère le choix du joueur
                    if (choix == 0) // Si le joueur ne veut pas acheter
                    {
                        chat.AddText(TextManagement.GetDataString("NoAchat")); // On ajoute au chat un petit message
                    }
                    if (choix == 1) // Si le joueur veut acheter
                    {
                        List<Element> cardsElements = DisplayCards(Urss, middle, -34); // On récupère l'affichage des différentes cartes
                      
                        screen.DisplayLayer(middle); // On les affiches
                        choix = screen.Select(cardsElements.ToArray()); // On fait faire un choix entre toutes les cartes
                        screen.HideLayer(middle);
                        middle.Clear();

                        // Selection de la carte choisi
                        CardsInfo c = CardChoice(choix);

                        // On vérifie que la carte est encore disponible
                        if (pile.GetNumberCard(choix) >= 0) // Si la carte n'est plus disponible
                        {
                            // On ajoute l'élément pour dire au joueur que la carte n'est plus disponible au background
                            background.Add(new Element(TextManagement.GetData("Indisponible")
                                                    , new Coordinates((screen.width-34)/2, screen.height/2+1),
                                                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                        }
                        else if (c.Cost <= playerH.UserMoney) // Si la carte est disponible on vérifi que le joueur a assez d'argent
                        {
                            buyCardPlayer += 1; // On incrément la stat du nombre de carte acheté
                            playerH.BuyCard(c, pile); // On fait acheter la carte
                            chat.AddText(TextManagement.GetDataString("CarteAchat", Urss ? c.NameURSS : c.Name)); // On dit dans le chat
                            DisplayHands(); // On actualise les cartes des joueurs
                            DisplayMoney(); // On actualise aussi leur argent
                            action = true; // On dit que le joueur a fait une action
                        }
                        else // Et si le joueur n'a pas assez d'argent
                        {
                            // On ajoute l'élément pour dire au joueur qu'il n'a pas assez d'argent au background
                            background.Add(new Element(TextManagement.GetData("ZeroArgent")
                                                    , new Coordinates((screen.width-34)/2, screen.height/2+1),
                                                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                        }
                        screen.DisplayLayer(background); // On affiche tous les éléments qu'on ajouté plus haut
                        Thread.Sleep(1000);
                        screen.HideLayer(background); // Puis on le cache
                    }
                    else { action = true; }
                }

                // verification condition de fin
                if (playerH.UserMoney >= gainFinish || playerIA.UserMoney >= gainFinish)
                {
                    if (gameOption.difficultee != 1) { break; }
                    else if ((playerH.UserMoney >= gainFinish && playerH.GetNumberCardType() == 8) || (playerIA.UserMoney >= gainFinish && playerIA.GetNumberCardType() == 8)) { break; }
                }
                DisplayHands(); // On actualise les cartes des joueurs
                DisplayMoney(); // On actualise aussi leur argent

                // tour joueur IA
                chat.AddText(TextManagement.GetDataString("TourIa", nbTurn.ToString()));
                resultDie = die.Lancer(); // On fait lancer le dé
                
                // Animation du Dé
                dieLayer = new Layer(1);
                for (int i = 0; i < 5; i++)
                {
                    // On creer un élément de dé aléatoire
                    dieLayer.Add(new Element(Die.ToStrings(rnd.Next(1, 7))
                        , new Coordinates((screen.width-34)/2, screen.height/2),
                        Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                    Thread.Sleep(300);
                    screen.DisplayLayer(dieLayer);
                }
                // Puis on creer un élément dé avec la valeur qu'on a obtenue
                dieLayer.Add(new Element(Die.ToStrings(resultDie)
                    , new Coordinates((screen.width-34)/2, screen.height/2),
                    Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
                screen.DisplayLayer(dieLayer);
                Thread.Sleep(3000);
                screen.HideLayer(dieLayer);;
                
                chat.AddText(TextManagement.GetDataString("IaDé", resultDie.ToString()));
                
                // Activation des cartes et ajout des textes dans le chat en fonction des revenus du joueur
                // On instantie des variables pour les connaitre les révenues des joueurs 
                incomePlayer = playerH.UserMoney;
                incomeIA = playerIA.UserMoney;
                CardsActivation(playerIA, playerH, resultDie); // On actives les bonnes cartes
                // Puis on calcule la différence
                incomePlayer = playerH.UserMoney - incomePlayer;
                incomeIA = playerIA.UserMoney - incomeIA;
                chat.AddText(TextManagement.GetDataString("RevenuIa", incomeIA.ToString()));
                gainMoneyIA += incomeIA;  // On ajoute la différence aux gains totaux de l'ia
                if (incomePlayer < 0)
                {
                    lossMoneyPlayer += Math.Abs(incomePlayer); // On ajoute la valeur absolue des pertes aux pertes total
                    chat.AddText(TextManagement.GetDataString("NegativeRevenu", incomePlayer.ToString()));
                }
                else
                {
                    gainMoneyPlayer += incomePlayer;
                    chat.AddText(TextManagement.GetDataString("Revenu", incomePlayer.ToString()));
                }
                // difficulté de l'IA et action
                actionIA(gameOption.niveauIA, Urss, ref buyCardIA);

                // verification condition de fin
                if (playerH.UserMoney >= gainFinish || playerIA.UserMoney >= gainFinish)
                {
                    if (gameOption.difficultee != 1) { break; }
                    else if ((playerH.UserMoney >= gainFinish && playerH.GetNumberCardType() == 8) || (playerIA.UserMoney >= gainFinish && playerIA.GetNumberCardType() == 8)) { break; }
                }

            }
            
            screen.Clear();
            Layer endLayer = new Layer(1);
            // Conditions de fin
            if (playerH.UserMoney > playerIA.UserMoney)
            {
                Menu.DisplayEnd(screen, endLayer, true, false, buyCardIA, buyCardPlayer, 
                    gainMoneyIA, gainMoneyPlayer, lossMoneyIA, lossMoneyPlayer);
                Thread.Sleep(10000);
            }
            else if (playerIA.UserMoney > playerH.UserMoney)
            {
                Menu.DisplayEnd(screen, endLayer, false, false, buyCardIA, buyCardPlayer, 
                    gainMoneyIA, gainMoneyPlayer, lossMoneyIA, lossMoneyPlayer);
                Thread.Sleep(10000);
            }
            else // egalité des sommes d'argent
            {
                Menu.DisplayEnd(screen, endLayer, false, true, buyCardIA, buyCardPlayer, 
                    gainMoneyIA, gainMoneyPlayer, lossMoneyIA, lossMoneyPlayer);
                Thread.Sleep(10000);
            }
            screen.DisplayLayer(background);
        }

        /// <summary>
        /// Permet d'avoir une carte avec son id
        /// </summary>
        /// <param name="i">l'id de la carte que l'on veut avoir</param>
        /// <returns>La carte avec l'id qui correspond</returns>
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
            else if (i == 7) { c = new Cards().CreateStade(); }
            else if (i == 8) { c = new Cards().CreateStation(); }
            else if (i == 9) { c = new Cards().CreateCinema(); }
            else if (i == 10) { c = new Cards().CreateLegend1(); }
            else { c = new Cards().CreateLegend2(); }

            return c;
        }
        
        /// <summary>
        /// Check des cartes en main et activation des effets selon la couleur
        /// </summary>
        /// <param name="userPlayer"></param>
        /// <param name="opponentPlayer"></param>
        /// <param name="dice"></param>
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
                //Condition pour la carte Légendaire 2
                if (dice == 1 || dice == 3 || dice == 5)
                {
                    if (card.Id == 11)
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
                    //Condition pour la carte légendaire 1
                    else if (card.Id == 10)
                    {
                        opponentPlayer.UserMoney += card.Gain;
                        userPlayer.UserMoney -= card.Gain - 2;
                    }
                }
            }
        }

        /// <summary>
        /// Permet à l'ia d'effectuer son tour
        /// </summary>
        /// <param name="difficulty">La difficulté de l'ia</param>
        /// <param name="Urss">Si le jeu est en mode URSS ou pas</param>
        /// <param name="buyCardIA">La stat du nombre de carte acheter</param>
        public void actionIA(int difficulty, bool Urss, ref int buyCardIA)
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
                        buyCardIA += 1;
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
                if (c != null)
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
        
        /// <summary>
        /// Fonction qui affiche les cartes au moment de l'achat et dans le menu des cartes.
        /// </summary>
        /// <param name="Urss"></param>
        /// <param name="layer"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<Element> DisplayCards(bool Urss, Layer layer, int offset)
        {
            List<Element> cards = new List<Element>();

            for (int i = 0; i <= 11; i++)
            {
                Coordinates coordinates = new Coordinates((screen.width + offset) / 2 - 6 * (18 + 2) / 2 + i % 6 * (18 + 2) + 9,
                    screen.height / 2 - 2 * (9 + 2) / 2 + (i >= 6 ? 11 : 0) + 4);
                Element amount = new Element(new string[1] { "x " + pile.GetNumberCard(i) },
                    new Coordinates(
                        (screen.width + offset) / 2 - 6 * (18 + 2) / 2 + i % 6 * (18 + 2) + 9,
                        screen.height / 2 - 2 * (9 + 2) / 2 + (i >= 6 ? 16 : -5) + 4), Animation.None, Placement.mid,
                    ConsoleColor.White, ConsoleColor.Black);

                Element[] card = Urss ? CardChoice(i).ToElementFull(coordinates, true) : CardChoice(i).ToElementFull(coordinates, false);

                layer.Add(amount);
                layer.Add(card[0]);
                layer.Add(card[1]);
                cards.Add(card[1]);
            }
            return cards;
        }
    }
}