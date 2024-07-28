using GUtils.Di.Builder;
using GUtils.Di.Installers;
using UnityEngine;

namespace GUtilsUnity.Di.Installers
{
    public abstract class MonoBehaviourInstaller : MonoBehaviour, IInstaller
    {
        public abstract void Install(IDiContainerBuilder builder);
    }
}
