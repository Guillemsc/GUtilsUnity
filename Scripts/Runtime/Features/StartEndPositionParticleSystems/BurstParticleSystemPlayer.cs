using GUtils.Types;
using UnityEngine;

namespace GUtilsUnity.Features.StartEndPositionParticleSystems
{
    [RequireComponent(typeof(StartEndPositionParticleSystem))]
    public sealed class BurstParticleSystemPlayer : MonoBehaviour
    {
        [Header("References")]
        public StartEndPositionParticleSystem ParticleSystem;
        public Transform StartPosition;
        public Transform EndPosition;

        [Header("Values")]
        [Min(0)] public float Amount = 10;
        [Min(0)] public float DelayBetween = 0.1f;

        public void Play()
        {
            object GetUserData(int particleIndex) => Nothing.Instance;

            Play(GetUserData);
        }

        public void Play(BurstParticleSystemPlayDelegate getUserData)
        {
            if (ParticleSystem == null)
            {
                return;
            }

            if (StartPosition == null)
            {
                return;
            }

            if (EndPosition == null)
            {
                return;
            }

            float currentDelay = 0f;

            for (int i = 0; i < Amount; i++)
            {
                object userData = getUserData.Invoke(i);

                ParticleSystem.PlayParticle(
                    StartPosition.transform.position,
                    EndPosition.transform.position,
                    currentDelay,
                    userData
                );

                currentDelay += DelayBetween;
            }
        }
    }
}
