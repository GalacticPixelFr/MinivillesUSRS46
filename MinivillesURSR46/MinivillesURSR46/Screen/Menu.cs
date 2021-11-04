using System;
using System.Collections.Generic;
using System.Threading;
using MinivillesURSR46;

public static class Menu
{
    private static string[] title = new string[8]
    {
        @"$$\      $$\ $$\           $$\            $$\ $$\ $$\                     ",
        @"$$$\    $$$ |\__|          \__|           \__|$$ |$$ |                    ",
        @"$$$$\  $$$$ |$$\ $$$$$$$\  $$\ $$\    $$\ $$\ $$ |$$ | $$$$$$\   $$$$$$$\ ",
        @"$$\$$\$$ $$ |$$ |$$  __$$\ $$ |\$$\  $$  |$$ |$$ |$$ |$$  __$$\ $$  _____|",
        @"$$ \$$$  $$ |$$ |$$ |  $$ |$$ | \$$\$$  / $$ |$$ |$$ |$$$$$$$$ |\$$$$$$\  ",
        @"$$ |\$  /$$ |$$ |$$ |  $$ |$$ |  \$$$  /  $$ |$$ |$$ |$$   ____| \____$$\ ",
        @"$$ | \_/ $$ |$$ |$$ |  $$ |$$ |   \$  /   $$ |$$ |$$ |\$$$$$$$\ $$$$$$$  |",
        @"\__|     \__|\__|\__|  \__|\__|    \_/    \__|\__|\__| \_______|\_______/ \"
    };
    
    private static string[] city = new string[]
    {
        @"                   _________                                                         ",
        @"                  |MMMMMMMMM|                _                                       ",
        @"     ________     |MMMMMMMMM|              _|l|_                                     ",
        @"     |!!!!!!!_|___|MMMMMMMMM|             |lllll|                                    ",
        @"     |!!!!!!|=========|MMMMM|             |lllll|_______                             ",
        @"     |!!!!!!|=========|MMMMM|            _|lllll|HHHHHHH|                            ",
        @"     |!!!!!!|=========|MMMMM|   ________|lllllllll|HHHHH|                            ",
        @"     |!!!!!!|=========|MMMMM|  |unununun|lllllllll|HHHHH|______                      ",
        @"     |!!!!!!|=========|MMMMM|  |nunununu|lllllllll|HH|:::::::::|                     ",
        @"     |!!!!!!|=========|MMM__|..|un__unun|lllllllll|HH|:::::::::|                     ",
        @"     |!!!!!!|=======_=|M_( ')' );' .)unu|lllllllll|HH|:::::::::|                     ",
        @"     |!!!_!!|======( )|(. ` ,) (_ ', )un|lllllllll|HH|:::::::::| ~~~                 ",
        @"     |!!(.)!|===__(`.')_(_ ')_,)(. _)unu|lllllllll|HH|:__::::::|~~  ~~               ",
        @"     |!(.`')|==( .)' .)MMM|M|| |un|nunun|lllllllll|``|( ,)_::::| ~~~~ ~              ",
        @"      -(: _)|=(`. ')_)|---|- '  ``|`````|lll____ll|  (_; `'):::|~~~  ~~~             ",
        @"  (.)     |  |==(_'_)|=|    ______        ''/\   \'   |(_'_)::::|\~~~~__|)__         ",
        @"  (_')       |   ''''|''o/`.-``~~~~~ ``-.     /--\___\    ``|`````` /____\____/      ",
        @" (: _)rei        |  h ( `; ~~~ ~~  ~ )    |M_|#_#|      ' --   __________|~~~~   ~~~ ",
        @"   |       --   *      '-.._~~__~..-'   --           -* -     /  ~~~~ ~~~~~~  ~~~~   ",
        @"     *   -   -      --           ----         ---         _.-'~~~~~     ~ ~~~        ",
        @"_______--_________............-------------'''''''''''''''` ~~~~~    ~~~ ~~~~~    ~~~",
        @"     ~~    ~~~~~~~~     ~~~~~~~   ~~~~~~~~~   ~~~~~~~~~~      ~~~~~~~     ~~~  ~~    ",
        @"~~   ~~~~~~~~~  ~~~~  ~~~~~ ~~~~~~~~~ ~ ~      ~~~~~~ ~~~~~~     ~~~~    ~~~~~~~~    ",
        @"  ~~ ~~~~~~~~     ~~~~~~~~~~~~~~~        ~~~~~~~~~~~~ ~~~~~~  ~~~ ~~~~~~  ~~~   ~~   "
    };

