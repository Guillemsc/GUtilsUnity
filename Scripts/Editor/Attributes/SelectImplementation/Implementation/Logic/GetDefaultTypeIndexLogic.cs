using System;
using GUtilsUnity.Attributes.SelectImplementation.Data;

namespace GUtilsUnity.Attributes.SelectImplementation.Logic
{
    public static class GetDefaultTypeIndexLogic
    {
        public static int Execute(
            EditorData editorData
           )
        {
            for (int i = 0; i < editorData.Types.Length; ++i)
            {
                Type type = editorData.Types[i];

                Attribute defaultTypeAttribute = Attribute.GetCustomAttribute(type, typeof(SelectImplementationDefaultTypeAttribute));

                if (defaultTypeAttribute is SelectImplementationDefaultTypeAttribute _)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
