using GUtilsUnity.ApplicationContextManagement;
using GUtilsUnity.AutomaticInputBlocking.UseCases;
using GUtilsUnity.Di.Builder;
using GUtilsUnity.UiFrame.Services;
using GUtilsUnity.Events.Extensions;

namespace GUtilsUnity.AutomaticInputBlocking.Installers
{
    public static class AutomaticInputBlockingInstaller
    {
        public static IDiContainerBuilder InstallAutomaticInputBlocking(this IDiContainerBuilder builder)
        {
            object activeHandle = new object();

            builder.Bind<DisableInputOnBeginApplicationContextChangeUseCase>()
                .FromFunction(c => new DisableInputOnBeginApplicationContextChangeUseCase(
                    c.Resolve<IUiFrameService>(),
                    activeHandle))
                .LinkEvent(
                    c => c.Resolve<IApplicationContextService>().OnBeginContextChange,
                    o => arg => o.Execute()
                );

            builder.Bind<EnableInputOnEndApplicationContextChangeUseCase>()
                .FromFunction(c => new EnableInputOnEndApplicationContextChangeUseCase(
                    c.Resolve<IUiFrameService>(),
                    activeHandle
                    ))
                .LinkEvent(
                    c => c.Resolve<IApplicationContextService>().OnEndContextChange,
                    o => arg => o.Execute()
                );

            return builder;
        }
    }
}
