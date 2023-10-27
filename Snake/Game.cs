class Game
{
    Vector3d pos = new Vector3d(1, 10, 0);
    List<Vector3d> snakePositions = new List<Vector3d>();
    short moveDirection = 0;
    Apple apple = new (10, 10);


    public void Run()
    {
        Init();
        while (true)
        {
            Update();
            Render();
            Thread.Sleep(50); // Add a small delay to give the console time to update
        }
    }

    void Init()
    {
        Console.SetWindowSize(37, 27); // set the conosles display to 37*27 size
        Console.CursorVisible = false;
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;
        snakePositions.Add(pos);
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
                case ConsoleKey.Spacebar:
                    // Add a new segment to the snake's body at the current head position
                    snakePositions.Add(new Vector3d(pos.X, pos.Y, pos.Z));
                    break;
            }
        }
    }

    void Move()
    {
        // Move the head based on the current direction
        switch (moveDirection)
        {
            case 0: pos.X++; break;
            case 1: pos.X--; break;
            case 2: pos.Y++; break;
            case 3: pos.Y--; break;
        }

        // Update the positions of the snake's body segments
        for (int i = snakePositions.Count - 1; i > 0; i--)
        {
            snakePositions[i] = snakePositions[i - 1];
        }

        // Set the first segment to the current head position
        snakePositions[0] = new Vector3d(pos.X, pos.Y, pos.Z);
    }

    void CheckCollisions()
    {
        Boarder_Collision();
        Body_Collision();
        EatApple_Collision();
    }
    void Boarder_Collision()
    {
        if (pos.X < 0 || pos.X >= Console.WindowWidth || pos.Y < 0 || pos.Y >= Console.WindowHeight)
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
            if (pos.X == snakePositions[i].X && pos.Y == snakePositions[i].Y)
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
        if (pos.X == apple.X && pos.Y == apple.Y)
        {
            snakePositions.Add(new Vector3d(pos.X, pos.Y, pos.Z)); // adds to the length of the snake

            // Change apple position to a random position within the console (37 * 27)
            // that isn't on top of the snake's body or head

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
        Console.SetCursorPosition((int)pos.X, (int)pos.Y);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine('#');
        Console.ResetColor();
    }

    void WriteBody()
    {
        foreach (var segment in snakePositions.Skip(1))
        {
            Console.SetCursorPosition((int)segment.X, (int)segment.Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine('#');
            Console.ResetColor();
        }
    }

    void WriteApple()
    {
        Console.SetCursorPosition((int)apple.X, (int)apple.Y);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine('#');
        Console.ResetColor();
    }
}