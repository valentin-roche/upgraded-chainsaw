using UnityEngine;

namespace UnityMovementAI
{
    public class PursueUnit : MonoBehaviour
    {
        public MovementAIRigidbody target;

        SteeringBasics steeringBasics;
        Pursue pursue;

        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementAIRigidbody>();
            steeringBasics = GetComponent<SteeringBasics>();
            pursue = GetComponent<Pursue>();
        }

        void FixedUpdate()
        {
            Vector3 accel = pursue.GetSteering(target);

            steeringBasics.Steer(accel);
            steeringBasics.LookWhereYoureGoing();
        }
    }
}