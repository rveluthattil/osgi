namespace ChartBuildManager.Services
{
    /// <summary>
    /// A control view for a test device.
    /// </summary>
    public interface IControlView
    {
        /// <summary>
        /// The name of the device instance.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Show the device configuration and control user interface.
        /// </summary>
        void Show();

        /// <summary>
        /// Close the connection to the device.
        /// </summary>
        void Close();
    }
}