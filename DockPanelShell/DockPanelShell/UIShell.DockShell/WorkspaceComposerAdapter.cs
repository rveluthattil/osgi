namespace DockShell
{
    public class WorkspaceComposerAdapter<TSmartPart, TSmartPartInfo> : WorkspaceComposer<TSmartPart, TSmartPartInfo>,
                                                                        IWorkspaceComposer<TSmartPart>
        where TSmartPartInfo : ISmartPartInfo, new()
    {
        public WorkspaceComposerAdapter(IComposableWorkspace<TSmartPart, TSmartPartInfo> composedWorkspace)
            : base(composedWorkspace)
        {
        }
    }
}