using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniLCDController
{
    public partial class CaptureForm : Form
    {
        #region For form style using 
        //private const int WM_NCHITTEST = 0x84;
        //private const int HTCLIENT = 0x1;
        //private const int HTCAPTION = 0x2;

        //private bool m_aeroEnabled;

        //private const int CS_DROPSHADOW = 0x00020000;
        //private const int WM_NCPAINT = 0x0085;
        //private const int WM_ACTIVATEAPP = 0x001C;

        //[System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        //public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        //[System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        //public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        //[System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        //public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        //[System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        //private static extern IntPtr CreateRoundRectRgn(
        //    int nLeftRect,
        //    int nTopRect,
        //    int nRightRect,
        //    int nBottomRect,
        //    int nWidthEllipse,
        //    int nHeightEllipse
        //    );

        //public struct MARGINS
        //{
        //    public int leftWidth;
        //    public int rightWidth;
        //    public int topHeight;
        //    public int bottomHeight;
        //}
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        m_aeroEnabled = CheckAeroEnabled();
        //        CreateParams cp = base.CreateParams;
        //        if (!m_aeroEnabled)
        //            cp.ClassStyle |= CS_DROPSHADOW; return cp;
        //    }
        //}
        //private bool CheckAeroEnabled()
        //{
        //    if (Environment.OSVersion.Version.Major >= 6)
        //    {
        //        int enabled = 0; DwmIsCompositionEnabled(ref enabled);
        //        return (enabled == 1) ? true : false;
        //    }
        //    return false;
        //}
        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case WM_NCPAINT:
        //            if (m_aeroEnabled)
        //            {
        //                var v = 2;
        //                DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
        //                MARGINS margins = new MARGINS()
        //                {
        //                    bottomHeight = 1,
        //                    leftWidth = 0,
        //                    rightWidth = 0,
        //                    topHeight = 0
        //                }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
        //            }
        //            break;
        //        default: break;
        //    }
        //    base.WndProc(ref m);
        //    if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        //}
        #endregion

        private bool Drag;
        private int MouseX;
        private int MouseY;

        public CaptureForm()
        {
            InitializeComponent();
        }

        private void CaptureForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(28, 28, 28);
        }
        #region 鼠标拖拽窗口事件
        private void CaptureForm_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;

        }

        private void CaptureForm_MouseUp(object sender, MouseEventArgs e)
        {
            Drag = false;
        }

        private void CaptureForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }
        #endregion
        private void CaptureForm_Paint(object sender, PaintEventArgs e)
        {

            //绘制窗体橙色边框
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(202, 81, 0), 1),
                    0, 0, this.Size.Width - 1, this.Size.Height - 1);
            //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(45, 45, 48)), 1, 1, this.Size.Width - 2, 30);
            //绘制蓝色捕捉边框
            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(0, 122, 240), 1),
                    capture_box.Left-1, capture_box.Top - 1, capture_box.Width +1,capture_box.Height+1);
        }

        private void title_bar_Paint(object sender, PaintEventArgs e)
        {
            //绘制标题
            FontFamily fontFamily = new FontFamily("宋体");
            Font font = new Font(fontFamily, 12,
               FontStyle.Regular, GraphicsUnit.Pixel);

            SolidBrush solidBrush = new SolidBrush(Color.FromArgb(153, 153, 153));
            e.Graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            e.Graphics.DrawString(this.Text, font, solidBrush, new PointF(9,9));
        }

        private void setAllowEvent_Click(object sender, EventArgs e)
        {
            setAllowEvent.Checked = !setAllowEvent.Checked;
            if (setAllowEvent.Checked)
            {
                // 更换TransparencyKey为其他颜色,让鼠标事件穿透窗体
                this.TransparencyKey = Color.Black;
                capture_box.BackColor = Color.Black;
            }
            else
            {
                // TransparencyKey为红色时,鼠标事件不会穿透
                this.TransparencyKey = Color.Red;
                capture_box.BackColor = Color.Red;
            }
        }
    }
}
