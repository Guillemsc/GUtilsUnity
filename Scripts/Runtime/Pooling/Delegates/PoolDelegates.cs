using UnityEngine;

namespace GUtilsUnity.Pooling
{
    public static class PoolDelegates
    {
        public static T InstantiatePrefabCreateAction<T>(T prefab, Transform parent) where T : MonoBehaviour
        {
            T instance = Object.Instantiate(prefab, parent);
            instance.gameObject.SetActive(false);
            return instance;
        }
        
        public static GameObject InstantiatePrefabCreateAction(GameObject prefab, Transform parent)
        {
            GameObject instance = Object.Instantiate(prefab, parent);
            instance.gameObject.SetActive(false);
            return instance;
        }
        
        public static void DisableGameObjectAction<T>(T item) where T : MonoBehaviour
        {
            item.gameObject.SetActive(false);
        }
        
        public static void EnableGameObjectAction<T>(T item) where T : MonoBehaviour
        {
            item.gameObject.SetActive(true);
        }
        
        public static void DisableGameObjectAction(GameObject item) 
        {
            item.SetActive(false);
        }
        
        public static void EnableGameObjectAction(GameObject item)
        {
            item.SetActive(true);
        }
    }
}