namespace AI;

enum Activation
{
    None,
    ReLU,
    Logistic,
    Tanh,
}

static class ActivationFunction
{
    public delegate double ActivationFuncDelegate(double x);

    public static ActivationFuncDelegate GetFunc(Activation activation)
    {
        switch (activation)
        {
            case Activation.ReLU:
            {
                return ReLU;
            }

            case Activation.Logistic:
            {
                return Logistic;
            }

            case Activation.Tanh:
            {
                return Tanh;
            }
        }

        return None;
    }

    public static double None(double x)
    {
        return x;
    }

    public static double ReLU(double x)
    {
        return Math.Max(0, x);
    }

    public static double Logistic(double x)
    {
        return 1f / (1f + Math.Exp(-x));
    }

    public static double Tanh(double x)
    {
        return Math.Tanh(x);
    }
}