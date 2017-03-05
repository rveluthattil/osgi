using System;
using UIShell.OSGi;

namespace UIShell.PresentationCore.WinForms
{
    internal class CommandStatusMonitor
    {
        public CommandStatusMonitor(string commandName, Action<bool> changeEnable, Action<bool> changeVisible)
        {
            if (string.IsNullOrEmpty(commandName))
            {
                throw new ArgumentException();
            }
            ChangeEnable = changeEnable;
            ChangeVisible = changeVisible;

            var commandService = BundleRuntime.Instance.GetFirstOrDefaultService<ICommandBusService>();
            if (commandService != null)
            {
                commandService.SubscribeCommandStatusEvent(commandName,
                                                           (sender, e) => UIInvoke(() => OnCommandReceived(sender, e)));
            }
        }

        public CommandStatusMonitor(string commandName)
            : this(commandName, null, null)
        {
        }

        public Action<bool> ChangeEnable { get; set; }
        public Action<bool> ChangeVisible { get; set; }

        private void UIInvoke(Action action)
        {
            var layoutView = BundleRuntime.Instance.GetFirstOrDefaultService<IShellLayoutView>();
            layoutView.Invoke(action);
        }

        private void OnCommandReceived(object sender, object e)
        {
            var contact = (CommandStatusContact) e;

            switch (contact.Status)
            {
                case CommandStatus.Enabled:
                    if (ChangeEnable != null)
                        ChangeEnable(true);
                    break;
                case CommandStatus.Disabled:
                    if (ChangeEnable != null)
                        ChangeEnable(false);
                    break;
                case CommandStatus.Unavailable:
                    if (ChangeVisible != null)
                    {
                        ChangeVisible(false);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}