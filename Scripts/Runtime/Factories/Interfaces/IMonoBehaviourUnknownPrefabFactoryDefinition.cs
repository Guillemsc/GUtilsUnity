using UnityEngine;

namespace GUtilsUnity.Factories
{
    /// <summary>
    /// Represents a definition interface for a MonoBehaviour factory that creates instances of a
    /// specific type with an unknown prefab (provided by this definition).
    /// </summary>
    /// <typeparam name="TCreation">The type of MonoBehaviour to be created.</typeparam>
    public interface IMonoBehaviourUnknownPrefabFactoryDefinition<TCreation> where TCreation : Component
    {
        /// <summary>
        /// Gets the prefab that will be instantiated by the MonoBehaviour unknown prefab factory.
        /// </summary>
        TCreation Prefab { get; }
    }
}
