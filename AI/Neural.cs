namespace AI;

struct NeuralNode
{
    public double Sum;
    public double Bias;

    public int Connections;
    public int Depth;

    public Activation Activation;
}

struct NeuralConnection
{
    public int To;
    public double Weight;
    public double Value;
}

struct NetworkInfo
{
    public int Inputs;
    public int Outputs;
    public int Hidden;

    public int Count
    {
        get
        {
            return Inputs + Outputs + Hidden;
        }
    }
}

class NeuralNetwork
{
    private NetworkInfo m_info;

    private List<NeuralNode> m_nodes             = new List<NeuralNode>();
    private List<NeuralConnection> m_connections = new List<NeuralConnection>();
    private List<double> m_outputs               = new List<double>();

    private int m_maxDepth;

    public NeuralNetwork(NetworkInfo info)
    {
        m_info = info;

        m_nodes.Capacity   = m_info.Count;
        m_outputs.Capacity = m_info.Outputs;
    }
}