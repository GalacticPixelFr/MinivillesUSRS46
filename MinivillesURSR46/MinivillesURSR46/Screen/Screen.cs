using System;
using System.Collections.Generic;
using System.Linq;

namespace MinivillesURSR46
{
    class Screen
    {
        public int height;
        public int width;

        //Dictionary<Coordinates, string[]> elements = new();
        Dictionary<int, List<Element>> layers = new();

        /// <summary>
        /// Constructeur de la classe, crée une fenêtre carré
        /// </summary>
        /// <param name="size">Permet de définir la taille d'un côté de l'écran</param>
        public Screen(int size) {
            this.height = size;
            this.width = size;
        }

        /// <summary>
        /// Constructeur de la classe, crée une fenêtre rectangulaire
        /// </summary>
        /// <param name="width">Permet de définir la longueur de l'écran</param>
        /// <param name="height">Permet de définir la largeur de l'écran</param>
        public Screen(int width, int height) {
            this.width = width;
            this.height = height;
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
            Console.Clear(); //On commence par clealr la console
            string background = string.Join("", BuildBorder()); // on crée les bord de l'écran
            Console.Write(background); //On affiche le bords

            bool recall = false; //Permet de savoir si l'éran doit être actualiser

            foreach (List<Element> layer in layers.Values)
            {
                foreach (Element element in layer)
                {
                    for (int i = 0; i < element.text.Count(); i++)
                    {

                        if (element.animation != Animation.None && element.animationIndex[i] > 0) //Si un element doit être actualisé
                        {
                            element.animationIndex[i]--;
                            recall = true;
                        }

                        //Les différentes façon de placer le texte
                        if (element.placement == Placement.topLeft)
                            Console.SetCursorPosition(element.coordinates.x, element.coordinates.y + i);

                        else if (element.placement == Placement.mid)
                            Console.SetCursorPosition(element.coordinates.x - (element.text[i].Length/2), element.coordinates.y + i);

                        else if (element.placement == Placement.topRight)
                            Console.SetCursorPosition(element.coordinates.x - element.text[i].Length, element.coordinates.y + i);
                        
                        else if (element.placement == Placement.botLeft)
                            Console.SetCursorPosition(element.coordinates.x, (element.coordinates.y+i) - element.text[i].Length);

                        else if (element.placement == Placement.botLeft)
                            Console.SetCursorPosition(element.coordinates.x - element.text[i].Length, (element.coordinates.y+i) - element.text[i].Length);

                        Console.ForegroundColor = element.foreground;
                        Console.BackgroundColor = element.background;

                        if (element.animationIndex[i] >= element.text[i].Length) element.animationIndex[i] = -1;

                        if (element.animationIndex[i] != -1)
                            Console.Write(string.Join("", element.text[i].Take(element.text[i].Length - element.animationIndex[i])));
                        else Console.Write(element.text[i]);
                    }

                    //Reset des couleurs
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            Console.SetCursorPosition(0, 0);

            if (recall) 
            {
                Thread.Sleep(100);
                Display();
            }
        }

        /// <summary>
        /// Permet d'ajouet un élément sans spécifier de layer
        /// </summary>
        /// <param name="element">L'élément à ajouter</param>
        public void Add(Element element) {
            this.Add(element, 0);
        }

        /// <summary>
        /// Permet d'ajouter un élément
        /// </summary>
        /// <param name="element">L'élément à ajouter</param>
        /// <param name="layer">Le layer sur lequel ajouter l'élément</param>
        public void Add(Element element, int layer) {
        if (!layers.ContainsKey(layer))
                layers.Add(layer, new List<Element>());
            layers[layer].Add(element);
        }

        /// <summary>
        /// Permet de supprimer les élément situer à une coordonnée
        /// </summary>
        /// <param name="coordinates">La coordonnée où supprimer les éléments</param>
        public void Delete(Coordinates coordinates) {
            foreach (List<Element> layer in this.layers)
            {
                layer.Remove(layer.FirstOrDefault(x => x.coordinates == coordinates));
            }
        }

        /// <summary>
        /// Permet de supprimer les élément situer à une coordonnée sur un layer
        /// </summary>
        /// <param name="coordinates">La coordonnée où supprimer les éléments</param>
        /// <param name="layer">Le layer sur lequel supprimer l'élément</param>
        public void Delete(Coordinates coordinates, int layer) {
            this.layers[layer].Remove(layer.FirstOrDefault(x => x.coordinates == coordinates));
        }

        /// <summary>
        /// Permet de supprimer un layer entier
        /// </summary>
        /// <param name="layer">Le layer à supprimer</param>
        public void DeleteLayer(int layer) {
            if (this.layers.ContainsKey(layer)) {
                this.layers.Remove(layer);
            }
        }

        /// <summary>
        /// Permet de clear l'écran
        /// </summary>
        public void Clear() {
            this.layers.Clear();
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
    }
}