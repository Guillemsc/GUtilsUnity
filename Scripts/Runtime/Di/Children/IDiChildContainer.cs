using System;
using System.Collections.Generic;
using GUtilsUnity.Di.Builder;
using GUtilsUnity.Di.Container;
using GUtilsUnity.Di.Delegates;
using GUtilsUnity.Di.Installers;

namespace GUtilsUnity.Di.Children
{
    [Obsolete("Use IDiChildContainerBuilder.Bind(id) instead")]
    public interface IDiChildContainer : IDiResolveContainer
    {
        IDiBindingBuilder<T> Bind<T>();
        IDiBindingBuilder<TConcrete> Bind<TInterface, TConcrete>() where TConcrete : TInterface;

        [Obsolete("Use Install instead")]
        IDiContainerBuilder Bind<T>(BindingBuilderDelegate<T> bindingBuilderDelegate);


        [Obsolete("Use Install instead")]
        IDiContainerBuilder Bind(params IInstaller[] installers);

        [Obsolete("Use Install instead")]
        IDiContainerBuilder Bind(IReadOnlyList<IInstaller> container);

        [Obsolete("Use Install instead")]
        IDiContainerBuilder Bind(Action<IDiContainerBuilder> action);

        IDiContainerBuilder Install(params IInstaller[] installers);
        IDiContainerBuilder Install(IReadOnlyList<IInstaller> installers);
        IDiContainerBuilder Install(Action<IDiContainerBuilder> action);

        IDiContainerBuilder WhenInit(Action action);
        IDiContainerBuilder WhenInit(Action<IDiResolveContainer> action);
        IDiContainerBuilder WhenDispose(Action action);
        IDiContainerBuilder WhenDispose(Action<IDiResolveContainer> action);
    }
}
