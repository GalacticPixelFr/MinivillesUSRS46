using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.IO;  
using System.Diagnostics;  
using System.Runtime.InteropServices; 

namespace MinivillesURSR46
{
    internal class Program
    {
        [DllImport("kernel32.dll", ExactSpelling = true)]  
        private static extern IntPtr GetConsoleWindow();  
        private static IntPtr ThisConsole = GetConsoleWindow();  
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]  
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);  
        private const int HIDE = 0;  
        private const int MAXIMIZE = 3;  
        private const int MINIMIZE = 6;  
        private const int RESTORE = 9;
        public static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight); 
            ShowWindow(ThisConsole, MAXIMIZE); 
            Game game = new Game(20);
            game.StartGame();
            
        }
    }
}