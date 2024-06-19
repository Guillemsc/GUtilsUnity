using UnityEngine;

namespace GUtilsUnity.Features.ExtendableButtons.Configuration
{
    [CreateAssetMenu(fileName = "UiShinyPlayUntilInteractionButtonExtensionConfiguration",
        menuName = "PopcoreCore/Features/ExtendableButtons/UiShinyPlayUntilInteractionButtonExtensionConfiguration")]
    public sealed class UiShinyPlayUntilInteractionButtonExtensionConfiguration : ScriptableObject
    {
        [Range(0f, 1f)] public float Width = 0.25f;
        [Range(-180, 180)] public int Rotation = 135;
        [Range(0f, 1f)] public float Softness = 1f;
        [Range(0f, 1f)] public float Brightness = 0.21f;
        [Range(0f, 1f)] public float Gloss = 0.792f;
        [Range(0f, 10f)] public float InitialPlayDelay = 0.5f;
        [Range(0f, 10f)] public float LoopDelay = 2.7f;
    }
}
