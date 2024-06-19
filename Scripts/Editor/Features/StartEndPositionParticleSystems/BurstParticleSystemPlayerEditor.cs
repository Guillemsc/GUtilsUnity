using UnityEditor;
using UnityEngine;

namespace GUtilsUnity.Features.StartEndPositionParticleSystems
{
    [CustomEditor(typeof(BurstParticleSystemPlayer))]
    public sealed class BurstParticleSystemPlayerEditor : Editor
    {
        BurstParticleSystemPlayer _actualTarget => (BurstParticleSystemPlayer)target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
            {
                return;
            }

            if (GUILayout.Button("Play"))
            {
                _actualTarget.Play();
            }
        }
    }
}
