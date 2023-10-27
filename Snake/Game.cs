class Game
{
    Vector3d pos = new Vector3d(1, 10, 0);
    List<Vector3d> snakePositions = new List<Vector3d>();
    Vector3d snakeTail;

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
        if (Console.KeyAvailable)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.A)
                {
                    pos.X--;
                }
                if (keyInfo.Key == ConsoleKey.D)
                {
                    pos.X++;
                }
                if (keyInfo.Key == ConsoleKey.S)
                {
                    pos.Y++;
                }
                if (keyInfo.Key == ConsoleKey.W)
                {
                    pos.Y--;
                }
                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    snakePositions.Add(new Vector3d(pos.X, pos.Y, pos.Z));
                    pos.X++;
                }
            }

            // Add the current snake position to the end of the list
            snakePositions.Add(new Vector3d(pos.X, pos.Y, pos.Z));

            // Save the first position in the list to SnakeTail
            snakeTail = snakePositions[0];

            // Remove the first position in the list
            snakePositions.RemoveAt(0);
        }
    }
    void Move()
    {

    }
    void AddLenth()
    {

    }
    void Render()
    {
        if (snakeTail != null) { RemoveEndOfTail(); }
        WriteHead();
    }
    void RemoveEndOfTail()
    {
        Console.SetCursorPosition((int)snakeTail.X, (int)snakeTail.Y);
        Console.ForegroundColor = ConsoleColor.Black;
        Write(snakeTail.X, snakeTail.Y);
    }
    void WriteHead()
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