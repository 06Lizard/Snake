class Game
{
    Vector3d pos = new Vector3d(1, 10, 0);
    List<Vector3d> snakePositions = new List<Vector3d>();
    short moveDirection = 0;

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
        // make the conosle 37 times 27 
        Console.CursorVisible = false;
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;
        snakePositions.Add(pos);
        // Spawn a apple at position 10, 10, 0
    }

    void Update()
    {
        PlayerInput();
        Move();
        CheckCollision();
        EatApple();
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

    void CheckCollision()
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
    void EatApple()
    {
        //if (snakePositions == ApplePosition)
        {
            snakePositions.Add(new Vector3d(pos.X, pos.Y, pos.Z));
            //Change apple position to a random position within the conole (37 * 27) that isn't on top of the snake's body or head
        }
    }
    void Render()
    {
        Console.Clear();
        WriteHead();
        WriteBody();
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

/*    void WriteApple()
    {
        Console.SetCursorPosition((int)apple.X, (int)apple.Y);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine('#');
        Console.ResetColor();
    }*/
}