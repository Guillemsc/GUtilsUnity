using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Delegates.Animation;
using GUtilsUnity.Resetting.Resetables;
using GUtilsUnity.Tutorial.Events;

namespace GUtilsUnity.Tutorial.Steps
{
    public interface ITutorialStep : IResetable
    {
        bool Completed { get; }

        IReadOnlyList<Action> WhenFinish { get; }
        IReadOnlyList<TaskAnimationEvent> WhenFinishAnimated { get; }

        Task Start(CancellationToken cancellationToken);
        void Receive(ITutorialEvent tutorialEvent);
    }
}
