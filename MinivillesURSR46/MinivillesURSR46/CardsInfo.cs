using System;

namespace MinivillesURSR46
{
    public class CardsInfo
    {
        public int Id { get; set; }
        public Color Color { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
        public string Effect { get; set; }
        public int Dice { get; set; }
        public int Gain { get; set; }

        public CardsInfo(int id, Color color, int cost, string name, string effect, int dice, int gain)
        {
            Id = id;
            Color = color;
            Cost = cost;
            Name = name;
            Effect = effect;
            Dice = dice;
            Gain = gain;
        }


        /// <summary>
        /// Permet de renvoyer une liste d'element pour afficher une carte
        /// </summary>
        /// <param name="cardsInfo">Les infos de la carte</param>
        /// <param name="coordinates">les coordonnées de la carte</param>
        /// <returns>liste d'element pour afficher une carte</returns>
        public Element[] ToElementFull(Coordinates coordinates)
        {
            string[] stringBackground = new string[9]{
                "+----------------+",
                "|                |",
                "|                |",
                "|                |",
                "|                |",
                "|                |",
                "|                |",
                "|                |",
                "+----------------+"
            };
            
            ConsoleColor color = ConsoleColor.White;
            switch (this.Color)
            {
                case Color.Bleu:
                    color = ConsoleColor.Blue;
                    break;
                case Color.Rouge:
                    color = ConsoleColor.Red;
                    break;
                case Color.Vert:

                    color = ConsoleColor.Green;
                    break;
            }
            Element background = new Element(stringBackground, coordinates, Animation.None, Placement.mid, color, ConsoleColor.Black);

            string[] stringInfos = new string[5]{
                this.Name,
                "",
                this.Cost+"  pièces",
                "",
                this.Dice.ToString()
            };
            Element infos = new Element(stringInfos, new Coordinates(coordinates.x, coordinates.y), Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);

            return new Element[2]{background, infos};
        }


        /// <summary>
        /// Permet d'afficher la moitier d'un carte (pour afficher celle du joueur)
        /// </summary>
        /// <param name="amount">Le nombre de cartes</param>
        /// <param name="cardsInfo">Les infos de la carte</param>
        /// <param name="coordinates">les coordonnées de la carte</param>
        /// <returns></returns>
        public Element[] ToElementSemi(bool top, int amount, Coordinates coordinates)
        {
            string[] stringBackground = new string[4]{
                "|                |",
                "|                |",
                "|                |",
                "+----------------+"
            };

            if (top) 
            {
                stringBackground = new string[4]{
                    "+----------------+",
                    "|                |",
                    "|                |",
                    "|                |"
                };
            }

            ConsoleColor color = ConsoleColor.White;
            switch (this.Color)
            {
                case Color.Bleu:
                    color = ConsoleColor.Blue;
                    break;
                case Color.Rouge:
                    color = ConsoleColor.Red;
                    break;
                case Color.Vert:
                    color = ConsoleColor.Green;
                    break;
            }
            Element background = new Element(stringBackground, coordinates, Animation.None, Placement.mid, color, ConsoleColor.Black);

            string[] stringInfos = new string[3]{
                this.Name,
                this.Dice.ToString(),
                "+"+amount
            };
            Element infos = new Element(stringInfos, new Coordinates(coordinates.x, coordinates.y + (top ? 0 : -1)), Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);

            return new Element[2]{background, infos};
        }
        
    }
    public enum Color
    {
        Bleu,
        Vert,
        Rouge
    }
}