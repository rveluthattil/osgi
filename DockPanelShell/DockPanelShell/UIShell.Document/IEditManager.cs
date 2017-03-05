using System;

namespace UIShell.Document
{
    /// <summary>
    /// Describes an edit manager service. The manager maps the basic edit functions
    /// (e.g. Undo, Cut, ...) of an UI element to the edit menu of the application.
    /// </summary>
    public interface IEditManager
    {
        /// <summary>
        /// Register an EditHandler.
        /// </summary>
        void Register(IEditHandler editHandler);

        /// <summary>
        /// Register an UI element which is wrapped by an adapter to support the IEditHandler interface.
        /// </summary>
        void Register(object uiElement);

        /// <summary>
        /// Register and set the active handler to the specified element.
        /// </summary>
        void RegisterActiveElement(object uiElement);

        /// <summary>
        /// Deregister an EditHandler. The method does not throw an exception if it is called more than
        /// once for the same object.
        /// </summary>
        void Deregister(IEditHandler editHandler);

        /// <summary>
        /// Deregister an UI element. The method does not throw an exception if it is called more than
        /// once for the same object.
        /// </summary>
        void Deregister(object uiElement);
    }

    /// <summary>
    /// Describes an edit handler.
    /// </summary>
    public interface IEditHandler
    {
        /// <summary>
        /// Gets a value indicating whether the user can undo the previous operation in the ui element.
        /// </summary>
        bool CanUndo { get; }

        /// <summary>
        /// Gets a value indicating whether there are actions that have occured within the ui control
        /// that can be reapplied.
        /// </summary>
        bool CanRedo { get; }

        /// <summary>
        /// The cut method can be applied.
        /// </summary>
        bool CanCut { get; }

        /// <summary>
        /// The copy method can be applied.
        /// </summary>
        bool CanCopy { get; }

        /// <summary>
        /// The paste method can be applied.
        /// </summary>
        bool CanPaste { get; }

        /// <summary>
        /// The delete method can be applied.
        /// </summary>
        bool CanDelete { get; }

        /// <summary>
        /// The select all method can be applied.
        /// </summary>
        bool CanSelectAll { get; }

        /// <summary>
        /// Occurs when the ui element is entered.
        /// </summary>
        event EventHandler Enter;

        /// <summary>
        /// Occurs when the input focus leaves the ui element.
        /// </summary>
        event EventHandler Leave;

        /// <summary>
        /// Undoes the last operation in the ui element.
        /// </summary>
        void Undo();

        /// <summary>
        /// Reapplies the last operation that was undone in the ui element.
        /// </summary>
        void Redo();

        /// <summary>
        /// Moves the current selection in the ui element to the Clipboard.
        /// </summary>
        void Cut();

        /// <summary>
        /// Copies the current selection in the ui element to the Clipboard.
        /// </summary>
        void Copy();

        /// <summary>
        /// Pastes the contents of the Clipboard into the ui element.
        /// </summary>
        void Paste();

        /// <summary>
        /// Deletes the current selection in the ui element.
        /// </summary>
        void Delete();

        /// <summary>
        /// Select all text and all items respectively.
        /// </summary>
        void SelectAll();
    }
}