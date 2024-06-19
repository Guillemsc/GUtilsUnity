using TMPro;
using UnityEngine;

namespace GUtilsUnity.SpriteAssetUpdating
{
    [CreateAssetMenu(fileName = "SpriteAssetUpdater", menuName = "PopcoreCore/SpriteAssetUpdater")]
    public class SpriteAssetUpdater : ScriptableObject
    {
        public TMP_SpriteAsset SpriteAsset;
        public float GlobalBearingX;
        public float GlobalBearingY;
    }
}
