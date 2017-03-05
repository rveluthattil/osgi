using System.Collections.Generic;

namespace ChartBuildManager.Services
{
    /// <summary>
    /// Describes a test device manager.
    /// </summary>
    public interface IControlViewManager
    {
        /// <summary>
        /// List of all available control views.
        /// </summary>
        IList<IControlView> ControlViews { get; }

        IList<IControlView> SelectedControlViews { get; set; }

        Dictionary<string, IControlViewFactory> Factories { get; }

        /// <summary>
        /// Register a control view factory. An instance of the device controller is created
        /// if a <see cref="IDevice"/> is registered.
        /// </summary>
        void Register(IControlViewFactory factory);

        void Register(IControlView controlView);
    }
}