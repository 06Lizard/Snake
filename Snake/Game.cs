using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Game
{
    Snake head = new Snake(1, 10);
    List<Snake> snakePositions = new List<Snake>();
    short moveDirection = 0;
    Apple apple = new Apple(10, 10);

    public void Run()
    {
        Init();
        while (true)
        {
            Update();
            Render();
            Thread.Sleep(200);
        }
    }

    void Init()
    {
        Console.SetWindowSize(37, 27);
        Console.CursorVisible = false;
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;
        snakePositions.Add(new Snake(head.X, head.Y));
        apple.X = 10;
        apple.Y = 10;
    }

    void Update()
    {
        PlayerInput();
        Move();
        CheckCollisions();
    }

    void PlayerInput()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.A:
                    if (moveDirection != 0)
                        moveDirection = 1;
                    break;
                case ConsoleKey.D:
                    if (moveDirection != 1)
                        moveDirection = 0;
                    break;
                case ConsoleKey.S:
                    if (moveDirection != 3)
                        moveDirection = 2;
                    break;
                case ConsoleKey.W:
                    if (moveDirection != 2)
                        moveDirection = 3;
                    break;
            }
        }
    }

    void Move()
    {
        switch (moveDirection)
        {
            case 0: head.X++; break;
            case 1: head.X--; break;
            case 2: head.Y++; break;
            case 3: head.Y--; break;
        }

        for (int i = snakePositions.Count - 1; i > 0; i--)
        {
            snakePositions[i] = snakePositions[i - 1];
        }

        snakePositions[0] = new Snake(head.X, head.Y);
    }

    void CheckCollisions()
    {
        Boarder_Collision();
        Body_Collision();
        EatApple_Collision();
    }

    void Boarder_Collision()
    {
        if (head.X < 0 || head.X >= Console.WindowWidth || head.Y < 0 || head.Y >= Console.WindowHeight)
        {
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }

    void Body_Collision()
    {
        for (int i = 1; i < snakePositions.Count; i++)
        {
            if (head.X == snakePositions[i].X && head.Y == snakePositions[i].Y)
            {
                Console.Clear();
                Console.WriteLine("Game Over");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }

    void EatApple_Collision()
    {
        if (head.X == apple.X && head.Y == apple.Y)
        {
            snakePositions.Add(new Snake(head.X, head.Y));

            Random randomX = new Random();
            Random randomY = new Random();

            do
            {
                apple.X = randomX.Next(37);
                apple.Y = randomY.Next(27);
            } while (snakePositions.Any(s => s.X == apple.X && s.Y == apple.Y));
        }
    }

    void Render()
    {
        Console.Clear();
        WriteHead();
        WriteBody();
        WriteApple();
    }

    void WriteHead()
    {
        Console.SetCursorPosition(head.X, head.Y);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine('#');
        Console.ResetColor();
    }

    void WriteBody()
    {
        foreach (var segment in snakePositions.Skip(1))
        {
            Console.SetCursorPosition(segment.X, segment.Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine('#');
            Console.ResetColor();
        }
    }

    void WriteApple()
    {
        Console.SetCursorPosition(apple.X, apple.Y);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine('#');
        Console.ResetColor();
    }
}
