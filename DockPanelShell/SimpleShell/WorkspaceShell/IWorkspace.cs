using System;
namespace WorkspaceShell
{
    public interface IWorkspace
    {
        void AddNavigation(WorkspaceShell.Command.NavigationItem control);
        void Show(object control, object controlInfo);
    }
}
