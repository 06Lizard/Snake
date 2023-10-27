class Game
{
    Vector3d pos = new Vector3d(1, 10, 0);
    List<Vector3d> snakePositions = new List<Vector3d>();
    Vector3d snakeTail;
    short moveDirection = 0;
    int time = 0;

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
        Console.CursorVisible = false;
        Console.BufferWidth = Console.WindowWidth;
        Console.BufferHeight = Console.WindowHeight;
        snakePositions.Add(pos);
    }


    void Update()
    {
        PlayerInput();

        // Only move the snake every 250 instances of the update function
        //if (time % 250 == 0)
        {
            Move();
            time = 0; // Reset the time counter
        }

        UpdateLength();
        CheckCollision();
    }

    void PlayerInput() // Check for player input
    {
        if (Console.KeyAvailable)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.A && moveDirection != 0)
                {
                    moveDirection = 1;
                }
                if (keyInfo.Key == ConsoleKey.D && moveDirection != 1)
                {
                    moveDirection = 0;
                }
                if (keyInfo.Key == ConsoleKey.S && moveDirection != 3)
                {
                    moveDirection = 2;
                }
                if (keyInfo.Key == ConsoleKey.W && moveDirection != 2)
                {
                    moveDirection = 3;
                }
                if (keyInfo.Key == ConsoleKey.Spacebar) // Temporary placeholder for eating an apple
                {
                    // Add the current snake position to the end of the list to make the snake longer 
                    snakePositions.Add(new Vector3d(pos.X, pos.Y, pos.Z));
                }
            }
        }
    }
    void Move() // Moves forward
    {
        switch (moveDirection)
        {
            case 0: pos.X++; break;
            case 1: pos.X--; break;
            case 2: pos.Y++; break;
            case 3: pos.Y--; break;
        }
    }
    void UpdateLength()
    {
        // Save the first position in the list to SnakeTail
        snakeTail = snakePositions[0];

        // Remove the first position in the list (this is the last part of the tail which needs to be removed)
        snakePositions.RemoveAt(0);

        // Add the current snake position to the end of the list
        snakePositions.Add(new Vector3d(pos.X, pos.Y, pos.Z));
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
                Environment.Exit(0); // Terminate the application
            }
        }
    }

    void Render() 
    {
        if (snakeTail != null) { RemoveEndOfTail(); }
        WriteHead();
    }
    void RemoveEndOfTail()
    {
        if (snakeTail.X >= 0 && snakeTail.X < Console.WindowWidth && snakeTail.Y >= 0 && snakeTail.Y < Console.WindowHeight)
        {
            Console.SetCursorPosition((int)snakeTail.X, (int)snakeTail.Y);
            Console.ForegroundColor = ConsoleColor.Black;
            Write(snakeTail.X, snakeTail.Y);
        }
    }

    void WriteHead() // Writes the head
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Write(pos.X, pos.Y);
        Console.ResetColor(); // Reset console color after writing
    }

    void Write(double X, double Y)
    {
        if (X >= 0 && X < Console.WindowWidth && Y >= 0 && Y < Console.WindowHeight)
        {
            Console.SetCursorPosition((int)X, (int)Y);
            Console.WriteLine('#');
            Console.ResetColor(); // Reset console color after writing
        }
    }

}