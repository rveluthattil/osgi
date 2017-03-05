using System.Windows.Forms;
using UIShell.Document.Builder;
using UIShell.Document.EditAdapters;
using UIShell.MessageBusService;
using UIShell.OSGi;
using UIShell.OSGi.Core.Service;
using UIShell.PresentationCore;
using UIShell.PresentationCore.Impl;
using UIShell.PresentationCore.WinForms;
//using Jbe.CABExtension.SmartPartInfos;

namespace UIShell.Document
{
    public class Activator : IBundleActivator
    {
        private readonly ActiveControlMonitor _activeControlMonitor = new ActiveControlMonitor();
        private IBundleContext _context;

        private IExtensionManager _extensionManager;
        ExtensionHooker _extensionHooker ;
        #region IBundleActivator Members

        public void Start(IBundleContext context)
        {
            _context = context;
            context.AddService<ICommandBusService>(
                new CommandBusService(context.GetFirstOrDefaultService<IMessageBusService>()));

            var factory = new AdapterFactoryCatalog<IEditHandler>();
            factory.RegisterFactory(new TextBoxAdapterFactory());
            context.AddService<IAdapterFactoryCatalog<IEditHandler>>(factory);
            context.AddService<IEditManager>(new EditManager(factory));
            context.AddService<IDocumentManager>(new DocumentManager());
            var layoutView = context.GetFirstOrDefaultService<IShellLayoutView>();

            _extensionManager = context.GetFirstOrDefaultService<IExtensionManager>();
            _extensionHooker = new ExtensionHooker(_extensionManager);

            var extensionChangeHandler = new ExtensionChangeHandler(new ToolStripBuilder(),
                                                                    toolStrip =>
                                                                    layoutView.AddToolStrip((ToolStrip) toolStrip),
                                                                    toolStrip =>
                                                                    layoutView.RemoveToolStrip((ToolStrip) toolStrip));

            _extensionHooker.HookExtension(ExtensionPointNames.ToolStrip, extensionChangeHandler);

            extensionChangeHandler = new ExtensionChangeHandler(new MainMenuBuilder(),
                                          toolStripItem => layoutView.AddMenuItem((ToolStripItem) toolStripItem),
                                          toolStripItem => layoutView.RemoveMenuItem((ToolStripItem)toolStripItem));
            _extensionHooker.HookExtension(ExtensionPointNames.MainMenu, extensionChangeHandler);

            var docManager = _context.GetFirstOrDefaultService<IDocumentManager>();

            extensionChangeHandler = new ExtensionChangeHandler(new FileFilterBuilder(),
                                          documentFactory => docManager.Register((IDocumentFactory) documentFactory),
                                          documentFactory => docManager.UnRegister((IDocumentFactory)documentFactory));
            _extensionHooker.HookExtension(ExtensionPointNames.FileFilters, extensionChangeHandler);
            StartActiveControlMonitor();
        }

        public void Stop(IBundleContext context)
        {
            _activeControlMonitor.Stop();
            _extensionHooker.Dispose();
        }

        #endregion

        private void StartActiveControlMonitor()
        {
            _activeControlMonitor.Start();
        }
    }
}