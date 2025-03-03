using System.Numerics;

namespace Simulation;

enum Action
{
    None,
    TurnLeft,
    TurnRight,
}

enum Status
{
    Running,
    GameOver,
    GameWon,
}

class SnakeEngine
{
    private int m_width;
    private int m_height;
    private int m_score;

    private bool m_noWalls = false;

    private Vector2 m_direction = Vector2.UnitX;
    private Vector2 m_food      = Vector2.Zero;

    private Snake m_snake = new Snake();

    private Random m_random;

    public int Score
    {
        get
        {
            return m_score; 
        }
    }

    public Vector2 Food
    {
        get
        {
            return m_food;
        }

        set
        {
            m_food = value;
        }
    }

    public LinkedList<Vector2> SnakeParts
    {
        get
        {
            return m_snake.BodyParts;
        }
    }

    public int Seed = Guid.NewGuid().GetHashCode();

    public SnakeEngine(int width, int height)
    {
        m_width  = width;
        m_height = height;

        m_random = new Random(Seed);

        m_snake.BodyParts.AddFirst(new Vector2(m_width / 2, m_height / 2));

        SpawnFood();
    }

    public SnakeEngine(int width, int height, int seed)
    {
        m_width  = width;
        m_height = height;

        Seed     = seed;
        m_random = new Random(seed);

        m_snake.BodyParts.AddFirst(new Vector2(m_width / 2, m_height / 2));

        SpawnFood();
    }

    public Status Step(Action action)
    {
        m_direction     = ActionDirection(action);
        Vector2 nextPos = TunnelThroughWalls(m_snake.Head + m_direction);

        Vector2 tail = m_snake.Tail;
        m_snake.BodyParts.RemoveLast();

        if (HitsWall(nextPos) || m_snake.HitsBody(nextPos))
        {
            return Status.GameOver;
        }

        if (nextPos == m_food)
        {
            m_score++;
            m_snake.BodyParts.AddLast(tail);
            SpawnFood();
        }

        m_snake.BodyParts.AddFirst(nextPos);

        return Status.Running;      
    }

    private void SpawnFood() {
        do
        {
            m_food.X = m_random.Next(0, m_width);
            m_food.Y = m_random.Next(0, m_height);
        } while (m_snake.HitsBody(m_food));
    }

    private Vector2 ActionDirection(Action action)
    {
        Vector2 direction = m_direction;

        switch (action)
        {
            case Action.TurnLeft:
            {
                direction = new Vector2(m_direction.Y, -m_direction.X);
                break;
            }

            case Action.TurnRight:
            {
                direction = new Vector2(-m_direction.Y, m_direction.X);
                break;
            }
        }

        return direction;
    }

    private bool HitsWall(Vector2 nextPos)
    {
        return !m_noWalls && (nextPos.X < 0 || nextPos.X >= m_width || nextPos.Y < 0 || nextPos.Y >= m_height);
    }

    private Vector2 TunnelThroughWalls(Vector2 nextPos)
    {
        if (!m_noWalls)
        {
            return nextPos;
        }

        // x axis
        if (nextPos.X < 0)
        {
            nextPos.X = m_width - 1;
        }
        else if (nextPos.X >= m_width)
        {
            nextPos.X = 0;
        }

        // y axis
        if (nextPos.Y < 0)
        {
            nextPos.Y = m_height - 1;
        }
        else if (nextPos.Y >= m_height)
        {
            nextPos.Y = 0;
        }

        return nextPos;
    }
}