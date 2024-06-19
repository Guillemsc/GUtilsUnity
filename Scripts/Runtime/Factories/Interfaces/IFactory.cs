namespace GUtilsUnity.Factories
{
    /// <summary>
    /// Represents a factory interface for creating instances of a specific type.
    /// </summary>
    /// <typeparam name="TCreation">The type of object to be created.</typeparam>
    public interface IFactory<TCreation>
    {
        /// <summary>
        /// Tries to create an instance of the specified type.
        /// </summary>
        /// <param name="creation">When this method returns, contains the created instance
        /// if successful, or the default value if not.</param>
        /// <returns><c>true</c> if the creation was successful; otherwise, <c>false</c>.</returns>
        bool TryCreate(out TCreation creation);
    }

    /// <summary>
    /// Represents a factory interface for creating instances of a specific type using a definition.
    /// </summary>
    /// <typeparam name="TDefinition">Some data that can be used to construct the object.</typeparam>
    /// <typeparam name="TCreation">The type of object to be created.</typeparam>
    public interface IFactory<in TDefinition, TCreation>
    {
        /// <summary>
        /// Tries to create an instance of the specified type using the provided definition.
        /// </summary>
        /// <param name="definition">Some data that can be used to construct the object.</param>
        /// <param name="creation">When this method returns, contains the created instance if successful,
        /// or the default value if not.</param>
        /// <returns><c>true</c> if the creation was successful; otherwise, <c>false</c>.</returns>
        bool TryCreate(TDefinition definition, out TCreation creation);
    }
}
