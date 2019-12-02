namespace BehaviorTree {
	class Parallel : Composite {
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

		public override Status Update(){
			return Status.BH_FAILURE;
		}
	}
}