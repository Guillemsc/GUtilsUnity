using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.PropertyDrawerLayout
{
    public sealed class PropertyDrawerLayoutHelper
    {
        Rect _totalRect;
        Rect _currentRect;

        bool _isFirst;

        public Rect TotalRect => _totalRect;

        public void Init(Rect rect)
        {
            _totalRect = new Rect(rect.position, Vector2.zero);
            _currentRect = rect;

            _isFirst = true;
        }

        public Rect NextVerticalRect(float height)
        {
            _currentRect.height = height;

            if (!_isFirst)
            {
                height += 2f;
                _currentRect.y = _totalRect.y + _totalRect.height + 2.0f;
            }

            _totalRect.height += height;

            _isFirst = false;

            return _currentRect;
        }

        public Rect NextVerticalRect()
        {
            return NextVerticalRect(EditorGUIUtility.singleLineHeight);
        }

        public Rect NextVerticalRect(SerializedProperty serializedProperty)
        {
            return NextVerticalRect(EditorGUI.GetPropertyHeight(serializedProperty));
        }

        public float GetElementsHeight(int elementsCount)
        {
            if (elementsCount <= 0)
            {
                return 0f;
            }

            return (elementsCount * (EditorGUIUtility.singleLineHeight + 2)) + 2;
        }
    }
}
