using System;
using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.UiStack.Builder
{
    public sealed class NopUiStackSequenceBuilder : IUiStackSequenceBuilder
    {
        public static readonly NopUiStackSequenceBuilder Instance = new();

        NopUiStackSequenceBuilder()
        {
        }

        public IUiStackSequenceBuilder Show<T>(T instance, bool instantly = false) => this;
        public IUiStackSequenceBuilder HideAndPush<T>(T instance, bool instantly = false) => this;
        public IUiStackSequenceBuilder Hide<T>(T instance, bool instantly = false) => this;
        public IUiStackSequenceBuilder HideCurrent(bool instantly) => this;
        public IUiStackSequenceBuilder HideAllPopups(bool instantly = false) => this;
        public IUiStackSequenceBuilder ShowLast(bool instantly) => this;
        public IUiStackSequenceBuilder ShowLastBehindForeground(bool instantly) => this;
        public IUiStackSequenceBuilder MoveToBackground<T>(T instance) => this;
        public IUiStackSequenceBuilder MoveToForeground<T>(T instance) => this;
        public IUiStackSequenceBuilder MoveCurrentToForeground() => this;
        public IUiStackSequenceBuilder SetInteractable<T>(T instance, bool set) => this;
        public IUiStackSequenceBuilder CurrentSetInteractable(bool set) => this;
        public IUiStackSequenceBuilder Callback(Action callback) => this;
        public Task Execute(CancellationToken cancellationToken) => Task.CompletedTask;

        public void Execute()
        {
        }
    }
}
