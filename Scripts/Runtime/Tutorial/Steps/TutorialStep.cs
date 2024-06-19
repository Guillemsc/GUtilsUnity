using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Delegates.Animation;
using GUtilsUnity.Tutorial.Events;

namespace GUtilsUnity.Tutorial.Steps
{
    public abstract class TutorialStep : ITutorialStep
    {
        readonly List<Action> _whenFinish = new();
        readonly List<TaskAnimationEvent> _whenFinishAnimated = new();

        public bool Completed { get; private set; }

        public IReadOnlyList<Action> WhenFinish => _whenFinish;
        public IReadOnlyList<TaskAnimationEvent> WhenFinishAnimated => _whenFinishAnimated;

        public void Reset()
        {
            Completed = false;
            _whenFinish.Clear();
            _whenFinishAnimated.Clear();
        }

        protected void Complete()
        {
            Completed = true;
        }

        protected void AddWhenFinish(Action action)
        {
            _whenFinish.Add(action);
        }

        protected void AddWhenFinish(TaskAnimationEvent taskAnimationEvent)
        {
            _whenFinishAnimated.Add(taskAnimationEvent);
        }

        public abstract Task Start(CancellationToken cancellationToken);

        [Obsolete("Do not use. Inject events directly")]
        public virtual void Receive(ITutorialEvent tutorialEvent) {}
    }
}
