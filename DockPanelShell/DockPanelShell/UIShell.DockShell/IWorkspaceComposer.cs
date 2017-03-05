namespace DockShell
{
    /// <summary>
    /// Represents an WorkspaceComposer.
    /// </summary>
    public interface IWorkspaceComposer<TSmartPart> : IWorkspace
    {
        ///// <summary>
        ///// Set the <see cref="WorkItem"/> where the object is contained.
        ///// </summary>
        //WorkItem WorkItem { set; }

        /// <summary>
        /// Sets the active smart part in the workspace.
        /// </summary>
        void SetActiveSmartPart(TSmartPart smartPart);

        /// <summary>
        /// Forcedly closes the smart part, without raising the <see cref="IWorkspace.SmartPartClosing"/> event.
        /// </summary>
        void ForceClose(TSmartPart smartPart);
    }
}