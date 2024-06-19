using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GUtilsUnity.Tutorial.Events;
using GUtilsUnity.Tutorial.Steps;

namespace GUtilsUnity.Tutorial.Player
{
    public interface ITutorialPlayer
    {
        Task Play(params ITutorialStep[] tutorialSteps);
        Task Play(IReadOnlyList<ITutorialStep> tutorialSteps);
        Task Play();
        Task Stop(bool instantly);

        [Obsolete("Do not use. Inject events directly on the tutorial step")]
        void SendEvent(ITutorialEvent tutorialEvent);
    }
}
