using GUtilsUnity.Di.Builder;
using GUtilsUnity.Disposing.Disposables;

namespace GUtilsUnity.Di.Installers
{
    public interface IInstaller
    {
        void Install(IDiContainerBuilder builder);
    }
}
