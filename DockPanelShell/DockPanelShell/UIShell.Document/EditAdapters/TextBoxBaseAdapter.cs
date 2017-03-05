using System;
using System.Windows.Forms;

namespace UIShell.Document.EditAdapters
{
    public class TextBoxBaseAdapter : IEditHandler
    {
        private readonly TextBoxBase adaptee;

        public TextBoxBaseAdapter(TextBoxBase adaptee)
        {
            this.adaptee = adaptee;
            RegisterEventDelegation();
        }

        #region IEditHandler Members

        public event EventHandler Enter;
        public event EventHandler Leave;

        public virtual bool CanUndo
        {
            get { return adaptee.CanUndo; }
        }

        public virtual bool CanRedo
        {
            get { return false; }
        }

        public virtual bool CanCut
        {
            get { return adaptee.SelectionLength > 0; }
        }

        public virtual bool CanCopy
        {
            get { return adaptee.SelectionLength > 0; }
        }

        public virtual bool CanPaste
        {
            get { return true; }
        }

        public virtual bool CanDelete
        {
            get { return adaptee.SelectionLength > 0; }
        }

        public virtual bool CanSelectAll
        {
            get { return adaptee.TextLength > 0; }
        }

        public virtual void Undo()
        {
            adaptee.Undo();
        }

        public virtual void Redo()
        {
            throw new NotSupportedException();
        }

        public virtual void Cut()
        {
            adaptee.Cut();
        }

        public virtual void Copy()
        {
            adaptee.Copy();
        }

        public virtual void Paste()
        {
            adaptee.Paste();
        }

        public virtual void Delete()
        {
            adaptee.SelectedText = "";
        }

        public virtual void SelectAll()
        {
            adaptee.SelectAll();
        }

        #endregion

        protected virtual void OnEnter(EventArgs e)
        {
            if (Enter != null)
            {
                Enter(this, e);
            }
        }

        protected virtual void OnLeave(EventArgs e)
        {
            if (Leave != null)
            {
                Leave(this, e);
            }
        }

        private void RegisterEventDelegation()
        {
            adaptee.Enter += AdapteeEnter;
            adaptee.Leave += AdapteeLeave;
        }

        private void AdapteeEnter(object sender, EventArgs e)
        {
            OnEnter(e);
        }

        private void AdapteeLeave(object sender, EventArgs e)
        {
            OnLeave(e);
        }
    }
}