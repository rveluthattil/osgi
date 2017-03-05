namespace DockShell
{
    public class ElementHostWorkspaceComposerAdapter<TSmartPart, TSmartPartInfo> :
        ElementHostWorkspaceComposer<TSmartPart, TSmartPartInfo>, IWorkspaceComposer<TSmartPart>
        where TSmartPartInfo : ISmartPartInfo, new()
    {
        public ElementHostWorkspaceComposerAdapter(IComposableWorkspace<TSmartPart, TSmartPartInfo> composedWorkspace)
            : base(composedWorkspace)
        {
        }
    }
}