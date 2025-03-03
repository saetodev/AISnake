using log4net;

namespace Simulation;

class Recording
{
    public int WorkerID;
    public int Score;
    public int Seed;
    public List<Action> Actions;

    public Recording(int workerID, int seed)
    {
        WorkerID = workerID;
        Seed     = seed;
        Actions  = new List<Action>();
    }
}

class SimManager
{
    private static readonly ILog s_logger = LogManager.GetLogger(typeof(SimManager));

    private Task[] m_tasks;

    private object m_recordedSimsLock      = new object();
    private List<Recording> m_recordedSims = new List<Recording>();

    public List<Recording> RecordedSims
    {
        get { return m_recordedSims; }
    }

    public SimManager(int maxRuns)
    {
        m_tasks = new Task[maxRuns];
    }

    public void Run()
    {
        for (int i = 0; i < m_tasks.Length; i++)
        {
            int workerID = i;
            m_tasks[i]   = Task.Run(() => Simulate(workerID));
        }

        Task.WhenAll(m_tasks).Wait();

        s_logger.Info("All Tasks Finished");
    }

    private void Simulate(int workerID)
    {
        s_logger.Info($"Worker {workerID}: Starting simulation");

        Status status       = Status.Running;
        SnakeEngine engine  = new SnakeEngine(40, 23);
        Recording recording = new Recording(workerID, engine.Seed);

        Random random = new Random();

        while (status == Status.Running)
        {
            //TODO: get the ai to output an action
            Action[] actions = { Action.None, Action.TurnLeft, Action.TurnRight };
            Action action    = actions[random.Next(0, actions.Length)];
            
            recording.Actions.Add(action);
            status = engine.Step(action);
        }

        recording.Score = engine.Score;

        lock (m_recordedSimsLock)
        {
            m_recordedSims.Add(recording);
        }

        s_logger.Info($"Worker {workerID}: Finished with {status} and score {engine.Score}");
    }
}