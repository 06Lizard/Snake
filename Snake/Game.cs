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
        }
    }
    void Init()
    {
        Console.CursorVisible = false;
        snakePositions.Add(pos);
    }
    void Update()
    {
        PlayerInput();
        if (time / 250 != 0) { Move(); }
        time = (time + 1) % 250;
        UpdateLenth();
        CheckColition();
    }
    void PlayerInput() // Check for player input
    {
        if (Console.KeyAvailable)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.A)
                {
                    moveDirection = 1;
                }
                if (keyInfo.Key == ConsoleKey.D)
                {
                    moveDirection = 0;
                }
                if (keyInfo.Key == ConsoleKey.S)
                {
                    moveDirection = 2;
                }
                if (keyInfo.Key == ConsoleKey.W)
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
            case 1: pos.Y--; break;
            case 2: pos.Z++; break;
            case 3: pos.Z--; break;
        }
    }
    void UpdateLenth() // Updates the position of the snakes head and removes the last part of the tail. 
    {
        // Add the current snake position to the end of the list 
        snakePositions.Add(new Vector3d(pos.X, pos.Y, pos.Z));

        // Save the first position in the list to SnakeTail (Saves the position of the last part of the tail so that we can remove the display position of it)
        snakeTail = snakePositions[0];

        // Remove the first position in the list (this is the last part of the tail witch needs to be removed)
        snakePositions.RemoveAt(0);
    }
    void CheckColition()
    {
        //if (snake heads position is the same as any of the snakes other positions in it's list) { Console.Clear(); cw("Game Over"); Console.ReadKey(); }
    }
    void Render() 
    {
        if (snakeTail != null) { RemoveEndOfTail(); }
        WriteHead();
    }
    void RemoveEndOfTail() // Removes the last part of the tail
    {
        Console.SetCursorPosition((int)snakeTail.X, (int)snakeTail.Y);
        Console.ForegroundColor = ConsoleColor.Black;
        Write(snakeTail.X, snakeTail.Y);
    }
    void WriteHead() // Writes the head
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Write(pos.X, pos.Y);
    }
    void Write(double X, double Y)
    {
        Console.SetCursorPosition((int)X, (int)Y);
        Console.WriteLine('#');
    }
}