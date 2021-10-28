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

        public void Add(Element element)
        {
            if (elements.AsEnumerable().Any(x => x.coordinates == element.coordinates))
            {
                Delete(elements.AsEnumerable().FirstOrDefault(x => x.coordinates == element.coordinates));
            }
            this.elements.Add(element);
        }

        public void Delete(Element element)
        {
            this.elements.Remove(element);
        }

        public void Clear()
        {
            this.elements.Clear();
        }
    }
}