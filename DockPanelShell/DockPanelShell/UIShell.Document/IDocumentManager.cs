using System;
using System.Collections.ObjectModel;

namespace UIShell.Document
{
    /// <summary>
    /// Describes a document.
    /// </summary>
    public interface IDocument
    {
        IDocumentType DocumentType { get; set; }
        string FileName { get; }
        event EventHandler DocumentActivated;
        event EventHandler DocumentDeactivated;
        event EventHandler Disposed;

        void Save(string fileName);
    }

    /// <summary>
    /// Describes a document type.
    /// </summary>
    public interface IDocumentType
    {
        string Description { get; }
        string[] Extensions { get; }
    }

    /// <summary>
    /// Describes a document factory which is able to create new documents and
    /// open existing ones.
    /// </summary>
    public interface IDocumentFactory : IDocumentType
    {
        IDocument New();
        IDocument Open(string fileName);
    }

    /// <summary>
    /// The IDocumentManager keeps track of the registered document types and it is
    /// able to create new documents.
    /// </summary>
    public interface IDocumentManager
    {
        ReadOnlyCollection<IDocumentType> DocumentTypes { get; }
        void Clear();
        void Register(IDocumentFactory documentFactory);
        void UnRegister(IDocumentFactory documentFactory);
        void New(IDocumentType documentType);
    }
}