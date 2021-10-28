using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MinivillesURSR46
{
    public class Screen
    {
        public int height;
        public int width;

        //Dictionary<Coordinates, string[]> elements = new();
        List<Layer> layers = new List<Layer>();
        Layer defaultLayer = new Layer(0);
        
        /// <summary>
        /// Constructeur de la classe, crée une fenêtre carré
        /// </summary>
        /// <param name="size">Permet de définir la taille d'un côté de l'écran</param>
        public Screen(int size) {
            this.height = size;
            this.width = size;
            this.AddLayer(defaultLayer);
            string background = string.Join("", BuildBorder()); // on crée les bord de l'écran
            Console.Write(background); //On affiche le bords
        }

        /// <summary>
        /// Constructeur de la classe, crée une fenêtre rectangulaire
        /// </summary>
        /// <param name="width">Permet de définir la longueur de l'écran</param>
        /// <param name="height">Permet de définir la largeur de l'écran</param>
        public Screen(int width, int height) {
            this.width = width;
            this.height = height;
            this.AddLayer(defaultLayer);
            string background = string.Join("", BuildBorder()); // on crée les bord de l'écran
            Console.Write(background); //On affiche le bords
        }


        /* OLD WAY
        /// <summary>
        /// Permet d'afficher l'écran dans la console
        /// </summary>
        public void Display() {
            List<string> lines = BuildBorder(); // on crée les bord de l'écran
            var layers = this.layers.OrderBy(x => x.Key).Select(x => x.Value).ToList(); // on trie les éléments à afficher en fonct
            foreach (var elements in layers)
            {
                foreach (KeyValuePair<Coordinates, string[]> element in elements) 
                {
                    int index = element.Key.x + 1;
                    for (int i = 0; i < element.Value.Count(); i++)
                    {
                        if (index + element.Value[i].Length > this.width)
                        {
                            string celuiQuiDepassePas = element.Value[i].Substring(0, (this.width - index) -1);
                            lines[element.Key.y + 1 + i] = lines[element.Key.y + 1].Remove(index, celuiQuiDepassePas.Length)
                                                                                .Insert(index, celuiQuiDepassePas);

                            Queue<string> file = new();
                            file.Enqueue(element.Value[i].Substring(celuiQuiDepassePas.Length, element.Value[i].Length - celuiQuiDepassePas.Length));
                            int lineIndex = 1;
                            while(file.Count() != 0) {
                                string elmt = file.Dequeue();
                                if (elmt.Length > this.width)
                                {
                                    Console.WriteLine(element.Value[i]);
                                    celuiQuiDepassePas = elmt.Substring(0, this.width - 2);
                                    lines[element.Key.y + 1 + lineIndex] = lines[element.Key.y + 1 + lineIndex]
                                                                                .Remove(1, celuiQuiDepassePas.Length)
                                                                                .Insert(1, celuiQuiDepassePas);

                                    file.Enqueue(elmt.Substring(celuiQuiDepassePas.Length, elmt.Length - celuiQuiDepassePas.Length));
                                } else
                                    lines[element.Key.y + 1 + lineIndex] = lines[element.Key.y + 1 + lineIndex].Remove(1, elmt.Length)
                                                                                                                .Insert(1, elmt);
                                lineIndex++;
                            }

                        } else{
                            lines[element.Key.y + 1 + i] = lines[element.Key.y + 1].Remove(index, element.Value[i].Length)
                                                                                .Insert(index, element.Value[i]);
                        }
                    }
                }
            }
            Console.Write(string.Join("", lines));
            Console.SetCursorPosition(0, 0);
        }
        */

        /// <summary>
        /// Permet d'afficher l'écran dans la console
        /// </summary>
        public void Display()
        {
            List<Element> elements = new List<Element>(); //Future liste des éléments

            foreach (Layer layer in layers.OrderBy(x => x.priority)) //On boucle sur tous les layers
            {
                DisplayLayer(layer);
            }
            Console.SetCursorPosition(0, 0); //On reset la position du cursor

        }

        public void DisplayLayer(Layer layer)
        {
            for (int i = 0; i < layer.elements.Count; i++)
            {
                DisplayElement(layer.elements[i]);
                if (layer.elements[i].temp)
                {
                    layer.Delete(layer.elements[i]);
                    i--;
                }
            }
            Console.SetCursorPosition(0, 0); //On reset la position du cursor
        }

        public void DisplayElement(Element element)
        {
            for (int i = 0; i < element.text.Length; i++)
            {
                if (element.animation != Animation.None && element.animationIndex[i] > 0) //Si un element doit être actualisé
                {
                    element.animationIndex[i]--; //On décrémente l'index de 1
                }

                SetCursorElement(element, element.text[i], i); //On positionne le cursor au bon endroit
                        
                Console.ForegroundColor = element.foreground;
                Console.BackgroundColor = element.background;

                if (element.animationIndex[i] >= element.text[i].Length) element.animationIndex[i] = -1;

                if (element.animationIndex[i] != -1) //TODO régler le problème
                {
                    Console.SetCursorPosition(Console.CursorLeft+element.text[i].Length - element.animationIndex[i], Console.CursorTop);
                    Console.Write(string.Join("", element.text[i].Skip(element.text[i].Length - element.animationIndex[i]-1).Take(1)));
                }
                else Console.Write(element.text[i]);
                        
                //Reset des couleurs
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.SetCursorPosition(0, 0); //On reset la position du cursor
        }

        public void AddLayer(Layer layer)
        {
            this.layers.Add(layer);
        }
        
        /// <summary>
        /// Permet d'ajouet un élément sans spécifier de layer
        /// </summary>
        /// <param name="element">L'élément à ajouter</param>
        public void AddElement(Element element) {
            defaultLayer.Add(element);
        }

        public void DeleteLayer(Layer layer)
        {
            HideLayer(layer);
            this.layers.Remove(layer);
        }

        public void DeleteElement(Element element)
        {
            foreach (Layer layer in layers)
            {
                foreach (Element _element in layer.elements)
                {
                    if (element.CompareTo(_element)) HideElement(element);
                    layer.Delete(element);
                    return;
                }
            }
        }

        public void HideLayer(Layer layer)
        {
            foreach (Element element in layer.elements)
            {
                HideElement(element);
            }
        }
        
        public void HideElement(Element element)
        {
            DisplayElement(element.GetEmptyClone());
        }

        private void SetCursorElement(Element element, string text, int index)
        {
            if (element.placement == Placement.topLeft)
                Console.SetCursorPosition(element.coordinates.x, element.coordinates.y + index);

            else if (element.placement == Placement.mid)
                Console.SetCursorPosition(element.coordinates.x - (text.Length/2), (element.coordinates.y - element.text.Length/2) + index);

            else if (element.placement == Placement.topRight)
                Console.SetCursorPosition(element.coordinates.x - text.Length, element.coordinates.y + index);
                        
            else if (element.placement == Placement.botLeft)
                Console.SetCursorPosition(element.coordinates.x, (element.coordinates.y+index) - text.Length);

            else if (element.placement == Placement.botLeft)
                Console.SetCursorPosition(element.coordinates.x - text.Length, (element.coordinates.y+index) - text.Length);

        }
        
        /// <summary>
        /// Permet de clear l'écran
        /// </summary>
        public void Clear() {
            this.layers.Clear();
            Console.Clear();
        }

        /// <summary>
        /// Permet de creer les bords de l'écran
        /// </summary>
        private List<string> BuildBorder() {
            string top = "+" + new String('-', this.width-2) + "+\n";
            string mid = "|" + new String(' ', this.width-2) + "|\n";
            List<string> lines = Enumerable.Repeat(mid, this.height-2).ToList();
            lines.Insert(0, top);
            lines.Add(top);
            return lines;
        }

        public int Choice(string[] choixArray, int height, Layer layer)
        {
            List<Element> choixElements = new List<Element>();
            int space = this.width / (choixArray.Length+1); //On détermine la taille entre chaque élément
            for(int i = 0; i < choixArray.Length; i++)
            {
                Element choixElement = new Element(new string[1]{choixArray[i]}, 
                                                    new Coordinates(space * (i+1), height),
                                                    Animation.None,
                                                    Placement.mid, ConsoleColor.White, ConsoleColor.Black);
                choixElements.Add(choixElement); //On ajoute l'élément créé a la liste d'éléments
                layer.Add(choixElement); //On Ajoute l'élément créé à l'écran
            }
            this.DisplayLayer(layer); //On Display l'écran
            return Select(choixElements.ToArray()); //On call Select avec les éléments précedement créé
        }

        public int Select(Element[] elementArray)
        {
            int choice = 0;
            while(true)
            {
                int newChoice = 0;

                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.RightArrow || key == ConsoleKey.UpArrow) {
                    newChoice = choice + 1;
                } else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.DownArrow) {
                    newChoice = choice - 1;
                } else if (key == ConsoleKey.Enter) {
                    break;
                }

                if (newChoice < 0) newChoice = elementArray.Length-1; //On module la variable choix
                if (newChoice >= elementArray.Length) newChoice = 0;



                elementArray[newChoice].foreground = ConsoleColor.Black;
                elementArray[newChoice].background = ConsoleColor.White;
                DisplayElement(elementArray[newChoice]);
                
                elementArray[choice].foreground = ConsoleColor.White; // On reset l'ancien élément selectionneé
                elementArray[choice].background = ConsoleColor.Black;
                DisplayElement(elementArray[choice]);

                choice = newChoice;
                
                
                Console.Write(choice); //Debug
                this.Display(); //On display l'écran avec les modifications
            }
            
            return choice;
        }
    }
}