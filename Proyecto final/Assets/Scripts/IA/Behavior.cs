using Status;

class Behavior {

	private Status status = BH_INVALID;

	public virtual void OnInitialize() {};
	public abstract Status Update();
	public virtual void OnTerminate(Status s) {};

    public Status Tick() {
       if (status != BH_RUNNING) OnInitialize();
       status = Update();
       if (status != BH_RUNNING) OnTerminate(status);
       return status;
	}

	public override bool Equals(Object obj) {
		if ((obj == null) || ! this.GetType().Equals(obj.GetType())) {
			return false;
		} else {
			return this == obj;
		}   
   }
}