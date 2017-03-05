using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Forms;
using Jbe.CABExtension.SmartPartInfos;
using WeifenLuo.WinFormsUI.Docking;

namespace DockShell
{
    /// <summary>
    /// Implements a Workspace that shows smartparts in dock panels.
    /// </summary>
    public class DockPanelWorkspace : DockPanel, IComposableWorkspace<Control, DockPanelSmartPartInfo>,
                                      IDockPanelWorkspace
    {
        private readonly IWorkspaceComposer<Control> composer;
        private readonly Dictionary<Control, IDockContent> dockContents;
        private bool isDisposing;

        ///// <summary>
        ///// Dependency injection setter property to get the WorkItem where the object is contained.
        ///// </summary>
        //[ServiceDependency]
        //public WorkItem WorkItem
        //{
        //    set { composer.WorkItem = value; }
        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="DockPanelWorkspace"/> class
        /// </summary>
        public DockPanelWorkspace()
        {
            dockContents = new Dictionary<Control, IDockContent>();
            composer = CreateWorkspaceComposer();

            ActiveContentChanged += ThisActiveContentChanged;
        }

        #region IComposableWorkspace<Control,DockPanelSmartPartInfo> Members

        void IComposableWorkspace<Control, DockPanelSmartPartInfo>.OnActivate(Control smartPart)
        {
            OnActivate(smartPart);
        }

        void IComposableWorkspace<Control, DockPanelSmartPartInfo>.OnApplySmartPartInfo(Control smartPart,
                                                                                        DockPanelSmartPartInfo
                                                                                            smartPartInfo)
        {
            OnApplySmartPartInfo(smartPart, smartPartInfo);
        }

        void IComposableWorkspace<Control, DockPanelSmartPartInfo>.OnShow(Control smartPart,
                                                                          DockPanelSmartPartInfo smartPartInfo)
        {
            OnShow(smartPart, smartPartInfo);
        }

        void IComposableWorkspace<Control, DockPanelSmartPartInfo>.OnHide(Control smartPart)
        {
            OnHide(smartPart);
        }

        void IComposableWorkspace<Control, DockPanelSmartPartInfo>.OnClose(Control smartPart)
        {
            OnClose(smartPart);
        }

        void IComposableWorkspace<Control, DockPanelSmartPartInfo>.RaiseSmartPartActivated(WorkspaceEventArgs e)
        {
            OnSmartPartActivated(e);
        }

        void IComposableWorkspace<Control, DockPanelSmartPartInfo>.RaiseSmartPartClosing(WorkspaceCancelEventArgs e)
        {
            OnSmartPartClosing(e);
        }

        DockPanelSmartPartInfo IComposableWorkspace<Control, DockPanelSmartPartInfo>.ConvertFrom(ISmartPartInfo source)
        {
            return OnConvertFrom(source);
        }

        public event EventHandler<WorkspaceCancelEventArgs> SmartPartClosing;

        public event EventHandler<WorkspaceEventArgs> SmartPartActivated;

        public ReadOnlyCollection<object> SmartParts
        {
            get { return composer.SmartParts; }
        }

        public object ActiveSmartPart
        {
            get { return composer.ActiveSmartPart; }
        }

        public void Activate(object smartPart)
        {
            composer.Activate(smartPart);
        }

        public void ApplySmartPartInfo(object smartPart, ISmartPartInfo smartPartInfo)
        {
            composer.ApplySmartPartInfo(smartPart, smartPartInfo);
        }

        public void Close(object smartPart)
        {
            composer.Close(smartPart);
        }

        public void Hide(object smartPart)
        {
            composer.Hide(smartPart);
        }

        public void Show(object smartPart, ISmartPartInfo smartPartInfo)
        {
            composer.Show(smartPart, smartPartInfo);
        }

        public void Show(object smartPart)
        {
            composer.Show(smartPart);
        }

        #endregion

        #region IDockPanelWorkspace Members

        DockPanel IDockPanelWorkspace.DockPanel
        {
            get { return this; }
        }

        IDockContent IDockPanelWorkspace.GetDockContent(Control smartPart)
        {
            return dockContents[smartPart];
        }

        #endregion

        /// <summary>
        /// Create a WorkspaceComposer. GoF Design Pattern: Factory Method.
        /// </summary>
        protected virtual IWorkspaceComposer<Control> CreateWorkspaceComposer()
        {
            return new WorkspaceComposerAdapter<Control, DockPanelSmartPartInfo>(this);
        }

        /// <summary>
        /// Overriden to control when the workspace is being disposed to disable the control activation logic.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            isDisposing = disposing;
            ActiveContentChanged -= ThisActiveContentChanged;
            base.Dispose(disposing);
        }

        #region Private

        private void ThisActiveContentChanged(object sender, EventArgs e)
        {
            if (ActiveContent == null)
            {
                return;
            }

            Control smartPart = GetSmartPart(ActiveContent);

            if (smartPart != null)
            {
                Activate(smartPart);
            }
            else
            {
                composer.SetActiveSmartPart(null);
            }
        }

