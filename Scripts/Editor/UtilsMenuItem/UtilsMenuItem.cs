using System.IO;
using System.Linq;
using GUtils.Extensions;
using GUtilsUnity.Extensions;
using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Serialization.UtilsMenuItem
{
    public static class UtilsMenuItem
    {
        const string CopyAssetGuidItemName = "Assets/PopcoreCore/Copy asset Guid";
        const string MakeTexturePowerOfTwo = "Assets/PopcoreCore/Make power of two";

        [MenuItem(CopyAssetGuidItemName, true)]
        static bool CopyGuidValidate()
        {
            return Selection.activeObject != null;
        }

        [MenuItem(CopyAssetGuidItemName)]
        public static void CopyGuid()
        {
            var asset = Selection.activeObject;
            var assetPathToGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset));
            GUIUtility.systemCopyBuffer = assetPathToGuid;
        }

        [MenuItem(MakeTexturePowerOfTwo, true)]
        static bool ResizeSpriteValidate()
        {
            var activeObject = Selection.activeObject;
            return activeObject is Texture2D;
        }

        [MenuItem(MakeTexturePowerOfTwo)]
        static void ResizeSprite()
        {
            var texture = Selection.activeObject as Texture2D;

            if (texture == null)
            {
                Debug.Log("Please select a Texture2D in the Project window.");
                return;
            }

            var width = texture.width;
            var height = texture.height;

            if (width.IsPowerOfTwo() && height.IsPowerOfTwo())
            {
                Debug.Log($"Asset is already a power of 2, skipping");
                return;
            }


            // Resize the texture to the next power of two
            var newWidth = width.IsPowerOfTwo() ? width : width.NextPowerOfTwo();
            var newHeight = height.IsPowerOfTwo() ? height : height.NextPowerOfTwo();

            var largestSize = Mathf.Max(newWidth, newHeight);

            var resizedTexture = new Texture2D(largestSize, largestSize);

            // Center the original texture in the resized texture
            var offsetX = (largestSize - width) / 2;
            var offsetY = (largestSize - height) / 2;

            resizedTexture.SetAllPixelsAsColor(new Color(0, 0, 0, 0));

            var pixelColor = texture.GetPixelsEditor(0, 0, texture.width, texture.height);
            resizedTexture.SetPixels(offsetX, offsetY, texture.width, texture.height, pixelColor);


            resizedTexture.Apply();

            // Save the resized texture as a new asset
            var bytes = resizedTexture.EncodeToPNG();
            var assetPath = AssetDatabase.GetAssetPath(texture);
            var directory = Path.GetDirectoryName(assetPath);
            var filename = Path.GetFileNameWithoutExtension(assetPath);
            var newAssetPath = $"{directory}/{filename}_POT.png";

            File.WriteAllBytes(newAssetPath, bytes);
            AssetDatabase.Refresh();

            Debug.Log($"Saved resized sprite as {newAssetPath}");
        }
    }
}
