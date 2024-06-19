using GUtilsUnity.Attributes;

namespace GUtilsUnity.Di.Container
{
    /// <summary>
    /// Contains a mapping of how objects need to be created, including their dependencies.
    /// </summary>
    public interface IDiResolveContainer
    {
        /// <summary>
        /// Creates the requested binded object if it hadn't been created already, and returns it.
        /// </summary>
        /// <exception cref="System.Exception">Thrown when type cannot be found.</exception>
        /// <exception cref="System.Exception">Thrown when there is a circular dependency creating the requested object.</exception>
        /// <exception cref="System.NullReferenceException">Thrown when created value is null.</exception>
        T Resolve<T>();

        [Experimental]
        T Resolve<T>(object id);

        /// <summary>
        /// If type is found, creates the requested binded object if it hadn't been created already, and returns it.
        /// </summary>
        /// <exception cref="System.Exception">Thrown when there is a circular dependency creating the requested object.</exception>
        /// <exception cref="System.NullReferenceException">Thrown when created value is null.</exception>
        bool TryResolve<T>(out T value);

        [Experimental]
        bool TryResolve<T>(object id, out T value);
    }
}
