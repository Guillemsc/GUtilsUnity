using System;
using UnityEngine;

namespace GUtilsUnity.ParticleSystems.Callbacks
{
    /// <summary>
    /// A component that provides a callback when a ParticleSystem stops playing.
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public sealed class ParticleSystemStoppedCallback : MonoBehaviour
    {
        public ParticleSystem System;

        public event Action<ParticleSystem> OnStopped;

        void Start()
        {
            if (System == null)
            {
                System = GetComponent<ParticleSystem>();

                if (System == null)
                {
                    return;
                }
            }

            ParticleSystem.MainModule main = System.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        void OnParticleSystemStopped()
        {
            if (System == null)
            {
                return;
            }

            OnStopped?.Invoke(System);
        }
    }
}
