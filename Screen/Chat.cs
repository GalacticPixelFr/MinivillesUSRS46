using System;
using System.Collections.Generic;
using System.Linq;

namespace MinivillesURSR46
{
    public class Chat
    {
        private Layer background;
        private Layer textLayer;
        private Stack<string[]> textStack;
        private Screen screen;
        
        public Coordinates coordinates;
        public int height { get; private set; }
        public int width { get; private set; }

        public Chat(Screen screen, Coordinates coordinates, int width, int height)
        {
            this.background = new Layer(0);
            this.textLayer = new Layer(0);
            this.textStack = new Stack<string[]>();

            this.screen = screen;
            this.coordinates = coordinates;
            this.height = height;
            this.width = width;
            
            WriteBorders();
        }

        /// <summary>
        /// Permet d'afficher les contours du chat
        /// </summary>
        public void WriteBorders()
        {
            List<string> lines = Screen.BuildBorder(this.width, this.height);
            background.Add(new Element(lines.ToArray(), new Coordinates(this.coordinates.x, this.coordinates.y-height),
                            Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black));
            screen.DisplayLayer(background);
        }

        /// <summary>
        /// Permet d'ajouter un élément dans le chat
        /// </summary>
        /// <param name="text">le texte à ajouter</param>
        public void AddText(string text)
        {
            textStack.Push(ReturnToLine(text, this.width-2)); //On ajoutes les lignes à la pile du chat

            Display(); //On affiche le chat une fois l'élément ajouté
        }

        public static string[] ReturnToLine(string text, int width)
        {
            List<string> lines = new List<string>(); //Les différentes lignes du textes
            Stack<string> stack = new Stack<string>(); //La pile qui va permettre de couper le texte
            stack.Push(text);
            while (stack.Count > 0)
            {
                string currentString = stack.Pop(); //On récupère le dernier élément ajouté

                if (currentString.Length > width) //Si le texte dépasse du chat
                {
                    int indexOfSpace = width; //L'index du dernier espace dans le texte, si il n'y a aucun espace on en imagine un à la fin
                    //On cherche ou se trouve le dernier espace dans le texte
                    for (int i = 0; i <= width ; i++)
                    {
                        if (currentString[i] == ' ')
                        {
                            indexOfSpace = i;
                        }
                    }
                    lines.Add(currentString.Substring(0, indexOfSpace)); //On ajoute la partie qui dépasse pas dans les lignes
                    
                    stack.Push(currentString.Substring(indexOfSpace + 1)); //On ajoute la partie qui dépasse dans la pile
                }
                else lines.Add(currentString); //Si le texte dépasse pas on l'ajoute en entier au lignes
            }
            return lines.ToArray();
        }

        public void Display()
        {
            screen.HideLayer(textLayer); //On efface le chat
            textLayer.Clear(); //Puis on le clear (car tous les éléments on changé)
            
            Stack<string[]> stack = new Stack<string[]>(textStack.Reverse()); //On copie la pile du chat en la renversant
            int height = 0; //La hauteur à laquel se situe l'élément à ajouter
            int index = 0; //L'index de lélément, pour savoir si c'est le premier
            Element firstElement = new Element(coordinates, ""); //On instancie le premier élément pour pouvoir l'afficher en dernier et différement
            while (stack.Count > 0)
            {
                string[] currentString = stack.Pop(); //On récupère le dernier élément ajouté
                height += currentString.Length; //On calcule la hauteur à laquel il doit être afficher
                if (height >= this.height-1) break; //Si la hauteur est trop haute on l'affiche pas

                if (index == 0) //Si c'est le premier élément
                {
                    firstElement = new Element(currentString, 
                        new Coordinates(coordinates.x+1, coordinates.y - height-1),
                        Animation.Typing, Placement.topLeft, //On met son animation en Typing pour faire plus beau
                        ConsoleColor.White, ConsoleColor.Black);
                }
                
                else
                {
                    Element element = new Element(currentString,
                        new Coordinates(coordinates.x + 1, coordinates.y - height - 1),
                        Animation.None, Placement.topLeft,
                        ConsoleColor.White, ConsoleColor.Black);

                    textLayer.Add(element);
                }
                index++;
            }

            textLayer.Add(firstElement); //On ajoute le permier élément pour qu'il afficher en dernier
            screen.DisplayLayer(textLayer);
        }
        
    }
}