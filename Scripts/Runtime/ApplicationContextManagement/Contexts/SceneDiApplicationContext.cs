using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Di.Contexts;
using GUtilsUnity.Di.Installers;
using GUtilsUnity.Disposing.Disposables;
using GUtilsUnity.Starting.Startables;
using GUtilsUnity.Di.Extensions;
using GUtils.Loading.Loadables;

namespace GUtilsUnity.ApplicationContextManagement
{
    /// <summary>
    /// Base ApplicationContext based on the SceneDiContext.
    /// It loads a scene with the scene name, and tries to get a MonoBehaviourInstaller from its root GameObjects.
    /// This MonoBehaviourInstaller is then installed in conjunction with everything else installes on the Install
    /// abstract method provided in this class.
    /// </summary>
    public abstract class SceneDiApplicationContext<TResult, TMonoBehaviourInstance> : IApplicationContext
        where TResult : ILoadable, IStartable
        where TMonoBehaviourInstance : MonoBehaviourInstaller
    {
        readonly IAsyncDiContext<TResult> _diContext;
        ITaskDisposable<TResult> _result;

        protected SceneDiApplicationContext(
            string sceneName,
            bool setAsActiveScene = false
        )
        {
            _diContext = new AsyncDiContext<TResult>()
                .AddSceneInstaller(sceneName, setAsActiveScene);
        }

        public async Task PreEnter()
        {
            _result = await _diContext.Install();
            await _result.Value.Load(CancellationToken.None);
        }

        public Task Enter()
        {
            _result.Value.Start();
            return Task.CompletedTask;
        }

        public Task Exit()
        {
            return _result.Dispose();
        }
    }
}