        // Raise the SmartPartClosing event and use the returned value.
        private void FormClosing(object sender, FormClosingEventArgs e)
        {
            Control smartPart = GetSmartPart((IDockContent) sender);
            if (smartPart != null)
            {
                var wce = new WorkspaceCancelEventArgs(smartPart);
                OnSmartPartClosing(wce);
                e.Cancel = wce.Cancel;
            }
        }

        private void ControlDisposed(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (!isDisposing && control != null)
            {
                composer.ForceClose(control);
            }
        }

        private Control GetSmartPart(IDockContent dockContent)
        {
            // Locate the smart part corresponding to the dockContent.
            foreach (var pair in dockContents)
            {
                if (pair.Value == dockContent)
                {
                    return pair.Key;
                }
            }

            // not found
            return null;
        }

        #endregion

        #region Protected virtual implementation

        /// <summary>
        /// Create an IDockContent object that hosts the smart part.
        /// </summary>
        protected virtual IDockContent CreateDockContent(Control smartPart)
        {
            var dockContent = new DockContent();
            dockContent.Name = Guid.NewGuid().ToString();
            dockContent.BackColor = ProfessionalColors.MenuBorder;
            dockContent.Padding = new Padding(1);

            var backgroundPanel = new Panel();
            backgroundPanel.BackColor = ProfessionalColors.ToolStripGradientMiddle;
            backgroundPanel.Dock = DockStyle.Fill;

            smartPart.Dock = DockStyle.Fill;

            dockContent.Controls.Add(backgroundPanel);
            backgroundPanel.Controls.Add(smartPart);
            return dockContent;
        }

        /// <summary>
        /// Converts a smart part information to a compatible one for the workspace.
        /// </summary>
        protected virtual DockPanelSmartPartInfo OnConvertFrom(ISmartPartInfo source)
        {
            return DockPanelSmartPartInfo.ConvertTo(source);
        }

        /// <summary>
        /// Activate the smart part.
        /// </summary>
        protected virtual void OnActivate(Control smartPart)
        {
            dockContents[smartPart].DockHandler.Activate();
        }

        /// <summary>
        /// Apply the DockPanelSmartPartInfo at the IDockContent container.
        /// </summary>
        protected virtual void OnApplySmartPartInfo(Control smartPart, DockPanelSmartPartInfo smartPartInfo)
        {
            IDockContent dockContent = dockContents[smartPart];
            dockContent.DockHandler.TabText = smartPartInfo.Title;
            dockContent.DockHandler.ToolTipText = smartPartInfo.Description;

            dockContent.DockHandler.Form.Icon = smartPartInfo.Icon;
            switch (smartPartInfo.DockingType)
            {
                case DockingType.Document:
                    dockContent.DockHandler.DockAreas = DockAreas.Document;
                    break;
                case DockingType.TaskView:
                    dockContent.DockHandler.DockAreas = DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockRight |
                                                        DockAreas.DockTop | DockAreas.Float;
                    break;
                default:
                    Debug.Assert(false, "The default case is not allowed!");
                    break;
            }
        }

        /// <summary>
        /// Close the window that contains the smart part.
        /// </summary>
        protected virtual void OnClose(Control smartPart)
        {
            // Prevent the closing of the same smartPart during DockHandler.Close()
            if (dockContents.ContainsKey(smartPart))
            {
                IDockContent dockContent = dockContents[smartPart];
                dockContents.Remove(smartPart);

                dockContent.DockHandler.Close();
                dockContent.DockHandler.Form.FormClosing -= FormClosing;
                smartPart.Disposed -= ControlDisposed;
            }
        }

        /// <summary>
        /// Hide the window that contains the smart part.
        /// </summary>
        protected virtual void OnHide(Control smartPart)
        {
            dockContents[smartPart].DockHandler.Hide();
        }

        /// <summary>
        /// Show the SmartPart inside a DockContent container and use the DockPanelSmartPartInfo to
        /// control the appearance.
        /// </summary>
        protected virtual void OnShow(Control smartPart, DockPanelSmartPartInfo smartPartInfo)
        {
            IDockContent dockContent;
            if (dockContents.ContainsKey(smartPart))
            {
                dockContent = dockContents[smartPart];
            }
            else
            {
                dockContent = smartPart as IDockContent;
                if (dockContent == null)
                {
                    dockContent = CreateDockContent(smartPart);
                }

                dockContents.Add(smartPart, dockContent);
                smartPart.Disposed += ControlDisposed;
                dockContent.DockHandler.Form.FormClosing += FormClosing;
                OnApplySmartPartInfo(smartPart, smartPartInfo);
            }

            smartPartInfo.ShowStrategy.Show(this, dockContent);
        }

        /// <summary>
        /// Raises the <see cref="SmartPartActivated"/> event.
        /// </summary>
        protected virtual void OnSmartPartActivated(WorkspaceEventArgs e)
        {
            if (SmartPartActivated != null)
            {
                SmartPartActivated(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="SmartPartClosing"/> event.
        /// </summary>
        protected virtual void OnSmartPartClosing(WorkspaceCancelEventArgs e)
        {
            if (SmartPartClosing != null)
            {
                SmartPartClosing(this, e);
            }
        }

        #endregion
    }
}