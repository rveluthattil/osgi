using System.Drawing;
using System.IO;

namespace UIShell.PresentationCore
{
    internal class ResourcesUtility
    {
        public static Bitmap TryGetBitmap(object resource)
        {
            if (resource == null)
                return null;
            var stream = resource as Stream;
            if (stream != null)
            {
                return new Bitmap(stream);
            }
            var bitmap = resource as Bitmap;
            if (bitmap != null)
            {
                return bitmap;
            }
            var icon = resource as Icon;
            if (icon != null)
            {
                return icon.ToBitmap();
            }
            return null;
        }
    }
}