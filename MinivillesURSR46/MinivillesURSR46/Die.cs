using System;

namespace MinivillesURSR46
{
    public class Die
    {
        static private Random random = new Random();

        public Die()
        {
            
        }
        
        public static int Lancer()
        {
            return random.Next(0, 6) + 1;
        }
    }
}