using GUtils.Di.Builder;
using GUtils.Di.Installers;

namespace GUtilsUnity.Di.Installers
{
    public abstract class ScriptableObjectInstaller : IInstaller
    {
        public abstract void Install(IDiContainerBuilder b);
    }
}
