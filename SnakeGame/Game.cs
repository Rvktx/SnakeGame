using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using static System.Console;

namespace SnakeGame
{
    class Game
    {
        public Random rand = new Random();

        static void Main(string[] args)
        {
            Game snake = new Game();
            snake.Initialize();
        }
        void Initialize()
        {
            WindowWidth = 80;
            WindowHeight = 40;
            CursorVisible = false;

            var fruit = new Pixel(rand.Next(1, WindowWidth - 2), rand.Next(1, WindowHeight - 2), ConsoleColor.Green);
            var head = new Pixel(WindowWidth / 2, WindowHeight / 2, ConsoleColor.Red);
            var body = new List<Pixel>();

            var segments = 5;
            var score = 0;
            var snakeDirection = Direction.Right;
            var gameOver = false;

            DrawBorder();

            while (!gameOver)
            {
                Refresh();

                if (head.XPos == fruit.XPos && head.YPos == fruit.YPos)
                {
                    segments++;
                    score++;

                    fruit = new Pixel(rand.Next(1, WindowWidth - 2), rand.Next(1, WindowHeight - 2), ConsoleColor.Green);
                }

                DrawPixel(fruit);
                DrawPixel(head);
                for (int i = 0; i < body.Count; i++)
                {
                    DrawPixel(body[i]);

                    gameOver |= (head.XPos == body[i].XPos && head.YPos == body[i].YPos);
                }

                gameOver |= (head.XPos == 0 || head.XPos == WindowWidth - 1 || head.YPos == 0 || head.YPos == WindowHeight - 1);

                var timer = Stopwatch.StartNew();
                while (timer.ElapsedMilliseconds <= 100) snakeDirection = ReadMovement(snakeDirection);
                body.Add(new Pixel(head.XPos, head.YPos, ConsoleColor.DarkYellow));
                switch (snakeDirection)
                {
                    case Direction.Up:
                        head.YPos--;
                        break;
                    case Direction.Down:
                        head.YPos++;
                        break;
                    case Direction.Left:
                        head.XPos--;
                        break;
                    case Direction.Right:
                        head.XPos++;
                        break;
                }
                if (body.Count > segments) body.RemoveAt(0);
                Title = $"Score: {score}";
            }

            SetCursorPosition(WindowWidth / 5, WindowHeight / 2);
            WriteLine($"Game over, Score: {score}");
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2 + 1);
            ReadKey();
        }
        static void DrawBorder()
        {
            for (int i = 0; i < WindowWidth; i++)
            {
                SetCursorPosition(i, 0);
                Write("#");

                SetCursorPosition(i, WindowHeight - 1);
                Write("#");
            }

            for (int i = 0; i < WindowHeight; i++)
            {
                SetCursorPosition(0, i);
                Write("#");

                SetCursorPosition(WindowWidth - 1, i);
                Write("#");
            }
        }
        static void Refresh()
        {
            var blackLine = string.Join("", new byte[WindowWidth - 2].Select(b => " ").ToArray());
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 1; i < WindowHeight - 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(blackLine);
            }
        }
        static Direction ReadMovement(Direction movement)
        {
            if (KeyAvailable)
            {
                var key = ReadKey().Key;

                if (key == ConsoleKey.UpArrow && movement != Direction.Down) movement = Direction.Up;
                if (key == ConsoleKey.DownArrow && movement != Direction.Up) movement = Direction.Down;
                if (key == ConsoleKey.LeftArrow && movement != Direction.Right) movement = Direction.Left;
                if (key == ConsoleKey.RightArrow && movement != Direction.Left) movement = Direction.Right;
            }

            return movement;
        }
        static void DrawPixel(Pixel pixel)
        {
            SetCursorPosition(pixel.XPos, pixel.YPos);
            ForegroundColor = pixel.ScreenColor;
            Write("#");
            SetCursorPosition(0, 0);
        }
        struct Pixel
        {
            public Pixel(int xPos, int yPos, ConsoleColor color)
            {
                XPos = xPos;
                YPos = yPos;
                ScreenColor = color;
            }
            public int XPos { get; set; }
            public int YPos { get; set; }
            public ConsoleColor ScreenColor { get; set; }
        }
        enum Direction
        {
            Up,
            Down,
            Right,
            Left
        }
    }
}
