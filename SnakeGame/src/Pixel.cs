using System;

namespace SnakeGame.src
{
    public class Pixel
    {
        private readonly Random rand = new Random();

        public Pixel(ConsoleColor color)
        {
            XPos = rand.Next();
            YPos = rand.Next();
            Color = color;
        }
        public Pixel(int x, int y, ConsoleColor color)
        {
            XPos = x;
            YPos = y;
            Color = color;
        }

        public int XPos { get; set; }
        public int YPos { get; set; }
        public ConsoleColor Color { get; set; }

        public void Draw()
        {
            Console.SetCursorPosition(XPos, YPos);
            Console.ForegroundColor = Color;
            Console.Write("#");
        }
    }
}
