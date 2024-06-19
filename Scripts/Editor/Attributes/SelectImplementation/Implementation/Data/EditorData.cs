using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUtilsUnity.Attributes.SelectImplementation.Data
{
    public class EditorData
    {
        public Type BaseType { get; set; }
        public Type[] Types { get; set; }
        public Dictionary<string, int> TypeIndexMap { get; } = new();
        public GUIContent[] NamesGuiContent { get; set; }
    }
}
