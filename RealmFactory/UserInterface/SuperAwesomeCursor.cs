using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using RealmFactory.Properties;

namespace Starbound.RealmFactory.UserInterface
{
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }
 
    public class SuperAwesomeCursor
    {
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IconInfo tmp = new IconInfo();
            GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            return new Cursor(CreateIconIndirect(ref tmp));
        }

        public static Cursor CreatePencilCursor()
        {
            return CreateCursor(Resources.Pencil, 0, 16);
        }

        public static Cursor CreatePaintCanCursor()
        {
            return CreateCursor(Resources.PaintCan, 13, 14);
        }

        public static Cursor CreateEraserCursor()
        {
            return CreateCursor(Resources.Eraser, 3, 13);
        }
    }
}
