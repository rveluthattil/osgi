using System;
using System.Windows;
using System.Windows.Controls;

namespace UIShell.Document.EditAdapters
{
    public class WPFTextBoxAdapter : IEditHandler
    {
        private readonly TextBox adaptee;

        public WPFTextBoxAdapter(TextBox adaptee)
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
            get { return adaptee.CanRedo; }
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
            get { return adaptee.Text.Length > 0; }
        }

        public virtual void Undo()
        {
            adaptee.Undo();
        }

        public virtual void Redo()
        {
            adaptee.Redo();
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
            adaptee.GotFocus += AdapteeEnter;
            adaptee.LostFocus += AdapteeLeave;
        }

        private void AdapteeEnter(object sender, RoutedEventArgs e)
        {
            OnEnter(EventArgs.Empty);
        }

        private void AdapteeLeave(object sender, RoutedEventArgs e)
        {
            OnLeave(EventArgs.Empty);
        }
    }
}