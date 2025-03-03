using System.Numerics;
using Raylib_cs;
using Simulation;

namespace UI;

class Application
{
    private SnakeEngine m_engine;
    private SimManager m_simManager;

    private int m_currentIndex;
    private Recording m_recording;

    private int m_tileSize = 32;

    public Application()
    {
        m_simManager = new SimManager(100000);
    }

    public void Run()
    {
        m_simManager.Run();

        int highestScore = -1;
        int longest = -1;

        foreach (Recording rec in m_simManager.RecordedSims)
        {
            if (rec.Score > highestScore && (rec.Score == highestScore || rec.Actions.Count > longest))
            {
                highestScore = rec.Score;
                longest = rec.Actions.Count;
                m_recording  = rec;
            }
        }

        m_engine = new SnakeEngine(40, 23, m_recording.Seed);

        Raylib.InitWindow(1280, 720, "WINDOW");

        bool playing = false;
        Util.Timer moveTimer = new Util.Timer(0.0625f);

        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                break;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Space) && !playing)
            {
                playing = true;
            }

            if (playing)
            {
                moveTimer.Tick(Raylib.GetFrameTime());

                if (moveTimer.Done)
                {
                    m_engine.Step(m_recording.Actions[m_currentIndex++]);
                    moveTimer.Reset();

                    if (m_currentIndex == m_recording.Actions.Count)
                    {
                        playing = false;
                    }
                }
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            
            Render();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private void Render()
    {
        // draw food
        Raylib.DrawRectangleV(m_engine.Food * m_tileSize, new Vector2(m_tileSize), Color.Red);

        // draw snake
        foreach (Vector2 part in m_engine.SnakeParts)
        {
            Raylib.DrawRectangleV(part * m_tileSize, new Vector2(m_tileSize), Color.Green);
        }

        // debug
        Raylib.DrawText($"Score: {m_engine.Score} Action: {m_currentIndex} of {m_recording.Actions.Count}", 32, 32, 20, Color.Blue);
    }
}