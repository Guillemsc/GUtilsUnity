using System.Collections.Generic;
using System.Threading.Tasks;
using GUtilsUnity.Di.Container;
using GUtilsUnity.Di.Delegates;
using GUtilsUnity.Di.Installers;
using GUtilsUnity.Disposing.Disposables;
using GUtilsUnity.Loadables;

namespace GUtilsUnity.Di.Contexts
{
    public interface IAsyncDiContext<TResult>
    {
        IAsyncDiContext<TResult> AddInstallerAsyncLoadable(IAsyncLoadable<IInstaller> asyncLoadable);
        IAsyncDiContext<TResult> AddInstallerLoadable(ILoadable<IInstaller> asyncLoadable);
        IAsyncDiContext<TResult> AddInstaller(IInstaller installer);
        IAsyncDiContext<TResult> AddInstallers(IReadOnlyList<IInstaller> installers);
        IAsyncDiContext<TResult> AddInstaller(InstallDelegate installer);

        Task<ITaskDisposable<TResult>> Install();

        IDiContainer GetContainerUnsafe();
    }
}
