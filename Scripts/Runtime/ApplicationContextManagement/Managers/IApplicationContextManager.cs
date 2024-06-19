using System;
using System.Collections.Generic;
using GUtilsUnity.Events;

namespace GUtilsUnity.ApplicationContextManagement
{
    // TODO: Add documentation :)
    public interface IApplicationContextService
    {
        public IListenEvent<EventArgs> OnBeginContextChange { get; }
        public IListenEvent<EventArgs> OnEndContextChange { get; }

        IApplicationContextChangeHandle Push(IApplicationContext applicationContext);
        IApplicationContextChangeHandle Push(IEnumerable<IApplicationContext> applicationContexts);

        IApplicationContextChangeHandle Pop();
        IApplicationContextChangeHandle PopUntil<T>()  where T : IApplicationContext;
        IApplicationContextChangeHandle PopUntilThenPush<T>(IApplicationContext applicationContext) where T : IApplicationContext;
        IApplicationContextChangeHandle PopUntilThenPush<T>(IEnumerable<IApplicationContext> applicationContexts) where T : IApplicationContext;

        bool TryGetNearest<T>(out T applicationContext) where T : IApplicationContext;
        T GetNearest<T>() where T : IApplicationContext;
        bool Contains<T>() where T : IApplicationContext;
    }
}
