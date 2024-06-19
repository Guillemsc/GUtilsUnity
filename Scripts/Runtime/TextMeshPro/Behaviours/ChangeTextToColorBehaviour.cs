using TMPro;
using UnityEngine;

namespace GUtilsUnity.TextMeshPro.Behaviours
{
    public sealed class ChangeTextToColorBehaviour : MonoBehaviour
    {
        public TMP_Text Text;
        public Color Color;

        public void Execute()
        {
            Text.color = Color;
        }
    }
}
