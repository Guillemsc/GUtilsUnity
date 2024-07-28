using System.Linq;
using GUtils.Extensions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore;

namespace GUtilsUnity.SpriteAssetUpdating
{
    [CustomEditor(typeof(SpriteAssetUpdater))]
    public sealed class SpriteAssetUpdaterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SpriteAssetUpdater spriteData = (SpriteAssetUpdater)target;

            if (GUILayout.Button("Update Sprite Glyphs"))
            {
                UpdateSpriteGlyphs(spriteData);
            }
        }

        void UpdateSpriteGlyphs(SpriteAssetUpdater spriteData)
        {
            TMP_SpriteAsset spriteAsset = spriteData.SpriteAsset;

            // Clear existing glyphs
            spriteAsset.spriteCharacterTable.Clear();
            spriteAsset.spriteGlyphTable.Clear();

            // Get all sprites from the texture
            Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(spriteAsset.spriteSheet))
                .OfType<Sprite>()
                .ToArray();

            // Add new glyphs for each sprite
            foreach ((Sprite sprite, int spriteIndex) in sprites.ZipIndex())
            {
                GlyphRect glyphRect = new(
                    (int)sprite.rect.x,
                    (int)sprite.rect.y,
                    (int)sprite.rect.width,
                    (int)sprite.rect.height
                );

                TMP_SpriteGlyph spriteGlyph = new TMP_SpriteGlyph
                {
                    index = (uint)spriteIndex,
                    sprite = sprite,
                    metrics = new GlyphMetrics(sprite.rect.width, sprite.rect.height, spriteData.GlobalBearingX, spriteData.GlobalBearingY, sprite.rect.width),
                    glyphRect = glyphRect,
                    scale = 1.0f,
                    atlasIndex = 0
                };

                spriteAsset.spriteGlyphTable.Add(spriteGlyph);

                TMP_SpriteCharacter spriteCharacter = new TMP_SpriteCharacter
                {
                    glyphIndex = (uint)spriteIndex,
                    glyph = spriteGlyph,
                    unicode = (uint)spriteIndex,
                    name = sprite.name,
                    scale = 1.0f,
                };

                spriteAsset.spriteCharacterTable.Add(spriteCharacter);
            }

            // Refresh the sprite asset
            EditorUtility.SetDirty(spriteAsset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
