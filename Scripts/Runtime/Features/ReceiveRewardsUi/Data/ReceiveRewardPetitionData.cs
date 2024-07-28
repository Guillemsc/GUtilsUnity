using System;
using UnityEngine;

namespace GUtilsUnity.Features.ReceiveRewardsUi.Data
{
    public sealed class ReceiveRewardPetitionData
    {
        public GameObject Prefab { get; }
        public Vector2 TargetScreenPosition { get; }
        public Action<GameObject> OnCreated { get; }
        public Action OnReceive { get; }

        public ReceiveRewardPetitionData(
            GameObject prefab,
            Vector2 targetScreenPosition,
            Action<GameObject> onCreated,
            Action onReceive
            )
        {
            Prefab = prefab;
            TargetScreenPosition = targetScreenPosition;
            OnCreated = onCreated;
            OnReceive = onReceive;
        }
    }
}
