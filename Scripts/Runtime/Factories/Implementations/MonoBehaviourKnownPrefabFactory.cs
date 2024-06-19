using GUtilsUnity.Disposing.Disposables;
using GUtilsUnity.Logging.Loggers;
using UnityEngine;
using LogType = GUtilsUnity.Logging.Enums.LogType;

namespace GUtilsUnity.Factories
{
    /// <summary>
    /// Represents an abstract base class for a factory that creates instances
    /// of a MonoBehaviour-based object of type <typeparamref name="TCreation"/>
    /// using a known prefab and optional parent transform.
    /// </summary>
    /// <typeparam name="TDefinition">Information used for creating the object.</typeparam>
    /// <typeparam name="TCreation">The type of the object to be created, which is a subclass of MonoBehaviour.</typeparam>
    /// <remarks>
    /// Created disposable object will automatically destroy the GameObject when Dispose is called.
    /// </remarks>
    public abstract class MonoBehaviourKnownPrefabFactory<TDefinition, TCreation>
        : IFactory<TDefinition, IDisposable<TCreation>> where TCreation : MonoBehaviour
    {
        readonly TCreation _prefab;
        readonly Transform _parent;

        protected MonoBehaviourKnownPrefabFactory(TCreation prefab, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
        }

        /// <inheritdoc />
        public bool TryCreate(TDefinition definition, out IDisposable<TCreation> creation)
        {
            if (_prefab == null)
            {
                DebugOnlyUnityLogger.Instance.Log(
                    Logging.Enums.LogType.Error,
                    "Prefab not found at {0} while trying to create {1} instance.",
                    nameof(MonoBehaviourKnownPrefabFactory<TDefinition, TCreation>),
                    typeof(TCreation).Name
                );
                creation = default;
                return false;
            }

            TCreation instance = Object.Instantiate(_prefab, _parent, false);

            if(instance == null)
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
            if(toDispose == null)
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
