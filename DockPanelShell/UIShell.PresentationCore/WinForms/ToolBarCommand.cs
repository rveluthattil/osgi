using System;
using System.Windows.Forms;
using System.Xml;
using UIShell.MessageBusService;
using UIShell.OSGi;
using UIShell.PresentationCore.Utility;
using Activator = System.Activator;

namespace UIShell.PresentationCore.WinForms
{
    public class ToolBarCommand : ToolStripMenuItem
    {
        private readonly string commandName;
        private readonly XmlNode node;
        private readonly IBundle owner;
        private CommandStatusMonitor _statusMonitor;
        private Type eventType;
        private object menuCommand; //ICommand or IViewProvider

        public ToolBarCommand(XmlNode node, IBundle owner, bool createCommand)
        {
            RightToLeft = RightToLeft.Inherit;
            this.node = node;
            this.owner = owner;

            if (createCommand)
            {
                LoadClass();
            }

            if (Image == null)
            {
                string icon = XmlUtility.ReadAttribute(node, "icon");
                if (!string.IsNullOrEmpty(icon))
                {
                    Image = ResourcesUtility.TryGetBitmap(owner.LoadResource(icon, ResourceLoadMode.ClassSpace));
                }
            }
            Text = XmlUtility.ReadAttribute(node, "text");
            ToolTipText = XmlUtility.ReadAttribute(node, "tooltip");
            commandName = XmlUtility.ReadAttribute(node, "command");

            if (!string.IsNullOrEmpty(commandName))
            {
                _statusMonitor = new CommandStatusMonitor(commandName, enable => Enabled = enable,
                                                          visible => Visible = visible);
            }
        }

        private void LoadClass()
        {
            string classType = XmlUtility.ReadAttribute(node, "class");
            if (menuCommand == null && !string.IsNullOrEmpty(classType))
            {
                menuCommand = Activator.CreateInstance(owner.LoadClass(classType));
            }
            string commandType = XmlUtility.ReadAttribute(node, "event");
            if (eventType == null && !string.IsNullOrEmpty(commandType))
            {
                eventType = owner.LoadClass(commandType);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (menuCommand == null || eventType == null)
            {
                LoadClass();
            }
            //1,Handle ICommand
            if (menuCommand != null)
            {
                var cmd = menuCommand as ICommand;
                if (cmd != null)
                {
                    cmd.Run();
                }
                var viewProvider = menuCommand as IViewProvider;
                if (viewProvider != null)
                {
                    var shellLayoutView = BundleRuntime.Instance.GetFirstOrDefaultService<IShellLayoutView>();
                    if (viewProvider.ViewInfo == null)
                    {
                        shellLayoutView.Show(viewProvider.View);
                    }
                    else
                    {
                        shellLayoutView.Show(viewProvider.View, viewProvider.ViewInfo);
                    }
                }
            }
            //2,Handle command name
            if (!string.IsNullOrEmpty(commandName))
            {
                var commandService = BundleRuntime.Instance.GetFirstOrDefaultService<ICommandBusService>();
                commandService.PublicCommand(commandName);
            }
            //3,Handle event bus
            if (eventType != null)
            {
                var service = BundleRuntime.Instance.GetFirstOrDefaultService<IMessageBusService>();
                service.Publish(eventType, Activator.CreateInstance(eventType));
            }
        }
    }
}