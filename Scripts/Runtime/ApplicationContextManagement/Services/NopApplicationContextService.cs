using System;
using System.Collections.Generic;
using GUtilsUnity.Events;

namespace GUtilsUnity.ApplicationContextManagement
{
    public sealed class NopApplicationContextService : IApplicationContextService
    {
        public static readonly NopApplicationContextService Instance = new();

        NopApplicationContextService()
        {

        }

        public IListenEvent<EventArgs> OnBeginContextChange { get; } = new Event<EventArgs>();
        public IListenEvent<EventArgs> OnEndContextChange { get; } = new Event<EventArgs>();
        public IApplicationContextChangeHandle Push(IApplicationContext applicationContext) => NopApplicationContextChangeHandle.Instance;
        public IApplicationContextChangeHandle Push(IEnumerable<IApplicationContext> applicationContexts) => NopApplicationContextChangeHandle.Instance;
        public IApplicationContextChangeHandle Pop() => NopApplicationContextChangeHandle.Instance;
        public IApplicationContextChangeHandle PopUntil<T>() where T : IApplicationContext => NopApplicationContextChangeHandle.Instance;
        public IApplicationContextChangeHandle PopUntilThenPush<T>(IApplicationContext applicationContext) where T : IApplicationContext
            => NopApplicationContextChangeHandle.Instance;
        public IApplicationContextChangeHandle PopUntilThenPush<T>(IEnumerable<IApplicationContext> applicationContexts) where T : IApplicationContext
            => NopApplicationContextChangeHandle.Instance;
        public bool TryGetNearest<T>(out T applicationContext) where T : IApplicationContext
        {
            applicationContext = default;
            return false;
        }
        public T GetNearest<T>() where T : IApplicationContext => default;
        public bool Contains<T>() where T : IApplicationContext => false;
    }
}
