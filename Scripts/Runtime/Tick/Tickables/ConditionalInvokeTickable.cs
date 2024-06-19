using System;

namespace GUtilsUnity.Tick.Tickables
{
    /// <summary>
    /// Represents a tickable object that conditionally invokes an action based on a specified condition.
    /// </summary>
    public sealed class ConditionalInvokeTickable : ITickable
    {
        readonly Func<bool> _condition;
        readonly Action _action;

        public ConditionalInvokeTickable(Func<bool> condition, Action action)
        {
            _condition = condition;
            _action = action;
        }

        public void Tick()
        {
            if (_condition.Invoke())
            {
                _action.Invoke();
            }
        }
    }
}
