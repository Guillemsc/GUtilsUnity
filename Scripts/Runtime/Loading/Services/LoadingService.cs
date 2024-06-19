﻿using System.Collections.Generic;
using GUtilsUnity.Delegates.Animation;
using GUtilsUnity.Loading.Contexts;
using GUtilsUnity.Sequencing.Sequencer;

namespace GUtilsUnity.Loading.Services
{
    /// <inheritdoc />
    public class LoadingService : ILoadingService
    {
        readonly ISequencer _sequencer = new Sequencer();

        readonly List<TaskAnimationEvent> _beforeLoad = new();
        readonly List<TaskAnimationEvent> _afterLoad = new();

        public bool IsLoading => _sequencer.IsRunning;

        public void AddAfterLoading(TaskAnimationEvent func)
        {
            _afterLoad.Add(func);
        }

        public void AddBeforeLoading(TaskAnimationEvent func)
        {
            _beforeLoad.Add(func);
        }

        public ILoadingContext New()
        {
            return new LoadingContext(
                _sequencer,
                _beforeLoad,
                _afterLoad
            );
        }
    }
}
