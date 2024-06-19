using GUtilsUnity.Disposing.Disposables;

namespace GUtilsUnity.Loadables
{
    public interface ILoadable<out T>
    {
        IDisposable<T> Load();
    }
}
