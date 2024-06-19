using GUtilsUnity.Di.Builder;
using UnityEngine;

namespace GUtilsUnity.Di.Installers
{
    public abstract class MonoBehaviourInstaller : MonoBehaviour, IInstaller
    {
        public abstract void Install(IDiContainerBuilder builder);
    }
}
