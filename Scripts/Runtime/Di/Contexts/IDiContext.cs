using System.Collections.Generic;
using GUtilsUnity.Di.Delegates;
using GUtilsUnity.Di.Installers;
using GUtilsUnity.Disposing.Disposables;
using GUtilsUnity.Loadables;

namespace GUtilsUnity.Di.Contexts
{
    public interface IDiContext<out TResult>
    {
        IDiContext<TResult> AddSettingLoadable<T>(ILoadable<T> loadable);
        IDiContext<TResult> AddInstallerLoadable(ILoadable<IInstaller> loadable);
        IDiContext<TResult> AddInstaller(IInstaller installer);
        IDiContext<TResult> AddInstaller(InstallDelegate installer);
        IDiContext<TResult> AddInstallers(IReadOnlyList<IInstaller> installers);

        IDisposable<TResult> Install();
    }
}
