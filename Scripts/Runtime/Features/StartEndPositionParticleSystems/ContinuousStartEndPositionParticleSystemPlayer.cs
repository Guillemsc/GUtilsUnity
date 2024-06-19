using UnityEngine;

namespace GUtilsUnity.Features.StartEndPositionParticleSystems
{
    [RequireComponent(typeof(StartEndPositionParticleSystem))]
    public sealed class ContinuousStartEndPositionParticleSystemPlayer : MonoBehaviour
    {
        [Header("References")]
        public StartEndPositionParticleSystem ParticleSystem;
        public Transform StartPosition;
        public Transform EndPosition;

        [Header("Values")]
        public bool EmissionEnabled;
        [Min(0)] public float RateOverTime = 10;

        float _currentRate;

        void Update()
        {
            TryTick();
        }

        void TryTick()
        {
            if (!EmissionEnabled)
            {
                return;
            }

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

            float rateOverTimeDelta = RateOverTime * UnityEngine.Time.deltaTime;

            _currentRate += rateOverTimeDelta;

            while (_currentRate > 1)
            {
                _currentRate -= 1;

                ParticleSystem.PlayParticle(
                    StartPosition.transform.position,
                    EndPosition.transform.position
                );
            }
        }
    }
}
