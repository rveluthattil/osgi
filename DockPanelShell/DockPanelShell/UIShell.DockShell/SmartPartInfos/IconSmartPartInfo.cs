using System.Drawing;

namespace Jbe.CABExtension.SmartPartInfos
{
    /// <summary>
    /// Provides information and an icon about a specific smartpart.
    /// </summary>
    public class IconSmartPartInfo : LightSmartPartInfo
    {
        public IconSmartPartInfo()
        {
        }

        public IconSmartPartInfo(string title, string description) : base(title, description)
        {
        }

        public Icon Icon { get; set; }
    }
}