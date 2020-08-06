using System;
using System.Linq;
using System.Threading;

namespace SnakeGame.src
{
    public class Game
    {
        private const int WINDOW_WIDTH = 80;
        private const int WINDOW_HEIGHT = 40;

        private readonly Random rand = new Random();
        private readonly Snake snake;
        private Pixel fruit;

        private int score;
        private bool isOver;

        public Game()
        {
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);
            Console.CursorVisible = false;

            snake = new Snake(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2);
            fruit = GenerateFruit();

            score = 0;
            isOver = false;
        }

        public void Loop()
        {
            DrawBorder();
            while (!isOver)
            {
                ClearConsole();
                DrawFrame();

                if (snake.Head.XPos == fruit.XPos && snake.Head.YPos == fruit.YPos)
                {
                    fruit = GenerateFruit();
                    snake.Length++;
                    score++;
                    Console.Title = $"Score: {score}";
                }

                Thread.Sleep(100);
                ReadKey();

                snake.Move();
                isOver = CheckForCollisions();
            }
            DisplaySummary();
        }

        private Pixel GenerateFruit()
        {
            int x = rand.Next(1, WINDOW_WIDTH - 2);
            int y = rand.Next(1, WINDOW_HEIGHT - 2);

            return new Pixel(x, y, ConsoleColor.Green);
        }

        private void DrawBorder()
        {
            for (int i = 0; i < WINDOW_WIDTH; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
                Console.SetCursorPosition(i, WINDOW_HEIGHT - 1);
                Console.Write("#");

            }
            for (int i = 0; i < WINDOW_HEIGHT; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
                Console.SetCursorPosition(WINDOW_WIDTH - 1, i);
                Console.Write("#");
            }
        }

        private void ClearConsole()
        {
            string blackLine = string.Join("", new byte[WINDOW_WIDTH - 2].Select(b => " ").ToArray());
            for (int i = 1; i < WINDOW_HEIGHT - 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(blackLine);
            }
        }

        private void DrawFrame()
        {
            fruit.Draw();
            snake.Head.Draw();
            for (int i = 0; i < snake.Body.Count; i++)
            {
                snake.Body[i].Draw();
            }
        }

        private void ReadKey()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow && snake.Direction != Direction.Down)
                {
                    snake.Direction = Direction.Up;
                }
                else if (key == ConsoleKey.DownArrow && snake.Direction != Direction.Up)
                {
                    snake.Direction = Direction.Down;
                }
                else if (key == ConsoleKey.LeftArrow && snake.Direction != Direction.Right)
                {
                    snake.Direction = Direction.Left;
                }
                else if (key == ConsoleKey.RightArrow && snake.Direction != Direction.Left)
                {
                    snake.Direction = Direction.Right;
                }
            }
        }

        private bool CheckForCollisions()
        {
            if (snake.Head.XPos == 0 || snake.Head.XPos == WINDOW_WIDTH - 1
                || snake.Head.YPos == 0 || snake.Head.YPos == WINDOW_HEIGHT - 1)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < snake.Body.Count; i++)
                {
                    if (snake.Head.XPos == snake.Body[i].XPos
                        && snake.Head.YPos == snake.Body[i].YPos)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private void DisplaySummary()
        {
            Console.SetCursorPosition((WINDOW_WIDTH / 2) - 10, (WINDOW_HEIGHT / 2) - 1);
            Console.WriteLine($"Game over, Score: {score}");
            Console.SetCursorPosition((WINDOW_WIDTH / 2) - 11, (WINDOW_HEIGHT / 2) + 1);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        }
    }
}
