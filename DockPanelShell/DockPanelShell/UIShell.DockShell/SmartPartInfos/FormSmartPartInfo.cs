using System.Drawing;
using System.Windows.Forms;
using DockShell;

namespace Jbe.CABExtension.SmartPartInfos
{
    public class FormSmartPartInfo : IconSmartPartInfo
    {
        private bool controlBox = true;
        private bool maximizeBox = true;
        private bool minimizeBox = true;
        private bool showIcon = true;
        private bool showInTaskBar = true;

        public FormSmartPartInfo() : base(null, null)
        {
        }

        public FormSmartPartInfo(string title, string description) : base(title, description)
        {
        }

        public Point Location { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public bool ControlBox
        {
            get { return controlBox; }
            set { controlBox = value; }
        }

        public bool MinimizeBox
        {
            get { return minimizeBox; }
            set { minimizeBox = value; }
        }

        public bool MaximizeBox
        {
            get { return maximizeBox; }
            set { maximizeBox = value; }
        }

        public bool ShowIcon
        {
            get { return showIcon; }
            set { showIcon = value; }
        }

        public bool ShowInTaskBar
        {
            get { return showInTaskBar; }
            set { showInTaskBar = value; }
        }

        public bool ShowModal { get; set; }

        public IButtonControl AcceptButton { get; set; }

        public IButtonControl CancelButton { get; set; }

        /// <summary>
        /// Creates a new instance of the FormSmartPartInfo with the settings for a modal dialog
        /// </summary>
        public static FormSmartPartInfo CreateModalDialog()
        {
            return CreateModalDialog(string.Empty, string.Empty);
        }

        /// <summary>
        /// Creates a new instance of the FormSmartPartInfo with the settings for a modal dialog
        /// </summary>
        public static FormSmartPartInfo CreateModalDialog(string title, string description)
        {
            var spi = new FormSmartPartInfo(title, description);
            spi.MinimizeBox = false;
            spi.MaximizeBox = false;
            spi.ShowIcon = false;
            spi.ShowInTaskBar = false;
            spi.ShowModal = true;
            return spi;
        }

        /// <summary>
        /// Creates a new instance of the FormSmartPartInfo and copies over the information 
        /// in the source smart part.
        /// </summary>
        public static FormSmartPartInfo ConvertTo(ISmartPartInfo source)
        {
            //Guard.ArgumentNotNull(source, "source");

            var info = new FormSmartPartInfo(source.Title, source.Description);

            var iconInfo = source as IconSmartPartInfo;
            if (iconInfo != null)
            {
                info.Icon = iconInfo.Icon;
            }

            return info;
        }
    }
}