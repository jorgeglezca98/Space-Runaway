using UnityEngine;

namespace BehaviorTree
{

    class Invert : Decorator
    {
        public Invert(Behavior child) : base(child)
        {

        }

        public override Status Update()
        {
            Debug.Log("HOLA BUENOS MEDIO DÍAS ESTOY EN EL INVERT.");
            Status s = Child.Tick();
            if (s == Status.BH_SUCCESS) return Status.BH_FAILURE;
            if (s == Status.BH_FAILURE) return Status.BH_SUCCESS;
            return s;
        }
    }
}
