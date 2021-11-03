using System;
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

    private static string[] creationduJeu = new string[8]
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
    
    private static Layer background = new Layer(1);
    private static Layer selectMainMenu = new Layer(1);
    
    public static bool Display(Screen screen, Game game)
    {
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

        Element jouerElement = new Element(jouer, new Coordinates(5, screen.height / 5 * 2),
            Animation.None, Placement.topLeft, ConsoleColor.Black, ConsoleColor.White);
        Element creditsElement = new Element(credits, new Coordinates(5, screen.height / 5 * 3),
            Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black);
        Element quitterElement = new Element(quitter, new Coordinates(5, screen.height / 5 * 4),
            Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black);

        selectMainMenu.Add(jouerElement);
        selectMainMenu.Add(creditsElement);
        selectMainMenu.Add(quitterElement);
        screen.DisplayLayer(selectMainMenu);
        int choix = screen.Select(new Element[3] {jouerElement, creditsElement, quitterElement});
        
        if (choix == 0) return DisplayCreateGame(screen, game);
        else if (choix == 1) return DisplayCredits(screen);
        return true;

    }

    public static bool DisplayCreateGame(Screen screen, Game game)
    {
        screen.HideLayer(background);
        screen.HideLayer(selectMainMenu);
        
        GameOption gameOption = new GameOption();
        
        background.Clear();
        
        Element titleElement = new Element(creationduJeu, new Coordinates(screen.width / 2, 7),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
        background.Add(titleElement);
        
        Element dureeDeLaPartie = new Element(new string[]{"Durée de la partie :"},
            new Coordinates(5, screen.height / 6 * 2),
            Animation.None, Placement.topLeft, ConsoleColor.Black, ConsoleColor.White);
        Element difficultee = new Element(new Coordinates(5, screen.height / 6 * 3), "Difficultée :");
        Element niveauIA = new Element(new Coordinates(5, screen.height / 6 * 4), "Niveau de l'IA :");
        Element modeDeJeu = new Element(new Coordinates(5, screen.height / 6 * 5), "Mode de jeu :");
        
        Element commencerElement = new Element(commencer, new Coordinates(screen.width / 2, screen.height - 5),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
        
        background.Add(dureeDeLaPartie);
        background.Add(difficultee);
        background.Add(niveauIA);
        background.Add(modeDeJeu);
        background.Add(commencerElement);
        
        Element dureeCourt = new Element( new string[]{"court"},
            new Coordinates(screen.width/8*3, screen.height / 6 * 2),
            Animation.None, Placement.mid, ConsoleColor.Black, ConsoleColor.White);
        Element dureeMoyen = new Element(new Coordinates(screen.width/8*4, screen.height / 6 * 2), "moyen");
        Element dureeLong = new Element(new Coordinates(screen.width/8*5, screen.height / 6 * 2), "long");
        background.Add(dureeCourt);
        background.Add(dureeMoyen);
        background.Add(dureeLong);
        
        Element difficulteeNormale = new Element(new string[]{"normale"}, 
            new Coordinates(screen.width/5*2, screen.height / 6 * 3)
            ,Animation.None, Placement.mid, ConsoleColor.Black, ConsoleColor.White);
        Element difficulteeExpert = new Element(new Coordinates(screen.width/5*3, screen.height / 6 * 3), "expert");
        background.Add(difficulteeNormale);
        background.Add(difficulteeExpert);
        
        Element niveauIADebile = new Element(new string[]{"débile"}, 
            new Coordinates(screen.width/5*2, screen.height / 6 * 4)
            ,Animation.None, Placement.mid, ConsoleColor.Black, ConsoleColor.White);
        Element niveauIAGenie = new Element(new Coordinates(screen.width/5*3, screen.height / 6 * 4), "génie");
        background.Add(niveauIADebile);
        background.Add(niveauIAGenie);
        
        Element modeDeJeuURSS = new Element(new string[]{"URSS"},
            new Coordinates(screen.width/5*2, screen.height / 6 * 5)
            ,Animation.None, Placement.mid, ConsoleColor.Black, ConsoleColor.White);
        Element modeDeJeuUSA = new Element(new Coordinates(screen.width/5*3, screen.height / 6 * 5), "USA");
        background.Add(modeDeJeuURSS);
        background.Add(modeDeJeuUSA);
        
        screen.DisplayLayer(background);

        int choix = -1;
        int startPosition = 0;

        while (choix != 4)
        {
            choix = screen.Select(new Element[] {dureeDeLaPartie, difficultee, niveauIA, modeDeJeu, commencerElement}, startPosition);
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
        }
        screen.HideLayer(background);
        background.Clear();
        game.Run(gameOption);
        return false;
    }

    public static bool DisplayCredits(Screen screen)
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
}

