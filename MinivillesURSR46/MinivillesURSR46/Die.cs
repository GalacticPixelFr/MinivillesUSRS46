using System;

namespace MinivillesURSR46
{
    public class Die
    {
        private Random random = new Random();
        public int Face { get; protected set; }

        public Die()
        {
            
        }
        
        public virtual int Lancer()
        {
            Face = random.Next(0, 6) + 1;
            return Face;
        }
    }
}