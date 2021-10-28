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

        public void WriteBorders()
        {
            List<string> lines = Screen.BuildBorder(this.width, this.height);
            background.Add(new Element(lines.ToArray(), new Coordinates(this.coordinates.x, this.coordinates.y-height),
                            Animation.None, Placement.topLeft, ConsoleColor.White, ConsoleColor.Black));
            screen.DisplayLayer(background);
        }

        public void AddText(string text)
        {
            List<string> lines = new List<string>();
            Stack<string> stack = new Stack<string>();
            stack.Push(text);
            while (stack.Count > 0)
            {
                string currentString = stack.Pop();
                //Console.Write(currentString.Take(width).ToString().Length);

                if (currentString.Length > width-2)
                {
                    lines.Add(currentString.Substring(0, width-2));
                    
                    stack.Push(currentString.Substring(width-2));
                }
                else lines.Add(currentString);
            }

            textStack.Push(lines.ToArray());

            Display();
        }

        public void Display()
        {
            textLayer.Clear();
            
            Stack<string[]> stack = new Stack<string[]>(textStack.Reverse());
            int height = 0;
            while (stack.Count > 0)
            {
                string[] currentString = stack.Pop();
                height += currentString.Length;
                if (height > this.height) return;
                
                Element element = new Element(currentString, 
                                              new Coordinates(coordinates.x+1, coordinates.y - height-1),
                                              Animation.None, Placement.topLeft,
                                        ConsoleColor.White, ConsoleColor.Black);

                textLayer.Add(element);
            }

            screen.DisplayLayer(textLayer);
        }
        
    }
}