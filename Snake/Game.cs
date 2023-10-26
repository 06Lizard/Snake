class Game
{
    Vector3d pos = new(1, 10, 0);
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
            }
        }
    }
    void Render()
    {
        Console.SetCursorPosition((int)pos.X, (int)pos.Y);
        Console.WriteLine('#');
    }
}