using System;
using GUtilsUnity.Factories;

namespace GUtilsUnity.Extensions
{
    public static class FactoryExtensions
    {
        /// <summary>
        /// Calls <see cref="IFactory{TCreation}.TryCreate"/> and returns the created value.
        /// If it could not create, it throws.
        /// </summary>
        /// <exception cref="InvalidOperationException">When it could not be created.</exception>
        public static TCreation CreateUnsafe<TCreation>(this IFactory<TCreation> factory)
        {
            if (!factory.TryCreate(out TCreation creation))
            {
                throw new InvalidOperationException($"Could not create object on factory {factory.GetType().FullName}");
            }

            return creation;
        }

        /// <summary>
        /// Calls <see cref="IFactory{T}.TryCreate"/> and returns the created value.
        /// If it could not create, it throws.
        /// </summary>
        /// <exception cref="InvalidOperationException">When it could not be created.</exception>
        public static TCreation CreateUnsafe<TDefinition, TCreation>(this IFactory<TDefinition, TCreation> factory, TDefinition definition)
        {
            if (!factory.TryCreate(definition, out TCreation creation))
            {
                throw new InvalidOperationException($"Could not create object on factory {factory.GetType().FullName}");
            }

            return creation;
        }
    }
}
