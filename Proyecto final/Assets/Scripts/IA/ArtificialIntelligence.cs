using UnityEngine;

namespace BehaviorTree
{
    public class ArtificialIntelligence : MonoBehaviour
    {

        public static GameObject target;
        public static AudioManager audioManager;
        protected BehaviorTree tree;
        protected int velocity = 40;
        protected GameObject shotPrefab;
        protected int shotMaxDistance = 100;
        protected int shotMinDistance = 10;
        protected int shotSpeed = 10000;
        protected int aimingHelpRange = 10;
        protected float lookForCollisionDistance = 60f;
        protected float shipsWingspan = 10f;
        protected float halfTheShipsLength = 7.5f;
        protected float halfTheShipsHeight = 2.5f;
        protected RotationInfo rotationInfo = new RotationInfo();
        protected float healthThreshold;
        protected float overheatUpperThreshold;
        protected float overheatLowerThreshold;
        protected OverheatStats overheatData = new OverheatStats();
        
        private void FixedUpdate()
        {
            tree.Tick();
        }
        
        public Vector3 GetSpaceshipDimension()
        {
            return new Vector3(shipsWingspan, halfTheShipsHeight, halfTheShipsLength);
        }
    }
}
