using System;
using GUtils.Di.Builder;
using GUtils.Di.Container;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class GameObjectDiExtensions
    {
        public static IDiBindingActionBuilder<T> FromGameObject<T>(
            this IDiBindingBuilder<T> builder,
            GameObject gameObject
            ) where T : MonoBehaviour
        {
            T Function(IDiResolveContainer resolver)
            {
                Type type = typeof(T);

                if (gameObject == null)
                {
                    throw new Exception($"Tried to bind {type.Name} from {nameof(GameObject)}, but {nameof(GameObject)} was null");
                }

                T foundObject = gameObject.GetComponent<T>();

                if (foundObject == null)
                {
                    throw new Exception($"Tried to bind {type.Name} from {nameof(GameObject)}, but the {nameof(MonoBehaviour)} could not be found");
                }

                return foundObject;
            }

            return builder.FromFunction(Function);
        }

        public static IDiBindingActionBuilder<T> FromGameObjectInChildren<T>(
            this IDiBindingBuilder<T> builder,
            GameObject gameObject
            ) where T : MonoBehaviour
        {
            T Function(IDiResolveContainer resolver)
            {
                Type type = typeof(T);

                if (gameObject == null)
                {
                    throw new Exception($"Tried to bind {type.Name} from {nameof(GameObject)}, but {nameof(GameObject)} was null");
                }

                T foundObject = gameObject.GetComponentInChildren<T>();

                if (foundObject == null)
                {
                    throw new Exception($"Tried to bind {type.Name} from {nameof(GameObject)}, but the {nameof(MonoBehaviour)} could not be found");
                }

                return foundObject;
            }

            return builder.FromFunction(Function);
        }
    }
}
