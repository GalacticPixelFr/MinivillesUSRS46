using System;
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
    
    private static Layer background = new Layer(1);
    private static Layer selectMainMenu = new Layer(1);
    private static Layer selectGameCreation = new Layer(1);
    
    public static void Display(Screen screen)
    {
        Element titleElement = new Element(title, new Coordinates(screen.width / 2, 10),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
        
        background.Add(titleElement);
        screen.DisplayLayer(background);

        Element jouerElement = new Element(jouer, new Coordinates(5, screen.height / 5 * 2),
            Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black);
        Element creditsElement = new Element(credits, new Coordinates(5, screen.height / 5 * 3),
            Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black);
        Element quitterElement = new Element(quitter, new Coordinates(5, screen.height / 5 * 4),
            Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black);

        selectMainMenu.Add(jouerElement);
        selectMainMenu.Add(creditsElement);
        selectMainMenu.Add(quitterElement);
        screen.DisplayLayer(selectMainMenu);
        int choix = screen.Select(new Element[3] {jouerElement, creditsElement, quitterElement});
        
        if (choix == 0) DisplayCreateGame(screen);
        else if (choix == 1) DisplayCreateGame(screen);

    }

    public static void DisplayCreateGame(Screen screen)
    {
        screen.HideLayer(background);
        screen.HideLayer(selectMainMenu);
        
        GameOption gameOption = new GameOption();
        
        background.Clear();
        
        Element titleElement = new Element(creationduJeu, new Coordinates(screen.width / 2, 7),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
        background.Add(titleElement);
        
        Element dureeDeLaPartie = new Element(new Coordinates(5, screen.height / 6 * 2), "Durée de la partie :");
        Element difficultee = new Element(new Coordinates(5, screen.height / 6 * 3), "Difficultée :");
        Element modeDeJeu = new Element(new Coordinates(5, screen.height / 6 * 4), "Mode de jeu :");
        
        Element commencerElement = new Element(commencer, new Coordinates(screen.width / 2, screen.height - 7),
            Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);
        
        background.Add(dureeDeLaPartie);
        background.Add(difficultee);
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
        
        Element difficulteeFacile = new Element(new string[]{"facile"}, 
            new Coordinates(screen.width/5*2, screen.height / 6 * 3)
            ,Animation.None, Placement.mid, ConsoleColor.Black, ConsoleColor.White);
        Element difficulteeComplique = new Element(new Coordinates(screen.width/5*3, screen.height / 6 * 3), "compliquée");
        background.Add(difficulteeFacile);
        background.Add(difficulteeComplique);
        
        Element modeDeJeuURSS = new Element(new string[]{"URSS"},
            new Coordinates(screen.width/5*2, screen.height / 6 * 4)
            ,Animation.None, Placement.mid, ConsoleColor.Black, ConsoleColor.White);
        Element modeDeJeuUSA = new Element(new Coordinates(screen.width/5*3, screen.height / 6 * 4), "USA");
        background.Add(modeDeJeuURSS);
        background.Add(modeDeJeuUSA);
        
        screen.DisplayLayer(background);

        int choix = -1;
        //

        while (choix != 3)
        {
            choix = screen.Select(new Element[] {dureeDeLaPartie, difficultee, modeDeJeu, commencerElement});
            if (choix == 0)
            {
                gameOption.duree = screen.Select(new Element[3] {dureeCourt, dureeMoyen, dureeLong});
                choix = -1;
            }
            else if (choix == 1)
            {
                gameOption.difficultee = screen.Select(new Element[2] {difficulteeFacile, difficulteeComplique});
                choix = -1;
            }
            else if (choix == 2)
            {
                gameOption.modeDeJeu = screen.Select(new Element[2] {modeDeJeuURSS, modeDeJeuUSA});
                choix = -1;
            }
        }

    }

    public static void DisplayCredits(Screen screen)
    {
        //TODO
    }
}

