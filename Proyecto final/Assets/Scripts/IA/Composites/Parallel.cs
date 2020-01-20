using System.Collections.Generic;

namespace BehaviorTree
{
    public class Parallel : Composite
    {
        public enum Policy
        {
            RequireOne,
            RequireAll,
            RequireNone
        }

        public Policy successPolicy;

        public Parallel(Policy successPolicy = Policy.RequireAll)
        {
            this.successPolicy = successPolicy;
        }

        public Parallel(Behavior b, Policy successPolicy = Policy.RequireAll) : base(b)
        {
            this.successPolicy = successPolicy;
        }

        public Parallel(List<Behavior> b, Policy successPolicy = Policy.RequireAll) : base(b)
        {
            this.successPolicy = successPolicy;
        }

        public override Status Update()
        {
            int successCount = 0;
            try
            {
                int i = 0;
                if (Status == Status.BH_RUNNING)
                {
                    while (i < children.Count && children[i].Status != Status.BH_RUNNING)
                    {
                        i++;
                    }
                }

                for (; i < children.Count; i++)
                {
                    Status s = children[i].Tick();
                    if (s == Status.BH_SUCCESS)
                    {
                        successCount++;
                    }
                    else if (s != Status.BH_FAILURE)
                    {
                        return s;
                    }
                }

                if (successCount == children.Count)
                {
                    return successPolicy == Policy.RequireAll ? Status.BH_SUCCESS : Status.BH_FAILURE;
                }

                if (successCount == 0)
                {
                    return successPolicy == Policy.RequireNone ? Status.BH_SUCCESS : Status.BH_FAILURE;
                }

                return successPolicy == Policy.RequireOne ? Status.BH_SUCCESS : Status.BH_FAILURE;

            }
            catch
            {
                return Status.BH_INVALID;
            }
        }
    }
}
