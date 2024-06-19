using System;
using System.Threading;
using System.Threading.Tasks;

namespace GUtilsUnity.UiStack.Builder
{
    /// <summary>
    /// Interface for building a sequence of UI view stack operations.
    /// There operations will be executed one after the other.
    /// </summary>
    public interface IUiStackSequenceBuilder
    {
        /// <summary>
        /// Shows a registered view with the id of type T.
        /// This also moves the view in front of all other views.
        /// </summary>
        /// <param name="instance">The object to be interacted with</param>
        /// <param name="instantly">Whether to show the view instantly.</param>
        IUiStackSequenceBuilder Show<T>(T instance, bool instantly = false);

        /// <summary>
        /// Hides a registered view with the id of type T, and pushes it to the stack.
        /// </summary>
        /// <param name="instance">The object to be interacted with</param>
        /// <param name="instantly">Whether to hide the view instantly.</param>
        IUiStackSequenceBuilder HideAndPush<T>(T instance, bool instantly = false);

        /// <summary>
        /// Hides a registered view with the id of type T.
        /// </summary>
        /// <param name="instance">The object to be interacted with</param>
        /// <param name="instantly">Whether to hide the view instantly.</param>
        IUiStackSequenceBuilder Hide<T>(T instance, bool instantly = false);

        /// <summary>
        /// Hides a the current view.
        /// </summary>
        /// <param name="instantly">Whether to hide the view instantly.</param>
        IUiStackSequenceBuilder HideCurrent(bool instantly = false);

        IUiStackSequenceBuilder HideAllPopups(bool instantly = false);

        /// <summary>
        /// Shows the last view that was pushed to the the stack.
        /// This also moves the view in front of all other views.
        /// </summary>
        /// <param name="instantly">Whether to show the view instantly.</param>
        IUiStackSequenceBuilder ShowLast(bool instantly = false);

        /// <summary>
        /// Shows the last view that was pushed to the the stack.
        /// This also moves the view behind the most foreground view.
        /// </summary>
        /// <param name="instantly">Whether to show the view instantly.</param>
        IUiStackSequenceBuilder ShowLastBehindForeground(bool instantly = false);

        /// <summary>
        /// Moves a registered view with the id of type T behind all other views.
        /// </summary>
        /// <param name="instance">The object to be interacted with</param>
        IUiStackSequenceBuilder MoveToBackground<T>(T instance);

        /// <summary>
        /// Moves a registered view with the id of type T in front of all other views.
        /// </summary>
        /// <param name="instance">The object to be interacted with</param>
        IUiStackSequenceBuilder MoveToForeground<T>(T instance);

        /// <summary>
        /// Moves the current showing view in front of all other views.
        /// </summary>
        IUiStackSequenceBuilder MoveCurrentToForeground();

        /// <summary>
        /// Sets a registered view with the id of type T, whether is interactable.
        ///</summary>
        /// <param name="instance">The object to be interacted with</param>
        ///<param name="set">Whether to set the view as interactable.</param>
        IUiStackSequenceBuilder SetInteractable<T>(T instance, bool set);

        /// <summary>
        /// Sets whether the current view is interactable.
        ///</summary>
        ///<param name="set">Whether to set the current view as interactable.</param>
        IUiStackSequenceBuilder CurrentSetInteractable(bool set);

        /// <summary>
        /// Adds a callback to the sequence.
        ///</summary>
        ///<param name="callback">The callback to add.</param>
        IUiStackSequenceBuilder Callback(Action callback);

        /// <summary>
        /// Executes the sequence asynchronously.
        ///</summary>
        ///<param name="cancellationToken">The cancellation token for the task.</param>
        Task Execute(CancellationToken cancellationToken);

        /// <summary>
        /// Executes the sequence asynchronously.
        ///</summary>
        void Execute();
    }
}
