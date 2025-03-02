using System.Diagnostics;
using System.Numerics;

namespace Simulation;

struct Snake
{
    public LinkedList<Vector2> BodyParts;

    public Snake()
    {
        BodyParts = new LinkedList<Vector2>();
    }

    public Vector2 Head
    {
        get
        {
            Debug.Assert(BodyParts.Count != 0);
            return BodyParts.FirstOrDefault();
        }
    }

    public Vector2 Tail
    {
        get
        {
            Debug.Assert(BodyParts.Count != 0);
            return BodyParts.LastOrDefault();
        }
    }

    public bool HitsBody(Vector2 pos)
    {
        LinkedListNode<Vector2>? current = BodyParts.First;

        while (current != null)
        {
            if (current?.Value == pos)
            {
                return true;
            }

            current = current?.Next;
        }

        return false;
    }
}