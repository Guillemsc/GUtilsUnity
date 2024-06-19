using UnityEngine;
using UnityEngine.UI;

namespace GUtilsUnity.Attributes
{
    public sealed class ComponentOfTest : MonoBehaviour
    {
        public GameObject GameObject;
        [ComponentOf(nameof(GameObject))] public Image Image;
        [ChildComponentOf(nameof(GameObject))] public Image ChildImage;
    }
}
