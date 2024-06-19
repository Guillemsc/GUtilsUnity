using System.Collections.Generic;
using UnityEngine;

namespace GUtilsUnity.SpritePacker
{
    [CreateAssetMenu(fileName = "SpriteAtlasTexturePacker", menuName = "PopcoreCore/SpritePacker/SpriteAtlasTexturePacker", order = 1)]
    public class SpriteAtlasTexturePacker : ScriptableObject
    {
        public List<Sprite> Sprites = new();
        public Texture2D GeneratedTexture;
    }
}
