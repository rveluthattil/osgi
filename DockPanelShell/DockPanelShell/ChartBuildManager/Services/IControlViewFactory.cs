namespace ChartBuildManager.Services
{
    /// <summary>
    /// A factory for creating <see cref="IControlViwe"/>.
    /// </summary>
    public interface IControlViewFactory
    {
        /// <summary>
        /// The profile supported by the created <see cref="IDeviceController"/>.
        /// </summary>
        string ProfileName { get; }

        /// <summary>
        /// Creates a <see cref="IDeviceController"/> instance and register it as
        /// service in the <see cref="WorkItem"/>.
        /// </summary>
        IControlView Create();
    }
}