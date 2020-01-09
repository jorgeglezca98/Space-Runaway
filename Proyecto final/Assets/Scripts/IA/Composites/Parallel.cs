

namespace BehaviorTree
{
    class Parallel : Composite
    {
        public enum Policy
        {
            RequireOne,
            RequireAll,
        }

        public Policy failurePolicy = Policy.RequireAll;
        public Policy successPolicy = Policy.RequireAll;

        public override Status Update()
        {
            int successCount = 0;
            int failureCount = 0;
            try
            {
                foreach (Behavior child in Children)
                {
                    Status s = child.Tick();
                    if (s == Status.BH_SUCCESS)
                    {
                        successCount++;
                        if (successPolicy == Policy.RequireOne)
                            return Status.BH_SUCCESS;
                    }

                    if (s == Status.BH_FAILURE)
                    {
                        failureCount++;
                        if (failurePolicy == Policy.RequireOne)
                            return Status.BH_FAILURE;
                    }
                }

                if (successPolicy == Policy.RequireAll && successCount == Children.Count)
                    return Status.BH_SUCCESS;

                if (failurePolicy == Policy.RequireAll && failureCount == Children.Count)
                    return Status.BH_FAILURE;

                // if all policies are "requireAll" and some BH fails, then BH_RUNNING will be returned, 
                // even if there are no behaviours running at the moment
                return Status.BH_RUNNING;
            }
            catch
            {
                return Status.BH_INVALID;
            }
        }

        /*public:
            enum Policy {
               RequireOne,
               RequireAll,
            }
            public Parallel(Policy success, Policy failure);
            protected Policy m_eSuccessPolicy;
            protected Policy m_eFailurePolicy;
            protected virtual Status update() override;

            virtual Status update() {
                size_t iSuccessCount = 0, iFailureCount = 0;
                for (auto it: m_Children) {
                   Behavior& b = **it;
                   if (!b.isTerminated()) b.tick();
                   if (b.getStatus() == BH_SUCCESS) {
                       ++iSuccessCount;
                       if (m_eSuccessPolicy == RequireOne)
                           return BH_SUCCESS;
                   }
                   if (b.getStatus() == BH_FAILURE) {
                       ++iFailureCount;
                       if (m_eFailurePolicy == RequireOne)
                           return BH_FAILURE;
                    } 
                }
                if (m_eFailurePolicy == RequireAll && iFailureCount == size)
                   return BH_FAILURE;
                if (m_eSuccessPolicy == RequireAll && iSuccessCount == size)
                   return BH_SUCCESS;
                return BH_RUNNING;
            }
            void Parallel::onTerminate(Status) {
                for (auto it: m_Children) {
                   Behavior& b = **it;
                   if (b.isRunning()) b.abort();
                }
            }*/
    }
}