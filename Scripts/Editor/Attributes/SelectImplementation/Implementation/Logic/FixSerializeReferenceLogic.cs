using GUtilsUnity.Attributes.SelectImplementation.Data;
using UnityEditor;

namespace GUtilsUnity.Attributes.SelectImplementation.Logic
{
    public static class FixSerializeReferenceLogic
    {
        public static void Execute(SerializedProperty property, EditorData editorData, int typeIndex)
        {
            long id = property.managedReferenceId;

            if (!SerializeReferenceFixData.HasUpdated.Add(property.propertyPath))
            {
                return;
            }

            if (SerializeReferenceFixData.ActiveManagedReferences.Add(id))
            {
                return;
            }

            InitializePropertyAtIndexLogic.Execute(
                editorData,
                property,
                typeIndex
            );
        }
    }
}
