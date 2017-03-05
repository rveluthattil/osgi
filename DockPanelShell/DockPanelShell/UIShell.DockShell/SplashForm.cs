using System.Drawing;
using System.Windows.Forms;

namespace DockShell
{
    public class SplashForm : Form
    {
        /// <summary>
        /// Creates a Form for the splash image.
        /// </summary>
        /// <param name="splashImage">The Image to show as splash screen.</param>
        /// <param name="transparentColor">The color used for transparent areas.</param>
        public SplashForm(Image splashImage, Color transparentColor)
        {
            BackColor = transparentColor;
            BackgroundImage = splashImage;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = splashImage.Size;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            TransparencyKey = transparentColor;
        }

        /// <summary>
        /// Creates a Form for the splash image.
        /// </summary>
        /// <param name="splashImage">The Image to show as splash screen.</param>
        public SplashForm(Image splashImage) : this(splashImage, Color.Magenta)
        {
        }
    }
}