using GUtils.Extensions;
using GUtilsUnity.Extensions;
using GUtilsUnity.Repositories;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Popcore.Core.Scripts.Editor.WithAssembly.Repository
{
    [CustomEditor(typeof(ScriptableObjectKeyValueRepository<,>), true)]
    public class ScriptableObjectKeyValueRepositoryEditor : UnityEditor.Editor
    {
        IUpdateKeys _updateKeys;

        bool _isUnityEngineAsset;

        protected virtual void OnEnable()
        {
            _updateKeys = (IUpdateKeys)target;
            _isUnityEngineAsset = target
                .GetType()
                .GetParentTypeOfGenericTypeDefinition(typeof(ScriptableObjectKeyValueRepository<,>))
                .UnsafeGet()
                .GetGenericArguments()[1]
                .IsSubclassOf(typeof(UnityEngine.Object));
        }

        public override void OnInspectorGUI()
        {
            if (_isUnityEngineAsset && GUILayout.Button("Refresh assets from database"))
            {
                RefreshAssetsFromDatabase();
            }

            if (GUILayout.Button("Update keys from values"))
            {
                RefreshKeys();
            }

            base.OnInspectorGUI();
        }

        protected void RefreshKeys()
        {
            _updateKeys.UpdateKeysFromValues();
            serializedObject.ApplyModifiedProperties();
        }

        void RefreshAssetsFromDatabase()
        {
            var repositoryType = target.GetType();
            var repositoryParentType = repositoryType
                .GetParentTypeOfGenericTypeDefinition(typeof(ScriptableObjectKeyValueRepository<,>))
                .UnsafeGet();
            var valueType = repositoryParentType.GetGenericArguments()[1];

            dynamic targetDynamic = target;
            targetDynamic.Clear();

            dynamic func = targetDynamic.GetKeyFromValue();

            var assets = AssetDatabaseExtensions.FindAssetsByTypeNameAndName(valueType.Name, string.Empty);
            foreach (dynamic asset in assets)
            {
                dynamic key = func.Invoke(asset);
                targetDynamic.Add(key, asset);
            }

            EditorUtility.SetDirty(target);
        }
    }
}
