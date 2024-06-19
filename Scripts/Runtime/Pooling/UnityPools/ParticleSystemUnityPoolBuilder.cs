using GUtilsUnity.ParticleSystems.Callbacks;
using GUtilsUnity.Extensions;
using UnityEngine;
using UnityEngine.Pool;

namespace GUtilsUnity.Pooling.UnityPools
{
    // TODO: Is this really a builder? Review
    public sealed class ParticleSystemUnityPoolBuilder
    {
        readonly ParticleSystem _prefab;
        readonly Transform _parent;

        public ParticleSystemUnityPoolBuilder(
            ParticleSystem prefab,
            Transform parent
            )
        {
            _prefab = prefab;
            _parent = parent;
        }

        public ObjectPool<ParticleSystem> Build()
        {
            ObjectPool<ParticleSystem> pool = null;

            pool = new(
                Create,
                OnGet,
                OnRelease
            );

            ParticleSystem Create()
            {
                ParticleSystem particleSystem = Object.Instantiate(_prefab, _parent);
                particleSystem.gameObject.SetActive(false);

                ParticleSystem.MainModule main = particleSystem.main;
                main.playOnAwake = false;

                return particleSystem;
            }

            void OnParticleSystemStopped(ParticleSystem particleSystem)
            {
                ParticleSystemStoppedCallback stoppedCallback = particleSystem.gameObject.GetOrAddComponent<ParticleSystemStoppedCallback>();
                stoppedCallback.OnStopped -= OnParticleSystemStopped;

                pool?.Release(particleSystem);
            }

            void OnGet(ParticleSystem particleSystem)
            {
                ParticleSystemStoppedCallback stoppedCallback = particleSystem.gameObject.GetOrAddComponent<ParticleSystemStoppedCallback>();
                stoppedCallback.System = particleSystem;
                stoppedCallback.OnStopped += OnParticleSystemStopped;

                particleSystem.gameObject.SetActive(true);

                particleSystem.time = 0f;
                particleSystem.Play(true);
            }

            void OnRelease(ParticleSystem particleSystem)
            {
                particleSystem.gameObject.SetActive(false);
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }

            return pool;
        }
    }
}
