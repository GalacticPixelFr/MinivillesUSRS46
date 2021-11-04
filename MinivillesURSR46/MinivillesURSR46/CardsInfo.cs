using System;

namespace MinivillesURSR46
{
    public class CardsInfo
    {
        public int Id { get; set; }
        public Color Color { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
        public string NameURSS { get; set; }
        public string Effect { get; set; }
        public int Dice { get; set; }
        public int Gain { get; set; }

        public CardsInfo(int id, Color color, int cost, string name, string nameUrss, string effect, int dice, int gain)
        {
            Id = id;
            Color = color;
            Cost = cost;
            Name = name;
            NameURSS = nameUrss;
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
        public Element[] ToElementFull(Coordinates coordinates, bool urss)
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
                    color = ConsoleColor.DarkGreen;
                    break;
                case Color.Jaune:
                    color = ConsoleColor.DarkYellow;
                    break;
            }
            Element background = new Element(stringBackground, coordinates, Animation.None, Placement.mid, color, ConsoleColor.Black);

            string[] stringInfos = new string[5]{
                urss ? this.NameURSS : this.Name,
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
        public Element[] ToElementSemi(bool top, Coordinates coordinates)
        {
            string[] stringBackground = new string[]{
                "|         |",
                "|         |",
                "|         |",
                "|         |",
                "+---------+"
            };

            if (top) 
            {
                stringBackground = new string[]{
                    "+---------+",
                    "|         |",
                    "|         |",
                    "|         |",
                    "|         |"
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
                    color = ConsoleColor.DarkGreen;
                    break;
                case Color.Jaune:
                    color = ConsoleColor.DarkYellow;
                    break;
            }
            Element background = new Element(stringBackground, new Coordinates(coordinates.x, coordinates.y + (top ? -1 : 0)), Animation.None, Placement.mid, color, ConsoleColor.Black);

            string[] name = Chat.ReturnToLine(this.Name, 11);
            
            string[] stringInfos = new string[]{
                name[0],
                name.Length >= 2 ? name[1] : "",
                this.Dice.ToString(),
                "+"+this.Gain
            };
            Element infos = new Element(stringInfos, coordinates, Animation.None, Placement.mid, ConsoleColor.White, ConsoleColor.Black);

            return new Element[]{background, infos};
        }
        
    }
    public enum Color
    {
        Bleu,
        Vert,
        Rouge,
        Jaune
    }
}