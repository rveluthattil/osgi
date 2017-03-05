using DockShell;

namespace Jbe.CABExtension.SmartPartInfos
{
    /// <summary>
    /// A <see cref="SmartPartInfo"/> that describes how a smartpart will be shown in a dock workspace.
    /// </summary>
    public class DockPanelSmartPartInfo : IconSmartPartInfo
    {
        private IShowStrategy showStrategy = new DefaultShowStrategy();

        public DockPanelSmartPartInfo()
        {
        }

        public DockPanelSmartPartInfo(string title, string description) : base(title, description)
        {
        }

        public DockingType DockingType { get; set; }

        public IShowStrategy ShowStrategy
        {
            get { return showStrategy; }
            set { showStrategy = value; }
        }

        /// <summary>
        /// Creates a new instance of the DockPanelSmartPartInfo and copies over the information 
        /// in the source smart part.
        /// </summary>
        public static DockPanelSmartPartInfo ConvertTo(ISmartPartInfo source)
        {
            //Guard.ArgumentNotNull(source, "source");

            var info = new DockPanelSmartPartInfo(source.Title, source.Description);

            var iconInfo = source as IconSmartPartInfo;
            if (iconInfo != null)
            {
                info.Icon = iconInfo.Icon;
            }

            return info;
        }
    }

    public enum DockingType
    {
        Document,
        TaskView
    }
}