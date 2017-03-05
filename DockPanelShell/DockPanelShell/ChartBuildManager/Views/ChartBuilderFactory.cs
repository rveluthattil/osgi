using System;
using ChartBuildManager.Services;

namespace ChartBuildManager.Views
{
    internal class ChartBuilderFactory : IControlViewFactory
    {
        private int controlViewCount;

        #region IControlViewFactory Members

        public string ProfileName
        {
            get { return "ChartBuilder"; }
        }

        public IControlView Create()
        {
            controlViewCount++;
            //FGenControlView controlView = workItem.Services.AddNew<FGenControlView, IControlView>();
            IControlView controlView = new ChartBuilderView {ProfileName = ProfileName};
            controlView.Name = String.Format("{0:00} {1}", controlViewCount, ProfileName);
            return controlView;
        }

        #endregion
    }
}