using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.SpritePacker
{
    [CustomEditor(typeof(SpriteAtlasTexturePacker))]
    public sealed class SpriteAtlasTexturePackerEditor : Editor
    {
        public SpriteAtlasTexturePacker ActualTarget => (SpriteAtlasTexturePacker)target;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Pack Sprites"))
            {
                PackSprites(ActualTarget);
            }
        }

        // void GetSpritesFromFolder()
        // {
        //     string absolutePath = DirectoryExtensions.AssetsRelativePathToAbsolutePath(ActualTarget.SpritesFolder);
        //
        //     bool folderExists = Directory.Exists(absolutePath);
        //
        //     if (!folderExists)
        //     {
        //         return;
        //     }
        //
        //     List<Sprite> sprites = AssetDatabaseExtensions.FindAssetsByTypeAtFolders<Sprite>(ActualTarget.SpritesFolder);
        //
        //     ActualTarget.Sprites.Clear();
        //     ActualTarget.Sprites.AddRange(sprites);
        // }

        void PackSprites(SpriteAtlasTexturePacker packer)
        {
            var maxSize = packer.Sprites.Select(x => x.rect.size).MaxComponents();
            var maxComponentSize = Mathf.CeilToInt(maxSize.MaxComponent());

            int cells = Mathf.CeilToInt(Mathf.Sqrt(packer.Sprites.Count));

            // Ensure the texture size is a power of 2
            int texSize = Mathf.NextPowerOfTwo(cells * maxComponentSize);

            // Create the texture
            Texture2D tex = new Texture2D(texSize, texSize);

            // Clear the texture
            var fillColorArray = tex.GetPixels();
            for (var i = 0; i < fillColorArray.Length; ++i)
            {
                fillColorArray[i] = Color.clear;
            }

            tex.SetPixels(fillColorArray);

            // Add the sprites and create spriteMetaData array
            SpriteMetaData[] spriteMetaData = new SpriteMetaData[packer.Sprites.Count];
            for (int i = 0; i < packer.Sprites.Count; i++)
            {
                var sprite = packer.Sprites[i];
                var pixels = sprite.texture.GetPixelsEditor((int)sprite.rect.x,
                    (int)sprite.rect.y,
                    (int)sprite.rect.width,
                    (int)sprite.rect.height);
                tex.SetPixels((i % cells) * maxComponentSize, (i / cells) * maxComponentSize, (int)sprite.rect.width, (int)sprite.rect.height, pixels);

                SpriteMetaData smd = new SpriteMetaData
                {
                    name = sprite.name,
                    rect = new Rect((i % cells) * maxComponentSize, (i / cells) * maxComponentSize, sprite.rect.width, sprite.rect.height),
                    alignment = (int)SpriteAlignment.Center,
                    pivot = new Vector2(0.5f, 0.5f)  // Assuming a center pivot.
                };

                spriteMetaData[i] = smd;
            }

            tex.Apply();

            string path = packer.GeneratedTexture
                ? AssetDatabase.GetAssetPath(packer.GeneratedTexture)
                : Path.Combine(Path.GetDirectoryName(AssetDatabase.GetAssetPath(packer)), packer.name + ".png");

            File.WriteAllBytes(path, tex.EncodeToPNG());
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            packer.GeneratedTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));

            // Set the spritesheet data and apply the changes.
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            textureImporter.crunchedCompression = true;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spriteImportMode = SpriteImportMode.Multiple;
            textureImporter.spritesheet = spriteMetaData;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
    }
}
