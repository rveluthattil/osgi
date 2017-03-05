using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UIShell.MessageBusService;
using UIShell.OSGi;
using UIShell.PresentationCore;
using Action = System.Action;

namespace UIShell.Document
{
    public class EditManager : IEditManager
    {
        //private WorkItem workItem;
        private readonly IAdapterFactoryCatalog<IEditHandler> factoryCatalog;
        private readonly Dictionary<object, IEditHandler> handlers = new Dictionary<object, IEditHandler>();
        private IEditHandler activeHandler;


        public EditManager(IAdapterFactoryCatalog<IEditHandler> factoryCatalog)
        {
            this.factoryCatalog = factoryCatalog;
            Application.Idle += ApplicationIdle;

            var service = BundleRuntime.Instance.GetFirstOrDefaultService<ICommandBusService>();
            service.SubscribeCommandEvent(CommandNames.Copy, (sender, e) => UIInvoke(Copy, sender, e));
            service.SubscribeCommandEvent(CommandNames.Cut, (sender, e) => UIInvoke(Cut, sender, e));
            service.SubscribeCommandEvent(CommandNames.Delete, (sender, e) => UIInvoke(Delete, sender, e));
            service.SubscribeCommandEvent(CommandNames.Paste, (sender, e) => UIInvoke(Paste, sender, e));
            service.SubscribeCommandEvent(CommandNames.Redo, (sender, e) => UIInvoke(Redo, sender, e));
            service.SubscribeCommandEvent(CommandNames.SelectAll, (sender, e) => UIInvoke(SelectAll, sender, e));
            service.SubscribeCommandEvent(CommandNames.Undo, (sender, e) => UIInvoke(Undo, sender, e));
        }

        #region IEditManager Members

        public void Register(IEditHandler editHandler)
        {
            editHandler.Enter += EditEnter;
            editHandler.Leave += EditLeave;
        }

        public void Register(object uiElement)
        {
            DoRegister(uiElement);
        }

        public void Deregister(IEditHandler editHandler)
        {
            editHandler.Enter -= EditEnter;
            editHandler.Leave -= EditLeave;
        }

        public void Deregister(object uiElement)
        {
            if (handlers.ContainsKey(uiElement))
            {
                Deregister(handlers[uiElement]);
                handlers.Remove(uiElement);
            }
        }

        #endregion

        private void UIInvoke(MessageBusHandler handler, object sender, object args)
        {
            if (activeHandler == null)
                return;
            UIInvoke(() => handler(sender, args));
        }

        private void UIInvoke(Action action)
        {
            var layoutView = BundleRuntime.Instance.GetFirstOrDefaultService<IShellLayoutView>();
            layoutView.Invoke(() => action());
        }

        private IEditHandler DoRegister(object uiElement)
        {
            if (handlers.ContainsKey(uiElement))
                return handlers[uiElement];
            IAdapterFactory<IEditHandler> factroy = factoryCatalog.GetFactory(uiElement);
            if (factroy == null)
                return null;
            IEditHandler handler = factroy.GetAdapter(uiElement);

            handlers.Add(uiElement, handler);
            Register(handler);
            return handler;
        }

        private void UpdateEditCommands()
        {
            Console.WriteLine(activeHandler);
            bool enabled = (activeHandler != null);

            SetCommandStatus(CommandNames.Undo, enabled && activeHandler.CanUndo);
            SetCommandStatus(CommandNames.Redo, enabled && activeHandler.CanRedo);
            SetCommandStatus(CommandNames.Cut, enabled && activeHandler.CanCut);
            SetCommandStatus(CommandNames.Copy, enabled && activeHandler.CanCopy);
            SetCommandStatus(CommandNames.Paste, enabled && activeHandler.CanPaste);
            SetCommandStatus(CommandNames.Delete, enabled && activeHandler.CanDelete);
            SetCommandStatus(CommandNames.SelectAll, enabled && activeHandler.CanSelectAll);
        }

        private void SetCommandStatus(string commandName, bool enabled)
        {
            Console.WriteLine("{0}->{1}", commandName, enabled);
            var service = BundleRuntime.Instance.GetFirstOrDefaultService<ICommandBusService>();
            if (service == null)
            {
                return;
            }
            service.PublicCommandStatus(commandName, (enabled) ? CommandStatus.Enabled : CommandStatus.Disabled);
        }

        #region Command and Event handler

        public void RegisterActiveElement(object uiElement)
        {
            IEditHandler newHandler = DoRegister(uiElement);
            if (newHandler != null)
            {
                activeHandler = newHandler;
            }
        }

        private void EditEnter(object sender, EventArgs e)
        {
            //Guard.TypeIsAssignableFromType(sender.GetType(), typeof(IEditHandler), "sender");

            activeHandler = (IEditHandler) sender;
            UpdateEditCommands();
        }

        private void EditLeave(object sender, EventArgs e)
        {
            activeHandler = null;
            UpdateEditCommands();
        }

        private void ApplicationIdle(object sender, EventArgs e)
        {
            UpdateEditCommands();
        }


        //[CommandHandler(CommandNames.Undo)]
        public void Undo(object sender, object e)
        {
            activeHandler.Undo();
        }

        //[CommandHandler(CommandNames.Redo)]
        public void Redo(object sender, object e)
        {
            activeHandler.Redo();
        }

        //[CommandHandler(CommandNames.Cut)]
        public void Cut(object sender, object e)
        {
            activeHandler.Cut();
        }

        //[CommandHandler(CommandNames.Copy)]
        public void Copy(object sender, object e)
        {
            activeHandler.Copy();
        }

        //[CommandHandler(CommandNames.Paste)]
        public void Paste(object sender, object e)
        {
            activeHandler.Paste();
        }

        //[CommandHandler(CommandNames.Delete)]
        public void Delete(object sender, object e)
        {
            activeHandler.Delete();
        }

        //[CommandHandler(CommandNames.SelectAll)]
        public void SelectAll(object sender, object e)
        {
            activeHandler.SelectAll();
        }

        #endregion
    }
}