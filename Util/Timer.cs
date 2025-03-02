namespace Util;

struct Timer
{
    public float Time    { get; private set; }
    public float MaxTime { get; private set; }
    public bool Done     { get; private set; }

    public Timer(float maxTime)
    {
        MaxTime = maxTime;
    }

    public void Tick(float deltaTime)
    {
        Time += deltaTime;

        if (Time >= MaxTime)
        {
            Done = true;
        }
    }

    public void Reset()
    {
        Time = 0f;
        Done = false;
    }
}