    private static string[] jouer = new string[6]
    {
        @"   ___                       ",
        @"  |_  |                      ",
        @"    | | ___  _   _  ___ _ __ ",
        @"    | |/ _ \| | | |/ _ \ '__|",
        @"/\__/ / (_) | |_| |  __/ |   ",
        @"\____/ \___/ \__,_|\___|_|   "
    };

    private static string[] cartes = new string[6]
    {
        @" _____            _            ",
        @"/ ____|          | |           ",
        @"| |     __ _ _ __| |_ ___  ___ ",
        @"| |    / _` | '__| __/ _ \/ __|",
        @"| |___| (_| | |  | ||  __/\__ \",
        @"\______\__,_|_|   \__\___||___/"
    };

    private static string[] credits = new string[6]
    {
        @" _____              _ _ _       ",
        @"/  __ \            | (_) |      ",
        @"| /  \/_ __ ___  __| |_| |_ ___ ",
        @"| |   | '__/ _ \/ _` | | __/ __|",
        @"| \__/\ | |  __/ (_| | | |_\__ \",
        @" \____/_|  \___|\__,_|_|\__|___/"
    };
    
    private static string[] quitter = new string[6]
    {
        @" _____       _ _   _            ",
        @"|  _  |     (_) | | |           ",
        @"| | | |_   _ _| |_| |_ ___ _ __ ",
        @"| | | | | | | | __| __/ _ \ '__|",
        @"\ \/' / |_| | | |_| ||  __/ |   ",
        @" \_/\_\\__,_|_|\__|\__\___|_|   ",
    };

    private static string[] creationDuJeu = new string[8]
    {
        @" _____                _   _                   _           _            ",
        @"/  __ \              | | (_)                 | |         (_)           ",
        @"| /  \/_ __ ___  __ _| |_ _  ___  _ __     __| |_   _     _  ___ _   _ ",
        @"| |   | '__/ _ \/ _` | __| |/ _ \| '_ \   / _` | | | |   | |/ _ \ | | |",
        @"| \__/\ | |  __/ (_| | |_| | (_) | | | | | (_| | |_| |   | |  __/ |_| |",
        @" \____/_|  \___|\__,_|\__|_|\___/|_| |_|  \__,_|\__,_|   | |\___|\__,_|",
        @"                                                        _/ |           ",
        @"                                                       |__/            "
    };

    private static string[] commencer = new string[]
    {
        @" _____                                                    ",
        @"/  __ \                                                   ",
        @"| /  \/ ___  _ __ ___  _ __ ___   ___ _ __   ___ ___ _ __ ",
        @"| |    / _ \| '_ ` _ \| '_ ` _ \ / _ \ '_ \ / __/ _ \ '__|",
        @"| \__/\ (_) | | | | | | | | | | |  __/ | | | (_|  __/ |   ",
        @" \____/\___/|_| |_| |_|_| |_| |_|\___|_| |_|\___\___|_|   ",
    };

    private static string[] retour = new string[]
    {
        @" _____      _                   ",
        @"|  __ \    | |                  ",
        @"| |__) |___| |_ ___  _   _ _ __ ",
        @"|  _  // _ \ __/ _ \| | | | '__|",
        @"| | \ \  __/ || (_) | |_| | |   ",
        @"|_|  \_\___|\__\___/ \__,_|_|   "
    };

    private static string[] jordan = new string[]
    {
        @"   ___               _              ______                       _   ",
        @"  |_  |             | |             | ___ \                     | |  ",
        @"    | | ___  _ __ __| | __ _ _ __   | |_/ /_   _ _ __ _ __   ___| |_ ",
        @"    | |/ _ \| '__/ _` |/ _` | '_ \  | ___ \ | | | '__| '_ \ / _ \ __|",
        @"/\__/ / (_) | | | (_| | (_| | | | | | |_/ / |_| | |  | | | |  __/ |_ ",
        @"\____/ \___/|_|  \__,_|\__,_|_| |_| \____/ \__,_|_|  |_| |_|\___|\__|",
    };

