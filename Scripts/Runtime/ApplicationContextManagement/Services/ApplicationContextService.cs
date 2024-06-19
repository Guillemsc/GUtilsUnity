using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GUtilsUnity.Events;
using GUtilsUnity.Tasks.Runners;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.ApplicationContextManagement
{
    /// <inheritdoc />
    public sealed class ApplicationContextService : IApplicationContextService
    {
        readonly AsyncSequenceTaskRunner _asyncSequenceTaskRunner = new();
        readonly Stack<IApplicationContext> _applicationContexts = new();

        public IListenEvent<EventArgs> OnBeginContextChange => _asyncSequenceTaskRunner.OnBeginRunning;
        public IListenEvent<EventArgs> OnEndContextChange => _asyncSequenceTaskRunner.OnEndRunning;

        public IApplicationContextChangeHandle Push(IApplicationContext applicationContext)
        {
            var applicationContextChangeHandle = new ApplicationContextChangeHandle();
            var task = _asyncSequenceTaskRunner.RunGetCompleteTask(
                ct => HandledPush(applicationContext, applicationContextChangeHandle));
            applicationContextChangeHandle.SetFullTask(task);
            return applicationContextChangeHandle;
        }

        async Task HandledPush(IApplicationContext applicationContext, ApplicationContextChangeHandle applicationContextChangeHandle)
        {
            if (_applicationContexts.Count > 0)
            {
                if (_applicationContexts.Peek() is IApplicationContextResumable resumable)
                {
                    await resumable.Suspend();
                }
            }

            _applicationContexts.Push(applicationContext);
            await EnterInternal(applicationContext, applicationContextChangeHandle);
        }

        public IApplicationContextChangeHandle Push(IEnumerable<IApplicationContext> applicationContexts)
        {
            var applicationContextChangeHandle = new ApplicationContextChangeHandle();
            var task = _asyncSequenceTaskRunner.RunGetCompleteTask(
                ct => HandledPush(applicationContexts, applicationContextChangeHandle));
            applicationContextChangeHandle.SetFullTask(task);
            return applicationContextChangeHandle;
        }

        async Task HandledPush(IEnumerable<IApplicationContext> applicationContexts, ApplicationContextChangeHandle applicationContextChangeHandle)
        {
            foreach ((var applicationContext, var isLast) in applicationContexts.ZipIsLast())
            {
                var iApplicationContextChangeHandle = isLast
                    ? applicationContextChangeHandle
                    : null;

                await HandledPush(applicationContext, iApplicationContextChangeHandle);
            }
        }

        bool DoesTypeMatch(Type queryApplicationContextType, IApplicationContext applicationContext)
        {
            var concreteContextType = applicationContext.GetType();
            return queryApplicationContextType.IsAssignableFrom(concreteContextType);
        }

        public IApplicationContextChangeHandle Pop()
        {
            var applicationContextChangeHandle = new ApplicationContextChangeHandle();
            var task = _asyncSequenceTaskRunner.RunGetCompleteTask(
                ct => HandledPop(applicationContextChangeHandle));
            applicationContextChangeHandle.SetFullTask(task);
            return applicationContextChangeHandle;
        }

        async Task HandledPop(ApplicationContextChangeHandle applicationContextChangeHandle)
        {
            await PopInternal();

            if (_applicationContexts.Count > 0)
            {
                await ResumeInternal(applicationContextChangeHandle);
            }
        }

        public IApplicationContextChangeHandle PopUntil<T>()
            where T : IApplicationContext
        {
            var applicationContextChangeHandle = new ApplicationContextChangeHandle();
            var task = _asyncSequenceTaskRunner.RunGetCompleteTask(
                ct => HandledPopUntil<T>(applicationContextChangeHandle));
            applicationContextChangeHandle.SetFullTask(task);
            return applicationContextChangeHandle;
        }

        async Task HandledPopUntil<T>(ApplicationContextChangeHandle applicationContextChangeHandle)
        {
            var type = typeof(T);
            while (!DoesTypeMatch(type, _applicationContexts.Peek()))
            {
                await PopInternal();
            }

            await ResumeInternal(applicationContextChangeHandle);
        }

        public IApplicationContextChangeHandle PopUntilThenPush<T>(IApplicationContext applicationContext) where T : IApplicationContext
        {
            var applicationContextChangeHandle = new ApplicationContextChangeHandle();
            var task = _asyncSequenceTaskRunner.RunGetCompleteTask(
                ct => HandlePopUntilThenPush<T>(applicationContext, applicationContextChangeHandle));
            applicationContextChangeHandle.SetFullTask(task);
            return applicationContextChangeHandle;
        }

        public IApplicationContextChangeHandle PopUntilThenPush<T>(IEnumerable<IApplicationContext> applicationContexts) where T : IApplicationContext
        {
            var applicationContextChangeHandle = new ApplicationContextChangeHandle();
            var task = _asyncSequenceTaskRunner.RunGetCompleteTask(
                ct => HandlePopUntilThenPush<T>(applicationContexts, applicationContextChangeHandle));
            applicationContextChangeHandle.SetFullTask(task);
            return applicationContextChangeHandle;
        }

        async Task HandlePopUntilThenPush<T>(
            IEnumerable<IApplicationContext> applicationContexts,
            ApplicationContextChangeHandle applicationContextChangeHandle)
        {
            await HandledPopUntil<T>(null);
            await HandledPush(applicationContexts, applicationContextChangeHandle);
        }

        async Task HandlePopUntilThenPush<T>(IApplicationContext applicationContext, ApplicationContextChangeHandle applicationContextChangeHandle)
        {
            await HandledPopUntil<T>(null);
            await HandledPush(applicationContext, applicationContextChangeHandle);
        }

        public bool TryGetNearest<T>(out T applicationContext)
            where T : IApplicationContext
        {
            var type = typeof(T);
            foreach (var iApplicationContext in _applicationContexts)
            {
                if (DoesTypeMatch(type, iApplicationContext))
                {
                    applicationContext = (T)iApplicationContext;
                    return true;
                }
            }

            applicationContext = default;
            return false;
        }

        public T GetNearest<T>()
            where T : IApplicationContext
        {
            if (!TryGetNearest<T>(out var applicationContext))
            {
                throw new InvalidOperationException($"Could not get application context of type {typeof(T).FullName}");
            }

            return applicationContext;
        }

        public bool Contains<T>() where T : IApplicationContext
        {
            return TryGetNearest<T>(out _);
        }

        async Task EnterInternal(IApplicationContext applicationContext, ApplicationContextChangeHandle applicationContextChangeHandle)
        {
            await applicationContext.PreEnter();
            if (applicationContextChangeHandle != null)
            {
                applicationContextChangeHandle.TryFinishPreStep();
                await applicationContextChangeHandle.WaitFinishBlockAsync();
            }
            await applicationContext.Enter();
        }

        async Task PopInternal()
        {
            await _applicationContexts.Peek().Exit();
            _applicationContexts.Pop();
        }

        async Task ResumeInternal(ApplicationContextChangeHandle applicationContextChangeHandle)
        {
            var resumable = _applicationContexts.Peek() as IApplicationContextResumable;
            if (resumable != null)
            {
                await resumable.PreResume();
            }

            if (applicationContextChangeHandle != null)
            {
                applicationContextChangeHandle.TryFinishPreStep();
                await applicationContextChangeHandle.WaitFinishBlockAsync();
            }

            if (resumable != null)
            {
                await resumable.Resume();
            }
        }
    }
}
