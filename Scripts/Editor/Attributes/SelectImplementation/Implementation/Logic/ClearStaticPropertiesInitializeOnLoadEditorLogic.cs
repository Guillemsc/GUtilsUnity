using GUtilsUnity.Attributes.SelectImplementation.Data;
using UnityEditor;

namespace GUtilsUnity.SelectImplementation.Logic
{
    [InitializeOnLoad]
    public static class ClearStaticPropertiesInitializeOnLoadEditorLogic
    {
        static ClearStaticPropertiesInitializeOnLoadEditorLogic()
        {
            EditorApplication.update += Update;
        }

        static void Update()
        {
            SerializeReferenceFixData.HasUpdated.Clear();
            SerializeReferenceFixData.ActiveManagedReferences.Clear();
        }
    }
}