    private static string[] didier = new string[]
    {
        @" ___  ___      _   _     _            ______ _     _ _           ",
        @" |  \/  |     | | | |   (_)           |  _  (_)   | (_)          ",
        @" | .  . | __ _| |_| |__  _  __ _ ___  | | | |_  __| |_  ___ _ __ ",
        @" | |\/| |/ _` | __| '_ \| |/ _` / __| | | | | |/ _` | |/ _ \ '__|",
        @" | |  | | (_| | |_| | | | | (_| \__ \ | |/ /| | (_| | |  __/ |   ",
        @" \_|  |_/\__,_|\__|_| |_|_|\__,_|___/ |___/ |_|\__,_|_|\___|_|   ",
    };

    private static string[] camille = new string[]
    {
        @" _____                 _ _ _       ______    _      ",
        @"/  __ \               (_) | |      | ___ \  | |     ",
        @"| /  \/ __ _ _ __ ___  _| | | ___  | |_/ /__| | ___ ",
        @"| |    / _` | '_ ` _ \| | | |/ _ \ |  __/ _ \ |/ _ \",
        @"| \__/\ (_| | | | | | | | | |  __/ | | |  __/ |  __/",
        @" \____/\__,_|_| |_| |_|_|_|_|\___| \_|  \___|_|\___|"
    };

    private static string[] statsIA = new string[]
    {
        @"  _____ _        _       ",
        @" / ____| |      | |      ",
        @"| (___ | |_ __ _| |_ ___ ",
        @" \___ \| __/ _` | __/ __|",
        @" ____) | || (_| | |_\__ \",
        @"|_____/_\__\__,_|\__|___/",
        @"    |_   _|   /\         ",
        @"      | |    /  \        ",
        @"      | |   / /\ \       ",
        @"     _| |_ / ____ \      ",
        @"    |_____/_/    \_\     "
    };

    private static string[] statsJoueur = new string[]
    {
        @"       _____ _        _             ",
        @"      / ____| |      | |            ",
        @"     | (___ | |_ __ _| |_ ___       ",
        @"      \___ \| __/ _` | __/ __|      ",
        @"      ____) | || (_| | |_\__ \      ",
        @"     |_____/ \__\__,_|\__|___/      ",
        @"     | |                            ",
        @"     | | ___  _   _  ___ _   _ _ __ ",
        @" _   | |/ _ \| | | |/ _ \ | | | '__|",
        @"| |__| | (_) | |_| |  __/ |_| | |   ",
        @" \____/ \___/ \__,_|\___|\__,_|_|   "
    };

    private static string[] gagne = new string[]
    {
        @" $$$$$$\                                          ",
        @"$$  __$$\                                         ",
        @"$$ /  \__| $$$$$$\   $$$$$$\  $$$$$$$\   $$$$$$\  ",
        @"$$ |$$$$\  \____$$\ $$  __$$\ $$  __$$\ $$  __$$\ ",
        @"$$ |\_$$ | $$$$$$$ |$$ /  $$ |$$ |  $$ |$$$$$$$$ |",
        @"$$ |  $$ |$$  __$$ |$$ |  $$ |$$ |  $$ |$$   ____|",
        @"\$$$$$$  |\$$$$$$$ |\$$$$$$$ |$$ |  $$ |\$$$$$$$\ ",
        @" \______/  \_______| \____$$ |\__|  \__| \_______|",
        @"                    $$\   $$ |                    ",
        @"                    \$$$$$$  |                    ",
        @"                     \______/                     "
    };

    private static string[] perdu = new string[]
    {
        @"$$$$$$$\                            $$\           ",
        @"$$  __$$\                           $$ |          ",
        @"$$ |  $$ | $$$$$$\   $$$$$$\   $$$$$$$ |$$\   $$\ ",
        @"$$$$$$$  |$$  __$$\ $$  __$$\ $$  __$$ |$$ |  $$ |",
        @"$$  ____/ $$$$$$$$ |$$ |  \__|$$ /  $$ |$$ |  $$ |",
        @"$$ |      $$   ____|$$ |      $$ |  $$ |$$ |  $$ |",
        @"$$ |      \$$$$$$$\ $$ |      \$$$$$$$ |\$$$$$$  |",
        @"\__|       \_______|\__|       \_______| \______/ "
    };

