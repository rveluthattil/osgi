using DockShell;

namespace Jbe.CABExtension.SmartPartInfos
{
    /// <summary>
    /// Provides information about a specific smartpart.
    /// </summary>
    /// <remarks>
    /// In comparison to <see cref="SmartPartInfo"/> this class does not extend from <see cref="Component"/> 
    /// and therefore it does not provide designer support.
    /// </remarks>
    public class LightSmartPartInfo : ISmartPartInfo
    {
        public LightSmartPartInfo() : this(string.Empty, string.Empty)
        {
        }

        public LightSmartPartInfo(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }

        #region ISmartPartInfo Members

        public string Title { get; set; }

        public string Description { get; set; }

        #endregion
    }
}