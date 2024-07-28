using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtils.Extensions;
using GUtils.Tasks.Sequencing.Instructions;
using GUtils.Tasks.Sequencing.Sequencer;
using GUtilsUnity.Repositories;
using GUtilsUnity.UiFrame.Services;
using GUtilsUnity.UiStack.Entries;
using GUtilsUnity.UiStack.Instructions;
using GUtilsUnity.UiStack.UseCases;

namespace GUtilsUnity.UiStack.Builder
{
    /// <inheritdoc />
    public sealed class UiStackSequenceBuilder : IUiStackSequenceBuilder
    {
        readonly List<IInstruction> _instructionsToPlay = new();

        readonly IUiFrameService _uiFrameService;
        readonly IKeyValueRepository<object, UiStackEntry> _entriesRepository;
        readonly ISingleRepository<object> _currentContextRepository;
        readonly IListRepository<object> _currentPopupsRepository;
        readonly List<object> _viewStack;
        readonly ITaskSequencer _sequencer;
        readonly ShowUseCase _showUseCase;
        readonly HideUseCase _hideUseCase;

        public UiStackSequenceBuilder(
            IUiFrameService uiFrameService,
            IKeyValueRepository<object, UiStackEntry> entriesRepository,
            ISingleRepository<object> currentContextRepository,
            IListRepository<object> currentPopupsRepository,
            List<object> viewStack,
            ITaskSequencer sequencer,
            ShowUseCase showUseCase,
            HideUseCase hideUseCase
            )
        {
            _uiFrameService = uiFrameService;
            _entriesRepository = entriesRepository;
            _currentContextRepository = currentContextRepository;
            _currentPopupsRepository = currentPopupsRepository;
            _viewStack = viewStack;
            _sequencer = sequencer;
            _showUseCase = showUseCase;
            _hideUseCase = hideUseCase;
        }

        public IUiStackSequenceBuilder Show<T>(T instance, bool instantly = false)
        {
            _instructionsToPlay.Add(new ShowInstruction(
                instance,
                instantly,
                _showUseCase
            ));

            return this;
        }

        public IUiStackSequenceBuilder HideAndPush<T>(T instance, bool instantly = false)
        {
            _instructionsToPlay.Add(new HideInstruction(
                instance,
                pushToViewQueue: true,
                instantly,
                _hideUseCase
            ));

            return this;
        }

        public IUiStackSequenceBuilder Hide<T>(T instance, bool instantly = false)
        {
            _instructionsToPlay.Add(new HideInstruction(
                instance,
                pushToViewQueue: false,
                instantly,
                _hideUseCase
            ));

            return this;
        }

        public IUiStackSequenceBuilder HideCurrent(bool instantly)
        {
            _instructionsToPlay.Add(new HideCurrentInstruction(
                _currentContextRepository,
                pushToViewQueue: false,
                instantly,
                _hideUseCase
            ));

            return this;
        }

        public IUiStackSequenceBuilder HideAllPopups(bool instantly = false)
        {
            _instructionsToPlay.Add(new HideAllPopupsInstruction(
                _currentPopupsRepository,
                instantly,
                _hideUseCase
            ));
            return this;
        }

        public IUiStackSequenceBuilder ShowLast(bool instantly)
        {
            _instructionsToPlay.Add(new ShowLastInstruction(
                _viewStack,
                behindForeground: false,
                instantly,
                _showUseCase
            ));

            return this;
        }

        public IUiStackSequenceBuilder ShowLastBehindForeground(bool instantly)
        {
            _instructionsToPlay.Add(new ShowLastInstruction(
                _viewStack,
                behindForeground: true,
                instantly,
                _showUseCase
            ));

            return this;
        }

        public IUiStackSequenceBuilder MoveToBackground<T>(T instance)
        {
            _instructionsToPlay.Add(new MoveToBackgroundInstruction(
                _uiFrameService,
                _entriesRepository,
                instance
                ));

            return this;
        }

        public IUiStackSequenceBuilder MoveToForeground<T>(T instance)
        {
            _instructionsToPlay.Add(new MoveToForegroundInstruction(
                _uiFrameService,
                _entriesRepository,
                instance
            ));

            return this;
        }

        public IUiStackSequenceBuilder MoveCurrentToForeground()
        {
            _instructionsToPlay.Add(new MoveCurrentToForegroundInstruction(
                _uiFrameService,
                _entriesRepository,
                _currentContextRepository
                ));

            return this;
        }

        public IUiStackSequenceBuilder SetInteractable<T>(T instance, bool set)
        {
            _instructionsToPlay.Add(new SetInteractableInstruction(
                _uiFrameService,
                _entriesRepository,
                instance,
                set
                ));

            return this;
        }

        public IUiStackSequenceBuilder CurrentSetInteractable(bool set)
        {
            _instructionsToPlay.Add(new CurrentSetInteractableInstruction(
                _entriesRepository,
                _currentContextRepository,
                set
                ));

            return this;
        }

        public IUiStackSequenceBuilder Callback(Action callback)
        {
            _instructionsToPlay.Add(new ActionInstruction(callback));

            return this;
        }

        public Task Execute(CancellationToken cancellationToken)
        {
            foreach(IInstruction instruction in _instructionsToPlay)
            {
                _sequencer.Play(instruction.Execute);
            }

            cancellationToken.Register(_sequencer.Kill);

            return _sequencer.AwaitCompletition(cancellationToken);
        }

        public void Execute()
        {
            Execute(default).RunAsync();
        }
    }
}
