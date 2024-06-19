using UnityEngine;

namespace GUtilsUnity.Json.ScriptableObjects
{
    // Necessary for drawing the custom Editor.
    public abstract class BaseJsonGeneratorScriptableObject : ScriptableObject
    {
        public abstract void GenerateJson();
        public abstract string GenerateCopyJson(bool indented);
    }
}
