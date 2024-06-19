using GUtilsUnity.Di.Builder;

namespace GUtilsUnity.Di.Installers
{
    public abstract class ScriptableObjectInstaller : IInstaller
    {
        public abstract void Install(IDiContainerBuilder b);
    }
}