    private static string[] egal = new string[]
    {
        @"$$$$$$$$\                    $$\ $$\   $$\               ",
        @"$$  _____|                   $$ |\__|  $$ |              ",
        @"$$ |      $$$$$$\   $$$$$$\  $$ |$$\ $$$$$$\    $$$$$$\  ",
        @"$$$$$\   $$  __$$\  \____$$\ $$ |$$ |\_$$  _|  $$  __$$\ ",
        @"$$  __|  $$ /  $$ | $$$$$$$ |$$ |$$ |  $$ |    $$$$$$$$ |",
        @"$$ |     $$ |  $$ |$$  __$$ |$$ |$$ |  $$ |$$\ $$   ____|",
        @"$$$$$$$$\\$$$$$$$ |\$$$$$$$ |$$ |$$ |  \$$$$  |\$$$$$$$\ ",
        @"\________|\____$$ | \_______|\__|\__|   \____/  \_______|",
        @"         $$\   $$ |                                      ",
        @"         \$$$$$$  |                                      ",
        @"          \______/                                       "
    };

    private static string[][] numberList = new string[][]
    {
        new string[] {
            @"  ___  ",
            @" / _ \ ",
            @"| | | |",
            @"| | | |",
            @"| |_| |",
            @" \___/ "
        },
        new string[] {
            @" __ ",
            @"/_ |",
            @" | |",
            @" | |",
            @" | |",
            @" |_|",
        },
        new string[] {
            @" ___  ",
            @"|__ \ ",
            @"   ) |",
            @"  / / ",
            @" / /_ ",
            @"|____|"
        },
        new string[] {
            @" ____  ",
            @"|___ \ ",
            @"  __) |",
            @" |__ < ",
            @" ___) |",
            @"|____/ "
        },
        new string[] {
            @" _  _   ",
            @"| || |  ",
            @"| || |_ ",
            @"|__   _|",
            @"   | |  ",
            @"   |_|  "
        },
        new string[] {
            @" _____ ",
            @"| ____|",
            @"| |__  ",
            @"|___ \ ",
            @" ___) |",
            @"|____/ "
        },
        new string[] {
            @"   __  ",
            @"  / /  ",
            @" / /_  ",
            @"| '_ \ ",
            @"| (_) |",
            @" \___/ "
        },
        new string[] {
            @" ______ ",
            @"|____  |",
            @"    / / ",
            @"   / /  ",
            @"  / /   ",
            @" /_/    "
        },
        new string[] {
            @"  ___  ",
            @" / _ \ ",
            @"| (_) |",
            @" > _ < ",
            @"| (_) |",
            @" \___/ "
        },
        new string[] {
            @"  ___  ",
            @" / _ \ ",
            @"| (_) |",
            @" \__, |",
            @"   / / ",
            @"  /_/  "
        }
    };
    
    private static Layer background = new Layer(1);
    private static Layer selectMainMenu = new Layer(1);
    
