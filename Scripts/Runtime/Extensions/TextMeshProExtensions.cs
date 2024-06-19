using System;
using GUtilsUnity.TextMeshPro.Configuration;
using GUtilsUnity.TextMeshPro.EventHandlers;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace GUtilsUnity.Extensions
{
    public static class TextMeshProExtensions
    {
        /// <summary>
        /// Sets the alpha value of a TextMeshPro text component's color to the specified value.
        /// </summary>
        public static void SetAlpha(this TMP_Text text, float alpha)
        {
            Color newColor = text.color;
            newColor.a = alpha;
            text.color = newColor;
        }

        /// <summary>
        /// Extension method to format a sprite using the TextMeshProSpriteConfiguration.
        /// </summary>
        /// <param name="textMeshProSpriteConfiguration">The TextMeshProSpriteConfiguration to use for formatting.</param>
        /// <param name="isTinted">Determines whether the sprite should be tinted.</param>
        /// <returns>A formatted sprite string.</returns>
        /// <remarks>
        /// This method formats a sprite using the provided TextMeshProSpriteConfiguration.
        /// The sprite is represented as a string with specific attributes, such as the sprite's asset name, sprite name, and whether it should be tinted.
        /// The method uses the assetName and spriteName properties from the TextMeshProSpriteConfiguration to create the formatted sprite string.
        /// The isTinted parameter determines whether the sprite should be tinted or not.
        /// </remarks>
        public static string FormatSprite(this TextMeshProSpriteConfiguration textMeshProSpriteConfiguration, bool isTinted)
        {
            return FormatSprite(
                textMeshProSpriteConfiguration.SpriteAsset.name,
                textMeshProSpriteConfiguration.SpriteName,
                isTinted);
        }

        /// <summary>
        /// Formats a sprite using the provided asset name, sprite name, and tinting option.
        /// </summary>
        /// <param name="assetName">The name of the sprite's asset.</param>
        /// <param name="spriteName">The name of the sprite.</param>
        /// <param name="isTinted">Determines whether the sprite should be tinted.</param>
        /// <returns>A formatted sprite string.</returns>
        /// <remarks>
        /// This method formats a sprite string using the provided asset name and sprite name.
        /// The sprite string is formatted with the appropriate attributes for TextMeshPro, including the asset name, sprite name, and optional tinting.
        /// The isTinted parameter determines whether the sprite should be tinted or not.
        /// </remarks>
        public static string FormatSprite(string assetName, string spriteName, bool isTinted)
        {
            return $@"<sprite=""{assetName}"" name=""{spriteName}"" {GetTintAttribute(isTinted)}>";
        }

        /// <summary>
        /// Formats a tint attribute following a boolean
        /// </summary>
        /// <param name="isTinted">Conditional value if the tint is enabled or not.</param>
        /// <returns>A string formatted with tint attribute that is properly enabled.</returns>
        public static string GetTintAttribute(bool isTinted)
        {
            return isTinted ? "tint=1" : "tint=0";
        }

        /// <summary>
        /// Formats a sprite name as a string that can be used in TextMeshPro text components.
        /// </summary>
        /// <param name="spriteName">The name of the sprite to format.</param>
        /// <returns>A string formatted with a sprite tag that specifies the sprite's name.</returns>
        public static string FormatSprite(string spriteName)
        {
            return $@"<sprite name=""{spriteName}"">";
        }

        /// <summary>
        /// Formats a color as a string that can be used in TextMeshPro text components.
        /// </summary>
        /// <param name="color">The color to format.</param>
        /// <returns>A string formatted with a color tag that specifies the color's HTML hex code.</returns>
        public static string FormatColor(Color color)
        {
            return $@"<color=""{color.ToHtmlColor()}"">";
        }

        /// <summary>
        /// Subscribes to the TextChanged event of a TMP_Text object and executes the specified action when the event is triggered.
        /// For unsubscribing, use <see cref="UnsubscribeOnTextChanged"/>.
        /// </summary>
        /// <param name="text">The TMP_Text object to subscribe to.</param>
        /// <param name="action">The action to execute when the TextChanged event is triggered.</param>
        /// <returns>The TextChangedEventHandler returned from the subscription, needed to unsubscribe
        /// with the method <see cref="UnsubscribeOnTextChanged"/>.</returns>
        [Pure]
        public static TextChangedEventHandler SubscribeOnTextChanged(this TMP_Text text, Action action)
        {
            void TextChanged(UnityEngine.Object obj)
            {
                if (obj.Equals(text))
                {
                    action.Invoke();
                }
            }

            TextChangedEventHandler textChangedEventHandler = new(TextChanged);

            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(TextChanged);

            return textChangedEventHandler;
        }

        /// <summary>
        /// Unsubscribes a TextChangedEventHandler from the TextChanged event of TMPro_EventManager.
        /// </summary>
        /// <param name="eventHandler">The TextChangedEventHandler to unsubscribe.</param>
        public static void UnsubscribeOnTextChanged(TextChangedEventHandler eventHandler)
        {
            if (eventHandler == null)
            {
                return;
            }

            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(eventHandler.Action);
        }
    }
}
