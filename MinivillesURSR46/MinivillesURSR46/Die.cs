using System;
using System.Collections.Generic;
using System.Linq;

namespace MinivillesURSR46
{
    public class Die
    {
        static private Random random = new Random();

        public Die()
        {
            
        }
        
        public int Lancer()
        {
            return random.Next(0, 6) + 1;
        }

        public static string[] ToStrings(int number)
        {
            /* +---------+
             * |       * |
             * |    *    |
             * | *       |
             * +---------+
             */

            string top = "+" + new String('-', 9) + "+";
            string mid = "|" + new String(' ', 9) + "|";
            List<string> lines = Enumerable.Repeat(mid, 3).ToList();
            lines.Insert(0, top);
            lines.Add(top);

            if (number == 1)
            {
                lines[2] = "|    *    |";
            }
            else if (number == 2)
            {
                lines[1] = "|       * |";
                lines[3] = "| *       |";
            }
            else if (number == 3)
            {
                lines[1] = "|       * |";
                lines[2] = "|    *    |";
                lines[3] = "| *       |";
            }
            else if (number == 4)
            {
                lines[1] = "| *     * |";
                lines[3] = "| *     * |";
            }
            else if (number == 5)
            {
                lines[1] = "| *     * |";
                lines[2] = "|    *    |";
                lines[3] = "| *     * |";
            }
            else if (number == 6)
            {
                lines[1] = "| *     * |";
                lines[2] = "| *     * |";
                lines[3] = "| *     * |";
            }
            //return new string[] { lines[2] };

            return lines.ToArray();
        }
    }
}