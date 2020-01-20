using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequence : Composite
    {
        public Sequence() { }

        public Sequence(Behavior b) : base(b) { }

        public Sequence(List<Behavior> b) : base(b) { }

        public override Status Update()
        {
            //Keep going until a child behavior says it’s running.
            try
            {
                if (Status != Status.BH_RUNNING) // Sequence Status
                {
                    foreach (Behavior child in children)
                    {
                        Status s = child.Tick();
                        if (s != Status.BH_SUCCESS)
                        {
                            return s;
                        }
                    }
                    return Status.BH_SUCCESS;
                }
                else
                {
                    int i = 0;
                    while (i < children.Count && children[i].Status != Status.BH_RUNNING)
                    {
                        i++;
                    }

                    for (; i < children.Count; i++)
                    {
                        Status s = children[i].Tick();
                        if (s != Status.BH_SUCCESS)
                        {
                            return s;
                        }
                    }
                    return Status.BH_SUCCESS;
                }
            }
            catch
            {
                return Status.BH_INVALID;
            }
        }
    }
}
