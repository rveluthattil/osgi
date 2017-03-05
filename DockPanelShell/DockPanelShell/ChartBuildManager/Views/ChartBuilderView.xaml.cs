using System.Windows.Controls;
using ChartBuildManager.Services;
using DockShell;
using Jbe.CABExtension.SmartPartInfos;
using UIShell.OSGi;
using Page = ChartBuilder.Page;

namespace ChartBuildManager.Views
{
    /// <summary>
    /// Interaction logic for ChartBuilderView.xaml
    /// </summary>
    public partial class ChartBuilderView : UserControl, IControlView
    {
        public ChartBuilderView()
        {
            InitializeComponent();

            var page = new Page();
            Content = page;
        }

        public string ProfileName { get; set; }

        #region IControlView Members

        string IControlView.Name { get; set; }

        void IControlView.Show()
        {
            IControlView view = this;
            var dpInfo = new DockPanelSmartPartInfo(view.Name, ProfileName);
            dpInfo.Icon = Properties.Resources.Device;
            var workspace = BundleRuntime.Instance.GetFirstOrDefaultService<IWorkspace>();
            workspace.Show(this, dpInfo);

            workspace.Activate(this);
        }

        void IControlView.Close()
        {
            var workspace = BundleRuntime.Instance.GetFirstOrDefaultService<IWorkspace>();
            workspace.Hide(this);
        }

        #endregion
    }
}