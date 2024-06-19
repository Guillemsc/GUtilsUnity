using NUnit.Framework;
using System;
using GUtilsUnity.Di.Builder;
using GUtilsUnity.Di.Container;
using GUtilsUnity.Di.Data;

namespace GUtilsUnity.Di
{
    public sealed class TestDiBuilder
    {
        [Test]
        public void DiBuilderTests_FromNew()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            IDiContainer container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DiBuilderTests_FromNewWithId()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>(this).FromNew();

            IDiContainer container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>(this);

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DiBuilderTests_FromInstance()
        {
            Class1Test classTest = new Class1Test();

            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromInstance(classTest);

            IDiContainer container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DiBuilderTests_FromFunction()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromFunction(_ => new Class1Test());

            IDiContainer container = containerBuilder.Build();

            Class1Test class1Test = container.Resolve<Class1Test>();

            Assert.IsNotNull(class1Test);
        }

        [Test]
        public void DiBuilderTests_FromFunctionLateResolve()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class2Test>()
                .FromFunction((c) => new Class2Test(
                    c.Resolve<Class1Test>()
                    ));

            IDiContainer container = containerBuilder.Build();

            Class2Test class2Test = container.Resolve<Class2Test>();

            Assert.IsNotNull(class2Test);
        }

        [Test]
        public void MultipleBindingsWithNoId_HaveCircularDependency_Assert()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class5Test>()
                .FromFunction((x) => new Class5Test(
                    x.Resolve<Class6Test>()
                    ));

            containerBuilder.Bind<Class6Test>()
                .FromFunction((x) => new Class6Test(
                    x.Resolve<Class5Test>()
                    ));

            IDiContainer container = containerBuilder.Build();

            Assert.Throws<Exception>(() => container.Resolve<Class5Test>());
        }

        [Test]
        public void DiBuilderTests_SimpleInit()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenInit((o) => o.Init);

            IDiContainer container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            Assert.IsNotNull(class3Test);
        }

        [Test]
        public void DiBuilderTests_LateResolveInit()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenInit((c, o) => o.Init(
                    c.Resolve<Class1Test>()
                ));

            IDiContainer container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            Assert.IsNotNull(class3Test);
            Assert.IsNotNull(class3Test.Class1Test);
        }

        [Test]
        public void MultipleCircularBindingsUsingInit_Resolve_DoesNotFail()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class7Test>()
                .FromFunction(c => new Class7Test(
                    c.Resolve<Class8Test>())
                ).NonLazy();

            containerBuilder.Bind<Class8Test>()
                .FromFunction(c => new Class8Test())
                .WhenInit((c, o) => o.Init(c.Resolve<Class7Test>()));

            containerBuilder.Build();
        }

        [Test]
        public void DiBuilderTests_SimpleDispose()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            containerBuilder.Bind<Class3Test>()
                .FromNew()
                .WhenDispose((o) => o.Dispose());

            IDiContainer container = containerBuilder.Build();

            Class3Test class3Test = container.Resolve<Class3Test>();

            container.Dispose();

            Assert.IsNotNull(class3Test);
            Assert.IsTrue(class3Test.Disposed);
        }

        [Test]
        public void DiBuilderTests_AssertResolve()
        {
            IDiContainer container = new DiContainerBuilder().Build();

            Assert.Throws<Exception>(() => container.Resolve<Class1Test>());
        }

        [Test]
        public void DiBuilderTests_InterfaceBind()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<IClass1Test, Class1Test>().FromNew();

            IDiContainer container = containerBuilder.Build();

            IClass1Test class1Test = container.Resolve<IClass1Test>();

            Assert.NotNull(class1Test);
        }

        [Test]
        public void DiBuilderTests_AssertDuplicateBind()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();

            Assert.Throws<Exception>(() => containerBuilder.Bind<Class1Test>().FromNew());
        }

        [Test]
        public void DiBuilderTests_BindWithIdNonLazyInitWorks()
        {
            bool inited = false;

            IDiContainerBuilder containerBuilder = new DiContainerBuilder();
            containerBuilder.Bind<Class1Test>(this).FromNew()
                .WhenInit(() => inited = true)
                .NonLazy();
            containerBuilder.Build();

            Assert.IsTrue(inited);
        }

        [Test]
        public void DiBuilderTests_BindWithIdDisposeWorks()
        {
            bool disposed = false;

            IDiContainerBuilder containerBuilder = new DiContainerBuilder();
            containerBuilder.Bind<Class1Test>(this).FromNew()
                .WhenDispose(() => disposed = true)
                .NonLazy();
            IDiContainer container = containerBuilder.Build();
            container.Dispose();

            Assert.IsTrue(disposed);
        }

        [Test]
        public void MultipleBindingsAllWithDifferentIds_Resolve_DoesNotFail()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();
            containerBuilder.Bind<Class1Test>(1).FromNew();
            containerBuilder.Bind<Class1Test>(2).FromNew();
            containerBuilder.Bind<Class1Test>(3).FromNew();

            IDiContainer container = containerBuilder.Build();

            Assert.DoesNotThrow(() =>
            {
                container.Resolve<Class1Test>();
                container.Resolve<Class1Test>(1);
                container.Resolve<Class1Test>(2);
                container.Resolve<Class1Test>(3);
            });
        }

        [Test]
        public void SameBindingsWhereOneWithIdAndTheOtherWithout_Resolve_DoesNotFail()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();
            containerBuilder.Bind<Class1Test>(this).FromNew();

            IDiContainer container = containerBuilder.Build();

            Assert.DoesNotThrow(() =>
            {
                container.Resolve<Class1Test>();
                container.Resolve<Class1Test>(this);
            });
        }

        [Test]
        public void MultipleBindingsWhereOneHasIdAndHasADependencyWithOneThatDoesNotHaveId_Resolve_DoesNotFail()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class1Test>().FromNew();
            containerBuilder.Bind<Class2Test>(this)
                .FromFunction(c => new Class2Test(
                    c.Resolve<Class1Test>()
                ));

            IDiContainer container = containerBuilder.Build();

            Assert.DoesNotThrow(() =>
            {
                container.Resolve<Class2Test>(this);
            });
        }

        [Test]
        public void MultipleBindingsOneWithIdAndTheOtherWithout_HaveCircularDependency_Asserts()
        {
            IDiContainerBuilder containerBuilder = new DiContainerBuilder();

            containerBuilder.Bind<Class5Test>(this)
                .FromFunction((x) => new Class5Test(
                    x.Resolve<Class6Test>()
                ));

            containerBuilder.Bind<Class6Test>()
                .FromFunction((x) => new Class6Test(
                    x.Resolve<Class5Test>()
                ));

            IDiContainer container = containerBuilder.Build();

            Assert.Throws<Exception>(() => container.Resolve<Class5Test>(this));
        }
    }
}