    public static bool Display(Screen screen, Game game)
    {
        if (Console.BufferWidth < screen.width) Console.BufferWidth = screen.width;
        if (Console.BufferHeight < screen.height) Console.BufferHeight = screen.height;

        screen.HideLayer(background);
        screen.HideLayer(selectMainMenu);
        background.Clear();
            
        Element titleElement = new Element(title, new Coordinates(screen.width / 2, 10),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
        
        Element cityElement = new Element(city, new Coordinates(screen.width / 6 *4, 30),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);

        background.Add(titleElement);
        background.Add(cityElement);
        screen.DisplayLayer(background);

        Element jouerElement = new Element(jouer, new Coordinates(5, screen.height / 5 * 1),
            Animation.None, Placement.topLeft, ConsoleColor.Black, ConsoleColor.White);
        Element cartesElement = new Element(cartes, new Coordinates(5, screen.height / 5 * 2),
            Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black);
        Element creditsElement = new Element(credits, new Coordinates(5, screen.height / 5 * 3),
            Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black);
        Element quitterElement = new Element(quitter, new Coordinates(5, screen.height / 5 * 4),
            Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black);

        selectMainMenu.Add(jouerElement);
        selectMainMenu.Add(cartesElement);
        selectMainMenu.Add(creditsElement);
        selectMainMenu.Add(quitterElement);
        screen.DisplayLayer(selectMainMenu);
      
        int choix = screen.Select(new Element[] {jouerElement, cartesElement, creditsElement, quitterElement});
        
        if (choix == 0) return DisplayCreateGame(screen, game);
        if (choix == 1) return DisplayCards(screen, game);
        else if (choix == 2) return DisplayCredits(screen);
        return true;

    }

    private static bool DisplayCreateGame(Screen screen, Game game)
    {
        screen.HideLayer(background);
        screen.HideLayer(selectMainMenu);
        
        GameOption gameOption = new GameOption();
        
        background.Clear();
        
        Element titleElement = new Element(creationDuJeu, new Coordinates(screen.width / 2, 7),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
        background.Add(titleElement);
        
        
        Element dureeDeLaPartie = new Element(new string[]{"Durée de la partie :"},
            new Coordinates(5, screen.height / 7 * 2),
            Animation.None, Placement.topLeft, ConsoleColor.Black, ConsoleColor.White);
        Element difficultee = new Element(new Coordinates(5, screen.height / 7 * 3), "Difficultée :");
        Element niveauIA = new Element(new Coordinates(5, screen.height / 7 * 4), "Niveau de l'IA :");
        Element modeDeJeu = new Element(new Coordinates(5, screen.height / 7 * 5), "Mode de jeu :");
        
        Element commencerElement = new Element(commencer, new Coordinates(screen.width / 3*1, screen.height - 5),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);

        Element retourElement = new Element(retour, new Coordinates(screen.width / 3*2, screen.height - 5),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);

        background.Add(dureeDeLaPartie);
        background.Add(difficultee);
        background.Add(niveauIA);
        background.Add(modeDeJeu);
        background.Add(commencerElement);
        background.Add(retourElement);
        
        Element dureeCourt = new Element( new string[]{"court"},
            new Coordinates(screen.width/8*3, screen.height / 7 * 2),
            Animation.None, Placement.mid, ConsoleColor.Black, ConsoleColor.White);
        Element dureeMoyen = new Element(new Coordinates(screen.width/8*4, screen.height / 7 * 2), "moyen");
        Element dureeLong = new Element(new Coordinates(screen.width/8*5, screen.height / 7 * 2), "long");
        background.Add(dureeCourt);
        background.Add(dureeMoyen);
        background.Add(dureeLong);
        
        Element difficulteeNormale = new Element(new string[]{"normale"}, 
            new Coordinates(screen.width/5*2, screen.height / 7 * 3)
            ,Animation.None, Placement.mid, ConsoleColor.Black, ConsoleColor.White);
        Element difficulteeExpert = new Element(new Coordinates(screen.width/5*3, screen.height / 7 * 3), "expert");
        background.Add(difficulteeNormale);
        background.Add(difficulteeExpert);
        
        Element niveauIADebile = new Element(new string[]{"débile"}, 
            new Coordinates(screen.width/5*2, screen.height / 7 * 4)
            ,Animation.None, Placement.mid, ConsoleColor.Black, ConsoleColor.White);
        Element niveauIAGenie = new Element(new Coordinates(screen.width/5*3, screen.height / 7 * 4), "génie");
        background.Add(niveauIADebile);
        background.Add(niveauIAGenie);
        
        Element modeDeJeuURSS = new Element(new string[]{"URSS"},
            new Coordinates(screen.width/5*2, screen.height / 7 * 5)
            ,Animation.None, Placement.mid, ConsoleColor.Black, ConsoleColor.White);
        Element modeDeJeuUSA = new Element(new Coordinates(screen.width/5*3, screen.height / 7 * 5), "USA");
        background.Add(modeDeJeuURSS);
        background.Add(modeDeJeuUSA);
        
        screen.DisplayLayer(background);

        int choix = -1;
        int startPosition = 0;

        while (choix != 4)
        {
            choix = screen.Select(new Element[] {dureeDeLaPartie, difficultee, niveauIA, modeDeJeu, commencerElement, retourElement}, startPosition);
            if (choix == 0)
            {
                gameOption.duree = screen.Select(new Element[3] {dureeCourt, dureeMoyen, dureeLong}, gameOption.duree);
                startPosition = 0;
                choix = -1;
            }
            else if (choix == 1)
            {
                gameOption.difficultee = screen.Select(new Element[2] {difficulteeNormale, difficulteeExpert}, gameOption.difficultee);
                startPosition = 1;
                choix = -1;
            }
            else if (choix == 2)
            {
                gameOption.niveauIA = screen.Select(new Element[2] {niveauIADebile, niveauIAGenie}, gameOption.niveauIA);
                startPosition = 2;
                choix = -1;
            }
            else if (choix == 3)
            {
                gameOption.modeDeJeu = screen.Select(new Element[2] {modeDeJeuURSS, modeDeJeuUSA}, gameOption.modeDeJeu);
                startPosition = 3;
                choix = -1;
            }
            else if (choix == 5)
            {
                return false;
            }
        }
        screen.HideLayer(background);
        background.Clear();
        game.Run(gameOption);
        return false;
    }

    private static bool DisplayCredits(Screen screen)
    {
        screen.HideLayer(background);
        screen.HideLayer(selectMainMenu);
        background.Clear();
        
        Element jordanElement = new Element(jordan, new Coordinates(screen.width / 2, screen.height / 4*1),
            Animation.Typing, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
        Element didierElement = new Element(didier, new Coordinates(screen.width / 2, screen.height / 4*2),
            Animation.Typing, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
        Element camilleElement = new Element(camille, new Coordinates(screen.width / 2, screen.height / 4*3),
            Animation.Typing, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
        background.Add(jordanElement);
        background.Add(didierElement);
        background.Add(camilleElement);

        screen.DisplayLayer(background);
        
        Thread.Sleep(3000);
        return false;
    }

    /// <summary>
    /// Permet d'afficher l'écran de fin de partie
    /// </summary>
    /// <param name="screen">le screen qui sert à affichjer le rendu</param>
    /// <param name="layer">la layer sur lequel mettre les éléments</param>
    /// <param name="win">si c'est une victoire ou pas</param>
    /// <param name="buyCard1">le nombre de cartes acheté par le joueur de gauche</param>
    /// <param name="buyCard2">le nombre de cartes acheté par le joueur de droite</param>
    /// <param name="gainMoney1">le montant d'argent amassé par le joueur de gauche</param>
    /// <param name="gainMoney2">le montant d'argent amassé par le joueur de droite</param>
    /// <param name="lossMoney1">le montant d'argent perdu par le joueur de gauche</param>
    /// <param name="lossMoney2">le montant d'argent perdu par le joueur de droite</param>
    public static void DisplayEnd(Screen screen, Layer layer, bool win, bool equality, int buyCard1, int buyCard2, int gainMoney1,
        int gainMoney2, int lossMoney1, int lossMoney2)
    {
        //Ajout de la bordure de gauche
        layer.Add(new Element(Screen.BuildBorder(57, screen.height).ToArray(), 
            new Coordinates(0, 0), Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout de la bordure de droite
        layer.Add(new Element(Screen.BuildBorder(57, screen.height).ToArray(), 
            new Coordinates(screen.width-57, 0), Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout du titre du milieu
        layer.Add(new Element((win ? gagne : equality ? egal : perdu), new Coordinates(screen.width / 2, screen.height / 2),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout du titre de gauche
        layer.Add(new Element(statsIA, new Coordinates(28, 7),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout du titre de gauche
        layer.Add(new Element(statsJoueur, new Coordinates(screen.width-28, 7),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un petit titre à gauche
        layer.Add(new Element(new string[]{"nombres de cartes acheté"}, new Coordinates(28, 15),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un petit titre à droite
        layer.Add(new Element(new string[]{"nombres de cartes acheté"}, new Coordinates(screen.width-28, 15),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un nombre à gauche
        layer.Add(new Element(GetAsciiNumber(buyCard1), new Coordinates(28, 20),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un nombre à droite
        layer.Add(new Element(GetAsciiNumber(buyCard2), new Coordinates(screen.width-28, 20),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un petit titre à gauche
        layer.Add(new Element(new string[]{"montant d'argent amassé au total"}, new Coordinates(28, 25),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un petit titre à droite
        layer.Add(new Element(new string[]{"montant d'argent amassé au total"}, new Coordinates(screen.width-28, 25),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un nombre à gauche
        layer.Add(new Element(GetAsciiNumber(gainMoney1), new Coordinates(28, 30),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un nombre à droite
        layer.Add(new Element(GetAsciiNumber(gainMoney2), new Coordinates(screen.width-28, 30),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un petit titre à gauche
        layer.Add(new Element(new string[]{"montant d'argent perdu"}, new Coordinates(28, 35),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un petit titre à droite
        layer.Add(new Element(new string[]{"montant d'argent perdu"}, new Coordinates(screen.width-28, 35),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un nombre à gauche
        layer.Add(new Element(GetAsciiNumber(lossMoney1), new Coordinates(28, 40),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        //Ajout d'un nombre à droite
        layer.Add(new Element(GetAsciiNumber(lossMoney2), new Coordinates(screen.width-28, 40),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black));
        
        screen.DisplayLayer(layer);
    }
    
    /// <summary>
    /// Permet de transformer un int en code ascii
    /// </summary>
    /// <param name="number">le nombre que l'on veut transformer</param>
    /// <returns>la liste des lignes</returns>
    public static string[] GetAsciiNumber(int number)
    {
        string stringInt = number.ToString();
        string[] result = new string[] {"", "", "", "", "", ""};
        
        for (int i = 0; i < stringInt.Length; i++)
        {
            int index;
            if (int.TryParse(stringInt[i].ToString(), out index))
            {
                for (int j = 0; j < result.Length; j++)
                {
                    result[j] += numberList[index][j];
                }
            }
        }
        return result;
    }
    
    /// <summary>
    /// Permet d'afficher les l'explication des cartes
    /// </summary>
    /// <param name="screen">l'écran sur lequel afficher le menu</param>
    /// <param name="game">pour accerder à la fonction Display cards</param>
    private static bool DisplayCards(Screen screen, Game game)
    {
        screen.HideLayer(background);
        screen.HideLayer(selectMainMenu);
        background.Clear();
        game.InitializePile();

        // affichage des cartes
        int choix = 0;
        while (true)
        {
            List<Element> cards = game.DisplayCards(false, background, 0);

            foreach (Element e in cards)
            {
                background.Add(e);
            }

            Element back = new Element(retour,
                                        new Coordinates(screen.width / 2, screen.height - 8),
                                        Animation.None,
                                        Placement.mid,
                                        ConsoleColor.White,
                                        ConsoleColor.Black);
            cards.Add(back);
            background.Add(back); //go back

            screen.DisplayLayer(background);

            choix = screen.Select(cards.ToArray(), choix);
            screen.HideLayer(background);
            background.Clear();

            if (choix == cards.Count -1) { break; }

            // affichage info carte
            background.Add(new Element(new Coordinates(screen.width / 5 * 2 - 30, screen.height / 2 - 6), "Version USA              Version URSS"));

            Cards c = new Cards();
            CardsInfo ci = c.EachCards[choix]; 
            Element[] card = ci.ToElementFull(new Coordinates(screen.width / 5 * 2 - 25, screen.height / 2), false); // version USA
            background.Add(card[0]);
            background.Add(card[1]);

            card = ci.ToElementFull(new Coordinates(screen.width / 5 * 2, screen.height / 2), true); // version URSS
            background.Add(card[0]);
            background.Add(card[1]);

            // affichage informations
            string couleur = "";
            if (ci.Color == Color.Bleu){couleur = "Carte Bleue : s'active lors de chaque tour"; }
            else if (ci.Color == Color.Rouge){couleur = "Carte Rouge : s'active lors du tour adverse"; }
            else if (ci.Color == Color.Vert){couleur = "Carte Verte : s'active lors de votre tour"; }
            else if (ci.Color == Color.Jaune && ci.Id == 10) { couleur = "Carte Jaune/Rouge : Légendaire (unique), s'active lors du tour adverse"; }
            else if (ci.Color == Color.Jaune && ci.Id == 11) { couleur = "Carte Jaune/Bleue : Légendaire (unique), s'active lors de votre tour"; }

            string de = $"S'active pour un dé valant {ci.Dice}";
            if (ci.Id == 11) { de = "S'active pour un dé valant 1, 3 ou 5"; }

            background.Add(new Element(new String[7] {couleur, 
                                                      $" ",
                                                      de,
                                                      $" ",
                                                      $"Capacité : {ci.Effect}",
                                                      $" ",
                                                      $"Prix : {ci.Cost} pieces"},
                                        new Coordinates(screen.width / 5 * 2 + 15, screen.height / 2 - 3),
                                        Animation.None,
                                        Placement.topLeft,
                                        ConsoleColor.White,
                                        ConsoleColor.Black)); // info carte

            background.Add(new Element(new String[1] {"Pressez entré pour retourner en arrière"},
                                        new Coordinates(screen.width / 2, screen.height - 2),
                                        Animation.None,
                                        Placement.mid,
                                        ConsoleColor.White,
                                        ConsoleColor.Black)); // press enter

            screen.DisplayLayer(background);
            while(Console.ReadKey().Key != ConsoleKey.Enter){ } // attente enter presser

            screen.HideLayer(background);
            background.Clear();
        }

        return false;
    }

}