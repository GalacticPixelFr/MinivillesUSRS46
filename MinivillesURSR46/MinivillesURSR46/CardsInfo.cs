namespace MinivillesURSR46
{
    public class CardsInfo
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
        public string Effect { get; set; }
        public int Dice { get; set; }
        public int Gain { get; set; }

        public CardsInfo(int id, string color, int cost, string name, string effect, int dice, int gain)
        {
            Id = id;
            Color = color;
            Cost = cost;
            Name = name;
            Effect = effect;
            Dice = dice;
            Gain = gain;
        }
    }
}