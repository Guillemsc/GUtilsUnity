using UnityEngine;

namespace ProgressionMap.Cameras.Configuration
{
    [CreateAssetMenu(fileName = "ReceiveRewardsUIConfiguration", menuName = "PopcoreCore/ReceiveRewardUi/Configuration", order = 1)]
    public sealed class ReceiveRewardsUiConfiguration : ScriptableObject
    {
        [Header("Camera Drag")]
        [SerializeField, Min(0)] float _receiveRewardFinalRewardScale = 1f;

        public float ReceiveRewardFinalRewardScale => _receiveRewardFinalRewardScale;
    }
}
