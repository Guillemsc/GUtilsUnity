using NUnit.Framework;

namespace GUtilsUnity.DynamicVisitor.Tests
{
    public class TestDynamicVisitor
    {
        interface IRoot { }
        class ImplA : IRoot { }
        interface IB : IRoot { }
        class ImplB1 : IB { }
        class ImplB2 : IB { }

        [Test]
        public void Execute_OnA_RunsOnce()
        {
            var callByType = new ActionDynamicVisitor<IRoot>();
            var call = 0;
            callByType.Add<ImplA>(x => call++);
            callByType.TryExecute(new ImplA());
            Assert.That(call, Is.EqualTo(1));
        }

        [Test]
        public void Execute_OnB1_RunsOnce()
        {
            var callByType = new ActionDynamicVisitor<IRoot>();
            var call = 0;
            callByType.Add<IB>(x => call++);
            callByType.TryExecute(new ImplB1());
            Assert.That(call, Is.EqualTo(1));
        }

        [Test]
        public void Execute_OnB2_RunsOnce()
        {
            var callByType = new ActionDynamicVisitor<IRoot>();
            var call = 0;
            callByType.Add<IB>(x => call++);
            callByType.TryExecute(new ImplB2());
            Assert.That(call, Is.EqualTo(1));
        }

        [Test]
        public void Execute_OnB2AndB1_RunsProperOnce()
        {
            var callByType = new ActionDynamicVisitor<IRoot>();
            var call = 0;
            callByType.Add<ImplA>(x => {});
            callByType.Add<IB>(x => call++);
            callByType.TryExecute(new ImplB2());
            Assert.That(call, Is.EqualTo(1));
        }

        [Test]
        public void Execute_OnNonAdded_Fails()
        {
            var callByType = new ActionDynamicVisitor<IRoot>();
            var success = callByType.TryExecute(new ImplB2());
            Assert.That(success, Is.False);
        }
    }
}
