using GUtilsUnity.Di.Builder;

namespace GUtilsUnity.Di.Installers
{
    public sealed class NopInstaller : IInstaller
    {
        public static readonly NopInstaller Instance = new();

        NopInstaller()
        {

        }

        public void Install(IDiContainerBuilder builder) { }
    }
}
