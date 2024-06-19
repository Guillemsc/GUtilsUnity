using System;
using Object = UnityEngine.Object;

namespace GUtilsUnity.TextMeshPro.EventHandlers
{
    public sealed class TextChangedEventHandler
    {
        public Action<Object> Action;

        public TextChangedEventHandler(Action<Object> action)
        {
            Action = action;
        }
    }
}
