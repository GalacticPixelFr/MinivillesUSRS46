using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MinivillesURSR46
{
    public class Screen
    {
        public int height;
        public int width;
        
        List<Layer> layers = new List<Layer>();
        Layer defaultLayer = new Layer(0);
        
        /// <summary>
        /// Constructeur de la classe, crée une fenêtre carré
        /// </summary>
        /// <param name="size">Permet de définir la taille d'un côté de l'écran</param>
        public Screen(int size) {
            this.height = size;
            this.width = size;
            Initialize();
        }

        /// <summary>
        /// Constructeur de la classe, crée une fenêtre rectangulaire
        /// </summary>
        /// <param name="width">Permet de définir la longueur de l'écran</param>
        /// <param name="height">Permet de définir la largeur de l'écran</param>
        public Screen(int width, int height) {
            this.width = width;
            this.height = height;
            Initialize();
        }

        /// <summary>
        /// Initialise le screen
        /// </summary>
        private void Initialize()
        {
            this.AddLayer(defaultLayer);
            string background = string.Join("", BuildBorder(this.width, this.height)); // on crée les bord de l'écran
            Console.Write(background); //On affiche le bords
            Console.CursorVisible = false; //On cache le cursor
        }

        /// <summary>
        /// Permet d'afficher l'écran tous les layer de screen dans la console
        /// </summary>
        public void Display()
        {
            List<Element> elements = new List<Element>(); //Future liste des éléments

            foreach (Layer layer in layers.OrderBy(x => x.priority)) //On boucle sur tous les layers
            {
                DisplayLayer(layer);
            }
        }

        /// <summary>
        /// Permet d'afficher un layer dans la console
        /// </summary>
        /// <param name="layer"></param>
        public void DisplayLayer(Layer layer)
        {
            for (int i = 0; i < layer.elements.Count; i++) //On boucle sur tous les éléments du layer
            {
                DisplayElement(layer.elements[i]); //On affiche l'élément
                if (layer.elements[i].temp) //Si l'élément est témporaire on le supprime du layer
                {
                    layer.Add(layer.elements[i].GetEmptyClone());
                    i--;
                }
            }
        }

        /// <summary>
        /// Permet d'afficher un élément dans la console
        /// </summary>
        /// <param name="element"></param>
        public void DisplayElement(Element element)
        {
            for (int i = 0; i < element.text.Length; i++) //On bocle sur les lignes du layer
            {
                SetCursorElement(element, i); //On positionne le cursor au bon endroit
                        
                //On met les bonne couleurs
                Console.ForegroundColor = element.foreground;
                Console.BackgroundColor = element.background;

                if (element.animation == Animation.Typing) //On effectue le Typing
                {
                    Typing(element, i);
                }
                else Console.Write(element.text[i]); //Sinon on l'affiche
                        
                //Reset des couleurs
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.SetCursorPosition(0, 0); //On reset la position du cursor
        }
        
        /// <summary>
        /// Permet d'afficher un élément lettre par lettre
        /// </summary>
        /// <param name="element">L'élément à afficher</param>
        /// <param name="index">L'index de la ligne de l'élément</param>
        private void Typing(Element element, int index)
        {
            for (int i = 0; i < element.text[index].Length; i++) //On boucle sur tous les charactères
            {
                Console.Write(element.text[index][i]); //On affiche le charactère
                Task.Delay(5).Wait(); //Puis on attends
            }
        }

        /// <summary>
        /// Permet d'ajouter un layer
        /// </summary>
        /// <param name="layer">Le layer à ajouter</param>
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

        /// <summary>
        /// Permet de supprimer un layer
        /// </summary>
        /// <param name="layer">Le layer à supprimer</param>
        public void DeleteLayer(Layer layer)
        {
            HideLayer(layer); //On le cache d'abord
            this.layers.Remove(layer); //Puis on le supprime
        }

        /// <summary>
        /// Permet de supprimer un élément sans spécifier de layer
        /// </summary>
        /// <param name="element">L'élément à supprimer</param>
        public void DeleteElement(Element element)
        {
            foreach (Layer layer in layers) //On boucle sur tous les layers
            {
                foreach (Element _element in layer.elements) //Puis sur tous les élément du layer
                {
                    if (element.CompareTo(_element)) HideElement(element); //Si l'élément correspond on le cache
                    layer.Delete(element); //Puis on le supprime
                    return;
                }
            }
        }

        /// <summary>
        /// Permet de cacher un layer entier
        /// </summary>
        /// <param name="layer">La layer à cacher</param>
        public void HideLayer(Layer layer)
        {
            foreach (Element element in layer.elements) //On boucle sur tous les éléments du layer
            {
                HideElement(element); //On l'élément
            }
        }
        
        /// <summary>
        /// Permet de cacher un élément
        /// </summary>
        /// <param name="element">l'élément à cacher</param>
        public void HideElement(Element element)
        {
            DisplayElement(element.GetEmptyClone()); //On affiche une copie vide de l'élément
        }

        /// <summary>
        /// Permet de positionner le cursor au bon endroit pour afficher la ligne d'un élément
        /// </summary>
        /// <param name="element">L'élément pour lequel on veut placer le cursor</param>
        /// <param name="index">L'index de la ligne pour lequel on veut placer le cursor</param>
        private void SetCursorElement(Element element, int index)
        {
            if (element.placement == Placement.topLeft)
                Console.SetCursorPosition(element.coordinates.x, element.coordinates.y + index);

            else if (element.placement == Placement.mid)
                Console.SetCursorPosition(element.coordinates.x - (element.text[index].Length/2), (element.coordinates.y - element.text.Length/2) + index);

            else if (element.placement == Placement.topRight)
                Console.SetCursorPosition(element.coordinates.x - element.text[index].Length, element.coordinates.y + index);
                        
            else if (element.placement == Placement.botLeft)
                Console.SetCursorPosition(element.coordinates.x, (element.coordinates.y+index) - element.text[index].Length);

            else if (element.placement == Placement.botLeft)
                Console.SetCursorPosition(element.coordinates.x - element.text[index].Length, (element.coordinates.y+index) - element.text[index].Length);
        }
        
        /// <summary>
        /// Permet de clear l'écran
        /// </summary>
        public void Clear() {
            this.layers.Clear();
            Console.Clear();
            Initialize();
        }

        /// <summary>
        /// Permet de creer les bords de l'écran
        /// </summary>
        public static List<string> BuildBorder(int width, int height) {
            string top = "+" + new String('-', width-2) + "+\n";
            string mid = "|" + new String(' ', width-2) + "|\n";
            List<string> lines = Enumerable.Repeat(mid, height-2).ToList();
            lines.Insert(0, top);
            lines.Add(top);
            return lines;
        }

        /// <summary>
        /// Permet de faire un choix entre plusieurs strings
        /// </summary>
        /// <param name="choixArray">La liste des différents choix</param>
        /// <param name="height">La ligne à laquel afficher les choix</param>
        /// <param name="layer">Le layer des futurs éléments</param>
        /// <returns>L'index du choix final</returns>
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

        /// <summary>
        /// Permet de faire un choix entre plusieurs éléments
        /// </summary>
        /// <param name="elementArray">La liste des éléments entre lesquels faire le choix</param>
        /// <param name="startPosition">La position à laquelle commence le choix</param>
        /// <returns>L'index du choix final</returns>
        public int Select(Element[] elementArray, int startPosition)
        {
            int choice = startPosition; //On initialise la choix à la position de départ

            while(true)
            {
                int newChoice = 0; //On initialise la valeur du nouveau choix

                ConsoleKey key = Console.ReadKey().Key; //On lit les touches de l'utilisateur
                if (key == ConsoleKey.RightArrow || key == ConsoleKey.DownArrow) {
                    newChoice = choice + 1; //On incrémente le choix
                } else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.UpArrow) {
                    newChoice = choice - 1; //On décrémente le choix
                } else if (key == ConsoleKey.Enter) {
                    break; //On fini la séléction
                }

                //On module la variable choix
                if (newChoice < 0) newChoice = elementArray.Length-1;
                if (newChoice >= elementArray.Length) newChoice = 0;

                //On modifie les couleurs de l'élément séléctionné
                elementArray[newChoice].foreground = ConsoleColor.Black;
                elementArray[newChoice].background = ConsoleColor.White;
                DisplayElement(elementArray[newChoice]);
                
                //On modifie les couleurs de l'élément séléctionné avant
                elementArray[choice].foreground = ConsoleColor.White;
                elementArray[choice].background = ConsoleColor.Black;
                DisplayElement(elementArray[choice]);

                choice = newChoice;
            }
            return choice;
        }

        /// <summary>
        /// Surchage de la fonction Select sans prendre en compte la position de départ
        /// </summary>
        /// <param name="elementArray">La liste des éléments entre lesquels faire le choix</param>
        /// <returns>L'index du choix final</returns>
        public int Select(Element[] elementArray)
        {
            return Select(elementArray, 0);
        }
    }
}