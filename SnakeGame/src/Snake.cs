using System;
using System.Collections.Generic;

namespace SnakeGame.src
{
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    public class Snake
    {
        public Snake(int headX, int HeadY, int len = 5)
        {
            Direction = Direction.Right;
            Length = len;
            Head = new Pixel(headX, HeadY, ConsoleColor.Red);
            Body = new List<Pixel>();
        }

        public int Length { get; set; }
        public Direction Direction { get; set; }
        public Pixel Head { get; }
        public List<Pixel> Body { get; }

        public void Move()
        {
            Body.Add(new Pixel(Head.XPos, Head.YPos, ConsoleColor.DarkYellow));
            switch (Direction)
            {
                case Direction.Left:
                    Head.XPos--;
                    break;
                case Direction.Right:
                    Head.XPos++;
                    break;
                case Direction.Up:
                    Head.YPos--;
                    break;
                case Direction.Down:
                    Head.YPos++;
                    break;
                default:
                    break;
            }
            if (Body.Count > Length)
            {
                Body.RemoveAt(0);
            }
        }
    }


}
