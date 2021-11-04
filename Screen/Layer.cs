using System.Collections.Generic;
using System.Linq;

namespace MinivillesURSR46
{
    public class Layer
    {
        public List<Element> elements = new List<Element>();
        public int priority { get; private set; }

        public Layer(List<Element> elements, int priority)
        {
            this.elements = elements;
            this.priority = priority;
        }

        public Layer(int priority)
        {
            this.priority = priority;
        }

        public Layer(Element element)
        {
            this.elements.Add(element);
        }

        /// <summary>
        /// Permet d'ajouter un Element au layer
        /// </summary>
        /// <param name="element">L'élément qu'on veut ajouter</param>
        public void Add(Element element)
        {
            if (elements.AsEnumerable().Any(x => x.coordinates == element.coordinates)) //Si un élément se trouve déjà à ces coordonées
            {
                Delete(elements.AsEnumerable().FirstOrDefault(x => x.coordinates == element.coordinates)); //On supprime l'élément au même coordonées
            }
            this.elements.Add(element);
        }

        /// <summary>
        /// Permet de supprimer un élément du layer
        /// </summary>
        /// <param name="element">L'élément à supprimer</param>
        public void Delete(Element element)
        {
            this.elements.Remove(element);
        }

        /// <summary>
        /// Permet de clear un Layer
        /// </summary>
        public void Clear()
        {
            this.elements.Clear();
        }
    }
}