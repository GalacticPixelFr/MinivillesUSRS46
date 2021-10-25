using System;
using System.Collections.Generic;
using System.Linq;

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

    public void Display()
    {
        Console.Clear();
        string background = string.Join("", BuildBorder()); // on crée les bord de l'écran
        Console.Write(background);
        foreach (List<Element> layer in layers.Values)
        {
            foreach (Element element in layer)
            {
                foreach (string text in element.text)
                {
                    if (element.placement == Placement.topLeft)
                        Console.SetCursorPosition(element.coordinates.x, element.coordinates.y);

                    else if (element.placement == Placement.mid)
                        Console.SetCursorPosition(element.coordinates.x - (text.Length/2), element.coordinates.y);

                    else if (element.placement == Placement.topRight)
                        Console.SetCursorPosition(element.coordinates.x - text.Length, element.coordinates.y);
                    
                    else if (element.placement == Placement.botLeft)
                        Console.SetCursorPosition(element.coordinates.x, element.coordinates.y - element.text.Length);

                    else if (element.placement == Placement.botLeft)
                        Console.SetCursorPosition(element.coordinates.x - text.Length, element.coordinates.y - element.text.Length);

                    Console.ForegroundColor = element.foreground;
                    Console.BackgroundColor = element.background;
                    Console.Write(text);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        Console.SetCursorPosition(0, 0);
    }

    public void Add(Element element) {
        this.Add(element, 0);
    }

    public void Add(Element element, int layer) {
       if (!layers.ContainsKey(layer))
            layers.Add(layer, new List<Element>());
        layers[layer].Add(element);
    }

    /* TODO
    public void Delete(Coordinates coordinates) {
        this.Delete(coordinates, 0);
    }

    public void Delete(Coordinates coordinates, int layer) {
        if(this.layers[layer].ContainsKey(coordinates))
            this.layers[layer].Remove(coordinates);
    }*/

    public void DeleteLayer(int layer) {
        if (this.layers.ContainsKey(layer)) {
            this.layers.Remove(layer);
        }
    }

    public void Clear() {
        this.layers.Clear();
    }

    private List<string> BuildBorder() {
        string top = "+" + new String('-', this.width-2) + "+\n";
        string mid = "|" + new String(' ', this.width-2) + "|\n";
        List<string> lines = Enumerable.Repeat(mid, this.height-2).ToList();
        lines.Insert(0, top);
        lines.Add(top);
        return lines;
    }
}
