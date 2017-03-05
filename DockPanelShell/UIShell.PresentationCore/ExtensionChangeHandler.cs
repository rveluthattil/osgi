using System;
using UIShell.OSGi.Utility;

namespace UIShell.PresentationCore
{
    public class ExtensionChangeHandler : IDisposable
    {
        public ExtensionChangeHandler(IExtensionBuilder builder, Action<object> newItemHandler,
                                       Action<object> extensionRemovedHandler)
        {
            Builder = builder;
            NewItemHandler = newItemHandler;
            RemoveItemHandler = extensionRemovedHandler;

            builder.ItemAdded += builder_ItemAdded;
            builder.ItemRemoved += builder_ItemRemoved;
        }

        void builder_ItemRemoved(object sender, EventArgs<object> e)
        {
            if (RemoveItemHandler != null)
            {
                RemoveItemHandler(e.Item);
            }
        }

        void builder_ItemAdded(object sender, EventArgs<object> e)
        {
            if (NewItemHandler != null)
            {
                NewItemHandler(e.Item);
            }
        }

        public IExtensionBuilder Builder { get; private set; }
        public Action<object> NewItemHandler { get; private set; }
        public Action<object> RemoveItemHandler { get; private set; }

        public void Dispose()
        {
            Builder.ItemAdded -= builder_ItemAdded;
            Builder.ItemRemoved -= builder_ItemRemoved;
        }
    }
}