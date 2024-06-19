using System;
using System.Collections.Generic;
using GUtilsUnity.Features.ExtendableButtons.ButtonExtensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUtilsUnity.Features.ExtendableButtons.MonoBehaviours
{
    public sealed class ExtendableButton : Button
    {
        public List<ButtonExtension> Extensions = new();

        bool _wasInteractable;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (!Application.isPlaying)
            {
                return;
            }

            ForEachExtension(e => e.WhenEnable());

            var isInteractable = IsInteractable();
            _wasInteractable = isInteractable;
            ForEachExtension(e => e.WhenInteractable(isInteractable));
        }

        protected override void Start()
        {
            base.Start();

            if (!Application.isPlaying)
            {
                return;
            }

            ForEachExtension(e => e.WhenStart());
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (!Application.isPlaying)
            {
                return;
            }

            bool shouldPointerUp = IsPressed();

            if (shouldPointerUp)
            {
                ForEachExtension(e => e.WhenUp());
            }

            ForEachExtension(e => e.WhenDisable());
        }

        void OnApplicationFocus(bool hasFocus)
        {
            bool shouldPointerUp = !hasFocus && IsPressed();

            if (shouldPointerUp)
            {
                ForEachExtension(e => e.WhenUp());
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInteractable())
            {
                return;
            }

            base.OnPointerDown(eventData);

            ForEachExtension(e => e.WhenDown());
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (!IsInteractable())
            {
                return;
            }

            base.OnPointerUp(eventData);

            ForEachExtension(e => e.WhenUp());
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!IsInteractable())
            {
                return;
            }

            base.OnPointerClick(eventData);

            ForEachExtension(e => e.WhenClick());
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            var currentInteractable = IsInteractable();
            if (_wasInteractable != currentInteractable)
            {
                _wasInteractable = currentInteractable;
                ForEachExtension(x => x.WhenInteractable(currentInteractable));
            }

            base.DoStateTransition(state, instant);
        }

        void ForEachExtension(Action<ButtonExtension> action)
        {
            foreach (ButtonExtension extension in Extensions)
            {
                if (extension == null)
                {
                    continue;
                }

                if (!extension.ExtensionEnabled)
                {
                    continue;
                }

                action.Invoke(extension);
            }
        }
    }
}
