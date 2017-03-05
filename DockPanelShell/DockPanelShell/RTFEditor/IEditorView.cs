using System;

namespace RTFEditor
{
    public interface IEditorView
    {
        bool Modified { get; set; }
        event EventHandler ModifiedChanged;
        event EventHandler Disposed;

        void Open(string fileName);
        void Save(string fileName);
    }
}