class Game
{
    Snake head = new Snake(10, 10);
    List<Snake> snakePositions = new List<Snake>();
    short moveDirection = 0;
    Apple apple = new Apple(20, 10);
    private int Score = 0;
    short BoarderMinX = 1;
    short BoarderMinY = 2;
    short BoarderSizeX = 37;
    short BoarderSizeY = 27;
    short MiliSeconds = 0;
    int Seconds = 0;

    public void Run()
    {
        Init();
        while (true)
        {
            Update();
            Render();
            Thread.Sleep(200);
            MiliSeconds += 200;
            Seconds += MiliSeconds/1000;
            MiliSeconds %= 1000;
        }
    }

    void Init()
    {
        //Console.SetWindowSize(37, 27);
        Console.CursorVisible = false;
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;
        snakePositions.Add(new Snake(head.X, head.Y));
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
        if (head.X < BoarderMinX || head.X >= (BoarderMinX+BoarderSizeX) || head.Y < BoarderMinY || head.Y >= (BoarderMinY+BoarderSizeY))
        {
            EndGame();
        }
    }

    void Body_Collision()
    {
        for (int i = 1; i < snakePositions.Count; i++)
        {
            if (head.X == snakePositions[i].X && head.Y == snakePositions[i].Y)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        Console.Clear();
        Console.WriteLine($"Game Over\nScore: {Score}\nTime: {Seconds}:{MiliSeconds}");
        Console.ReadKey();
        Environment.Exit(0);
    }

    void EatApple_Collision()
    {
        if (head.X == apple.X && head.Y == apple.Y)
        {
            snakePositions.Add(new Snake(head.X, head.Y));

            Score++;

            Random randomX = new Random();
            Random randomY = new Random();

            do
            {
                apple.X = BoarderMinX + randomX.Next(BoarderSizeX);
                apple.Y = BoarderMinY + randomY.Next(BoarderSizeY);
            } while (snakePositions.Any(s => s.X == apple.X && s.Y == apple.Y));
        }
    }

    void Render()
    {
        Console.Clear();
        WriteScoreAndTime();
        WriteBorder();
        WriteHead();
        WriteBody();
        WriteApple();
    }

    void WriteScoreAndTime()
    {
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Score: {Score}\tTime: {Seconds}:{MiliSeconds}");
    }

    void WriteBorder()
    {
        Console.ForegroundColor = ConsoleColor.Blue;

        // Draw horizontal borders
        for (int x = BoarderMinX - 1; x <= BoarderMinX + BoarderSizeX; x++)
        {
            Console.SetCursorPosition(x, BoarderMinY - 1);
            Console.Write('#');
            Console.SetCursorPosition(x, BoarderMinY + BoarderSizeY);
            Console.Write('#');
        }

        // Draw vertical borders
        for (int y = BoarderMinY; y <= BoarderMinY + BoarderSizeY - 1; y++)
        {
            Console.SetCursorPosition(BoarderMinX - 1, y);
            Console.Write('#');
            Console.SetCursorPosition(BoarderMinX + BoarderSizeX, y);
            Console.Write('#');
        }
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
