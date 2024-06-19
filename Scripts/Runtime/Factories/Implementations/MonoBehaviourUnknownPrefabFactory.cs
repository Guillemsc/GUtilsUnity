using GUtilsUnity.Disposing.Disposables;
using GUtilsUnity.Logging.Loggers;
using UnityEngine;

namespace GUtilsUnity.Factories
{
    /// <summary>
    /// Represents an abstract base class for a factory that creates instances
    /// of a component-based object of type <typeparamref name="TCreation"/>
    /// using an unknown prefab obtained from the provided definition.
    /// </summary>
    /// <typeparam name="TDefinition">The type of the definition used to create the object.</typeparam>
    /// <typeparam name="TCreation">The type of the object to be created, which is a subclass of Component.</typeparam>
    /// <remarks>
    /// Created disposable object will automatically destroy the GameObject when Dispose is called.
    /// </remarks>
    public abstract class MonoBehaviourUnknownPrefabFactory<TDefinition, TCreation>
        : IFactory<TDefinition, IDisposable<TCreation>>
        where TDefinition : IMonoBehaviourUnknownPrefabFactoryDefinition<TCreation>
        where TCreation : Component
    {
        readonly Transform _parent;

        protected MonoBehaviourUnknownPrefabFactory(Transform parent = null)
        {
            _parent = parent;
        }

        /// <inheritdoc />
        public bool TryCreate(TDefinition definition, out IDisposable<TCreation> creation)
        {
            if (definition.Prefab == null)
            {
                DebugOnlyUnityLogger.Instance.Log(
                    Logging.Enums.LogType.Error,
                    "Prefab not found at {0} while trying to create {1} instance.",
                    nameof(MonoBehaviourUnknownPrefabFactory<TDefinition, TCreation>),
                    typeof(TCreation).Name
                );
            }

            TCreation instance = Object.Instantiate(definition.Prefab, _parent, false);

            if (instance == null)
            {
                creation = default;
                return false;
            }

            Init(definition, instance);

            creation = new CallbackDisposable<TCreation>(
                instance,
                Dispose
                );

            return true;
        }

        void Dispose(TCreation toDispose)
        {
            if (toDispose == null)
            {
                return;
            }

            toDispose.transform.SetParent(null);

            OnDispose(toDispose);

            Object.Destroy(toDispose.gameObject);
        }

        /// <summary>
        /// Used for setting up the created object.
        /// </summary>
        /// <param name="definition">The definition used to create the object instance.</param>
        /// <param name="creation">The created object instance.</param>
        protected abstract void Init(TDefinition definition, TCreation creation);

        /// <summary>
        /// Can be implemented for performing additional disposal actions for the specified object instance.
        /// </summary>
        /// <param name="toDispose">The object instance to dispose.</param>
        protected virtual void OnDispose(TCreation toDispose) { }
    }
}
