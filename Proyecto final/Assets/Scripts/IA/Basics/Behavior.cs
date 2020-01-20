namespace BehaviorTree
{
    public abstract class Behavior
    {
        public Status Status { get; set; }

        public virtual void OnInitialize() { }
        public virtual void OnTerminate(Status s) { }

        public abstract Status Update();

        protected Behavior()
        {
            Status = Status.BH_INVALID;
        }

        public Status Tick()
        {
            if (Status != Status.BH_RUNNING)
            {
                OnInitialize();
            }

            Status = Update();

            if (Status != Status.BH_RUNNING)
            {
                OnTerminate(Status);
            }

            return Status;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return this == obj;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
