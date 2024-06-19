using System;
using UnityEngine;

namespace GUtilsUnity.Serialization.SerializableTypes
{
    [Serializable]
    public struct SerializableInterface<T>
    {
        [SerializeField] UnityEngine.Object _object;

        /// <summary>
        /// Provides the serialized Unity object as a value of type T.
        /// </summary>
        public T Value => (T)(object)_object;

        /// <summary>
        /// Creates a new instance of SerializableInterface with provided object of TInterface type.
        /// </summary>
        /// <param name="obj">The object of type TInterface.</param>
        /// <returns>New instance of SerializableInterface.</returns>
        public static SerializableInterface<TInterface> FromObject<TInterface>(TInterface obj)
            where TInterface : UnityEngine.Object
        {
            return new SerializableInterface<TInterface>()
            {
                _object = obj
            };
        }

        /// <summary>
        /// Creates a new instance of SerializableInterface with provided object of TObject type.
        /// </summary>
        /// <param name="obj">The object of type TObject.</param>
        /// <returns>New instance of SerializableInterface.</returns>
        public static SerializableInterface<TInterface> FromObject<TInterface, TObject>(TObject obj)
            where TObject : UnityEngine.Object, TInterface
        {
            return new SerializableInterface<TInterface>()
            {
                _object = obj
            };
        }

        /// <summary>
        /// Creates a new instance of SerializableInterface without type safety checks meaning this operation can fail
        /// </summary>
        /// <param name="obj">The object of type TInterface.</param>
        /// <returns>New instance of SerializableInterface.</returns>
        public static SerializableInterface<TInterface> FromObjectUnsafe<TInterface>(TInterface obj)
        {
            return new SerializableInterface<TInterface>()
            {
                _object = (UnityEngine.Object)(object)obj
            };
        }
        /// <summary>
        /// Attempts to create a new instance of SerializableInterface from a provided object.
        /// This operation succeeds when the object provided is a UnityEngine.Object
        /// </summary>
        /// <param name="obj">The object of type TInterface.</param>
        /// <param name="serializableInterface">Out parameter that will contain the new SerializableInterface if the operation is successful.</param>
        /// <returns>True if the operation was successful, false otherwise.</returns>
        public static bool TryFromObject<TInterface>(TInterface obj, out SerializableInterface<TInterface> serializableInterface)
        {
            if (!(obj is UnityEngine.Object engineObj))
            {
                serializableInterface = default;
                return false;
            }

            serializableInterface = new SerializableInterface<TInterface>()
            {
                _object = engineObj
            };
            return true;
        }
    }
}
