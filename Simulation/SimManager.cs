namespace Simulation;

class SimManager
{
    private Task[] m_tasks;

    public SimManager(int maxRuns)
    {
        m_tasks = new Task[maxRuns];
    }

    public async Task Run()
    {
        for (int i = 0; i < m_tasks.Length; i++)
        {
            int workerID = i;
            m_tasks[i]   = Task.Run(() => Simulate(workerID));
        }

        await Task.WhenAll(m_tasks);
        Console.WriteLine("Done All Tasks");
    }

    private static void Simulate(int workerID)
    {
        Console.WriteLine($"Worker {workerID}: Starting simulation");

        SnakeEngine engine = new SnakeEngine(40, 23);

        Status status = Status.Running;
        Action action = Action.None;

        Random random = new Random();

        while (status == Status.Running)
        {
            //TODO: get the ai to output an action
            Action[] actions = { Action.None, Action.TurnLeft, Action.TurnRight };

            action = actions[random.Next(0, actions.Length)];
            //Console.WriteLine($"Worker {workerID}: Doing action {action}");

            status = engine.Step(action);
        }

        Console.WriteLine($"Worker {workerID}: Finished with {status} and score {engine.Score}");
    }
}