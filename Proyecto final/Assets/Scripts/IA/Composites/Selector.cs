using System.Collections.Generic;

namespace BehaviorTree
{
    public class Selector : Composite
    {
        public Selector() { }

        public Selector(Behavior b) : base(b) { }

        public Selector(List<Behavior> b) : base(b) { }

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
                        if (s != Status.BH_FAILURE)
                        {
                            return s;
                        }
                    }
                    return Status.BH_FAILURE;
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
                        if (s != Status.BH_FAILURE)
                        {
                            return s;
                        }
                    }
                    return Status.BH_FAILURE;
                }
            }
            catch
            {
                return Status.BH_INVALID;
            }
        }
    }
}
