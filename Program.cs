using System.Numerics;
using Raylib_cs;
using Simulation;

/*
const int WINDOW_WIDTH  = 1280;
const int WINDOW_HEIGHT = 720;

const int TILE_SIZE = 32;

Status gameStatus    = Status.Running;
Simulation.Action action  = Simulation.Action.None;
Util.Timer moveTimer = new Util.Timer(0.0625f);
SnakeEngine engine   = new SnakeEngine(40, 23);

Raylib.InitWindow(WINDOW_WIDTH, WINDOW_HEIGHT, "SNAKE");

while (!Raylib.WindowShouldClose())
{
    if (gameStatus == Status.Running)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.D))
        {
            action = Simulation.Action.TurnRight;
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.A))
        {
            action = Simulation.Action.TurnLeft;
        }

        moveTimer.Tick(Raylib.GetFrameTime());

        if (moveTimer.Done)
        {   
            gameStatus = engine.Step(action);
            moveTimer.Reset();

            action = Simulation.Action.None;
        }
    }
    else
    {
        if (Raylib.IsKeyPressed(KeyboardKey.R))
        {
            gameStatus = Status.Running;
            engine     = new SnakeEngine(40, 23);
        }
    }

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);

    // draw food
    Raylib.DrawRectangleV(engine.Food * TILE_SIZE, new Vector2(TILE_SIZE, TILE_SIZE), Color.Red);

    // draw snake parts
    foreach (Vector2 part in engine.SnakeParts)
    {
        Raylib.DrawRectangleV(part * TILE_SIZE, new Vector2(TILE_SIZE, TILE_SIZE), Color.Green);
    }

    if (gameStatus == Status.GameOver)
    {
        Raylib.DrawText("DEAD", WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2, 20, Color.Red);
    }

    Raylib.EndDrawing();
}

Raylib.CloseWindow();
*/

await new SimManager(10).Run();