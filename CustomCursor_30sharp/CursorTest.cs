using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CursorTest
{
  public struct IconInfo
  {
    public bool fIcon;
    public int xHotspot;
    public int yHotspot;
    public IntPtr hbmMask;
    public IntPtr hbmColor;
  }

  public class CursorTest : Form
  {
  
    public CursorTest()
    {
      this.Text = "Custom Cursor - 30Sharp.com";
      this.BackColor = Color.Black;

      Bitmap bitmap = new Bitmap(140, 25);
      Graphics g = Graphics.FromImage(bitmap);
      using (Font f = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Bold))
        g.DrawString("\" 30SHARP.COM \"", f, Brushes.White, 0, 0);

      this.Cursor = CreateCursor(bitmap, 3, 3);

      bitmap.Dispose();
    }

    [DllImport("user32.dll")]
    public static extern IntPtr CreateIconIndirect(ref IconInfo icon);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

    public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
    {
      IntPtr ptr = bmp.GetHicon();
      IconInfo tmp = new IconInfo();
      GetIconInfo(ptr, ref tmp);
      tmp.xHotspot = xHotSpot;
      tmp.yHotspot = yHotSpot;
      tmp.fIcon = false;
      ptr = CreateIconIndirect(ref tmp);
      return new Cursor(ptr);
    }

      private void InitializeComponent()
      {
          this.SuspendLayout();
          // 
          // CursorTest
          // 
          this.ClientSize = new System.Drawing.Size(292, 266);
          this.Name = "CursorTest";
          this.ResumeLayout(false);

      }
  }
}