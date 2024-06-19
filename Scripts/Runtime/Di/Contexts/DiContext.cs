using System;
using System.Collections.Generic;
using GUtilsUnity.Di.Builder;
using GUtilsUnity.Di.Container;
using GUtilsUnity.Di.Delegates;
using GUtilsUnity.Di.Installers;
using GUtilsUnity.Disposing.Disposables;
using GUtilsUnity.Loadables;

namespace GUtilsUnity.Di.Contexts
{
    public sealed class DiContext<TResult> : IDiContext<TResult>
    {
        readonly List<Action<List<IInstaller>, List<IDisposable>>> _loadableSettings = new();
        readonly List<ILoadable<IInstaller>> _loadableInstallers = new();
        readonly List<IInstaller> _installers = new();

        bool hasValidContainer;
        IDiContainer _container;

        [Obsolete("Use with installer method")]
        public DiContext(params IInstaller[] installers)
            : this((IReadOnlyList<IInstaller>)installers)
        {
        }

        [Obsolete("Use with installer method")]
        public DiContext(Action<IDiContainerBuilder> installDelegate)
            : this(new CallbackInstaller(installDelegate))
        {
        }

        [Obsolete("Use Add installers method")]
        public DiContext(IReadOnlyList<IInstaller> installers)
        {
            AddInstallers(installers);
        }

        public DiContext()
        {
        }

        public IDiContext<TResult> AddSettingLoadable<T>(ILoadable<T> loadable)
        {
            void CreateInstaller(List<IInstaller> installers, List<IDisposable> disposables)
            {
                var value = loadable.Load();

                installers.Add(new CallbackInstaller(b => b.AddSettings(value.Value)));
                disposables.Add(value);
            }

            _loadableSettings.Add(CreateInstaller);
            return this;
        }

        public IDiContext<TResult> AddInstallerLoadable(ILoadable<IInstaller> loadable)
        {
            _loadableInstallers.Add(loadable);
            return this;
        }

        public IDiContext<TResult> AddInstaller(IInstaller installer)
        {
            _installers.Add(installer);
            return this;
        }

        public IDiContext<TResult> AddInstaller(InstallDelegate installer)
        {
            _installers.Add(new CallbackInstaller(installer.Invoke));
            return this;
        }

        public IDiContext<TResult> AddInstallers(IReadOnlyList<IInstaller> installers)
        {
            _installers.AddRange(installers);
            return this;
        }

        public IDisposable<TResult> Install()
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            var disposables = new List<IDisposable>();

            foreach (var installer in _loadableInstallers)
            {
                var disposable = installer.Load();
                _installers.Add(disposable.Value);
                disposables.Add(disposable);
            }

            foreach (var loadableSetting in _loadableSettings)
            {
                loadableSetting.Invoke(_installers, disposables);
            }

            builder.Install(_installers);

            _container = builder.Build();

            TResult result = _container.Resolve<TResult>();

            hasValidContainer = true;

            return new CallbackDisposable<TResult>(
                result,
                x =>
                {
                    hasValidContainer = false;
                    _container.Dispose();

                    foreach (var disposable in disposables)
                    {
                        disposable.Dispose();
                    }
                }
            );
        }

        public IDiContainer GetContainerUnsafe()
        {
            if (!hasValidContainer)
            {
                throw new AccessViolationException("Tried to get container but it was not created or already disposed");
            }

            return _container;
        }
    }
}
