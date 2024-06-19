using GUtilsUnity.Attributes;
using GUtilsUnity.Extensions;
using UnityEditor;

namespace GUtilsUnity.Extensions
{
    [Experimental]
    public static class AddressablesSerializedPropertyExtensions
    {
        [Experimental]
        public static UnityEngine.Object GetAssetReferenceObject(this SerializedProperty serializedProperty)
        {
            SerializedProperty assetGuidProperty = serializedProperty.FindPropertyRelative("m_AssetGUID");
            string assetGuid = assetGuidProperty.stringValue;

            AssetDatabaseExtensions.TryFindMainAssetByGuid(assetGuid, out UnityEngine.Object asset);

            return asset;
        }

        [Experimental]
        public static void SetAssetReferenceObject(
            this SerializedProperty serializedProperty,
            UnityEngine.Object obj
            )
        {
            AssetDatabaseExtensions.TryGetAssetGuid(obj, out string assetGuid);

            SerializedProperty assetGuidProperty = serializedProperty.FindPropertyRelative("m_AssetGUID");
            assetGuidProperty.stringValue = assetGuid;
        }
    }
}
