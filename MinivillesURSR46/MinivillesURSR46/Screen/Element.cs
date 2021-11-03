using System;
using System.Collections.Generic;
using System.Linq;

namespace MinivillesURSR46
{
    public class Element
    {
        public string[] text;
        public Coordinates coordinates;
        public Animation animation;
        public Placement placement;
        public ConsoleColor foreground;
        public ConsoleColor background;
        public bool temp { get; private set; } = false;

        public Element(Coordinates coordinates, string text)
        {
            this.text = new string[1] {text};
            this.coordinates = coordinates;

            this.animation = Animation.None;
            this.placement = Placement.topLeft;
            this.foreground = ConsoleColor.White;
            this.background = ConsoleColor.Black;

        }

        public Element(string[] text, Coordinates coordinates, Animation animation, Placement placement, ConsoleColor foreground, ConsoleColor background)
        {
            this.text = text;
            this.coordinates = coordinates;
            this.animation = animation;
            this.placement = placement;
            this.foreground = foreground;
            this.background = background;
        }

        public Element(string[] text, Coordinates coordinates, Animation animation, Placement placement, ConsoleColor foreground, ConsoleColor background, bool temp)
        {
            this.text = text;
            this.coordinates = coordinates;
            this.animation = animation;
            this.placement = placement;
            this.foreground = foreground;
            this.background = background;
            this.temp = temp;
        }
        
        /// <summary>
        /// Permet de savoir si un élément est le même qu'un autre
        /// </summary>
        /// <param name="other">L'élément avec lequel on veut comparer celui-ci</param>
        /// <returns>true si les éléments son les mêmes</returns>
        public bool CompareTo(Element other)
        {
            return this.text == other.text && this.coordinates == other.coordinates &&
                   this.animation == other.animation && this.placement == other.placement &&
                   this.foreground == other.foreground && this.background == other.background;
        }

        /// <summary>
        /// Permet de faire une copie d'un élément mais remplie d'espace
        /// </summary>
        /// <returns>L'élément vide</returns>
        public Element GetEmptyClone()
        {
            List<string> textClone = new List<string>();
        
            for (int i = 0; i < this.text.Length; i++)
            {
                textClone.Add(new string(' ', this.text[i].Length)); //On creer des string rempli d'espaces
            }
            return new Element(textClone.ToArray(), this.coordinates, Animation.None, this.placement, ConsoleColor.White, ConsoleColor.Black);
        }
    }

    public enum Animation
    {
        None,
        Typing //Fait une pose pour le reste de l'affichage

    }

    public enum Placement
    {
        topLeft,
        topRight,
        botLeft,
        botRight,
        mid
    }


    public class Coordinates
    {
        public int x;
        public int y;

        public Coordinates(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
}