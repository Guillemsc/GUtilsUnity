using System.Threading.Tasks;
using GUtilsUnity.Optionals;

namespace GUtilsUnity.Factories
{
    /// <summary>
    /// Represents a factory that asynchronously creates an object of type <typeparamref name="TCreation"/>
    /// based on a provided definition of type <typeparamref name="TDefinition"/>.
    /// </summary>
    /// <typeparam name="TDefinition">Some data that can be used to construct the object.</typeparam>
    /// <typeparam name="TCreation">The type of the object to be created.</typeparam>
    public interface ITaskFactory<TDefinition, TCreation>
    {
        /// <summary>
        /// Tries to asynchronously create an object of type <typeparamref name="TCreation"/>
        /// based on the provided definition of type <typeparamref name="TDefinition"/>.
        /// </summary>
        /// <param name="definition">Some data that can be used to construct the object.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an optional
        /// object of type <typeparamref name="TCreation"/>.</returns>
        Task<Optional<TCreation>> TryCreate(TDefinition definition);
    }

    /// <summary>
    /// Represents a factory that asynchronously creates an object of type <typeparamref name="TCreation"/>.
    /// </summary>
    /// <typeparam name="TCreation">The type of the object to be created.</typeparam>
    public interface ITaskFactory<TCreation>
    {
        /// <summary>
        /// Tries to asynchronously create an object of type <typeparamref name="TCreation"/>.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains an optional
        /// object of type <typeparamref name="TCreation"/>.</returns>
        Task<Optional<TCreation>> TryCreate();
    }
}
