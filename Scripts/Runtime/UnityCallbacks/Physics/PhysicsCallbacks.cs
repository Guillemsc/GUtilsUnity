using GUtilsUnity.Delegates.Generics;
using UnityEngine;

namespace GUtilsUnity.UnityCallbacks.Physics
{
    /// <summary>
    /// By default Unity only allows us to receive physics callbacks on the GameObject that's receiving the collision (OnCollisionEnter, OnCollisionExit, etc).
    /// With tgus ckass we can receive this callbacks on the object that has the collider, and then use them anywhere else whith its events.
    /// </summary>
    public sealed class PhysicsCallbacks : MonoBehaviour
    {
        public event GenericEvent<PhysicsCallbacks, Collision> OnPhysicsCollisionEnter;
        public event GenericEvent<PhysicsCallbacks, Collision> OnPhysicsCollisionStay;
        public event GenericEvent<PhysicsCallbacks, Collision> OnPhysicsCollisionExit;

        public event GenericEvent<PhysicsCallbacks, Collision2D> OnPhysicsCollisionEnter2D;
        public event GenericEvent<PhysicsCallbacks, Collision2D> OnPhysicsCollisionStay2D;
        public event GenericEvent<PhysicsCallbacks, Collision2D> OnPhysicsCollisionExit2D;

        public event GenericEvent<PhysicsCallbacks, Collider> OnPhysicsTriggerEnter;
        public event GenericEvent<PhysicsCallbacks, Collider> OnPhysicsTriggerStay;
        public event GenericEvent<PhysicsCallbacks, Collider> OnPhysicsTriggerExit;

        public event GenericEvent<PhysicsCallbacks, Collider2D> OnPhysicsTriggerEnter2D;
        public event GenericEvent<PhysicsCallbacks, Collider2D> OnPhysicsTriggerStay2D;
        public event GenericEvent<PhysicsCallbacks, Collider2D> OnPhysicsTriggerExit2D;

        void OnCollisionEnter(Collision collision)
        {
            OnPhysicsCollisionEnter?.Invoke(this, collision);
        }

        void OnCollisionStay(Collision collision)
        {
            OnPhysicsCollisionStay?.Invoke(this,  collision);
        }

        void OnCollisionExit(Collision collision)
        {
            OnPhysicsCollisionExit?.Invoke(this,  collision);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            OnPhysicsCollisionEnter2D?.Invoke(this, collision);
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            OnPhysicsCollisionStay2D?.Invoke(this, collision);
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            OnPhysicsCollisionExit2D?.Invoke(this, collision);
        }

        void OnTriggerEnter(Collider collision)
        {
            OnPhysicsTriggerEnter?.Invoke(this, collision);
        }

        void OnTriggerStay(Collider collision)
        {
            OnPhysicsTriggerStay?.Invoke(this, collision);
        }

        void OnTriggerExit(Collider collision)
        {
            OnPhysicsTriggerExit?.Invoke(this, collision);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            OnPhysicsTriggerEnter2D?.Invoke(this, collision);
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            OnPhysicsTriggerStay2D?.Invoke(this, collision);
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            OnPhysicsTriggerExit2D?.Invoke(this, collision);
        }
    }
}
