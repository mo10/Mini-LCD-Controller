using LibUsbDotNet;
using LibUsbDotNet.DeviceNotify;
using LibUsbDotNet.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MiniLCDController
{
    public partial class Form1 : Form
    {
        #region For capture mouse pointer
        //[StructLayout(LayoutKind.Sequential)]
        //struct CURSORINFO
        //{
        //    public Int32 cbSize;
        //    public Int32 flags;
        //    public IntPtr hCursor;
        //    public POINTAPI ptScreenPos;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //struct POINTAPI
        //{
        //    public int x;
        //    public int y;
        //}
        //[DllImport("user32.dll")]
        //static extern bool GetCursorInfo(out CURSORINFO pci);

        //[DllImport("user32.dll")]
        //static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

        //const Int32 CURSOR_SHOWING = 0x00000001;
        #endregion

        CaptureForm captureForm = null;
        Thread thread = null;
        UsbDevice inUsingDevce = null;
        IDeviceNotifier UsbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();
        UsbEndpointWriter writer = null;
        UsbCmd usbcmd = null;
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 将 Bitmap 转换为 Byte数组
        /// </summary>
        /// <param name="bitmap">Bitmap对象</param>
        /// <returns>Bitmap像素集合</returns>
        public byte[] imageToByte(Bitmap bitmap)
        {
            Bitmap temp;
            lock (bitmap)
                temp = (Bitmap)bitmap.Clone();//从原始Bitmap拷贝一个新的对象，否则LockBits会抛出异常
            Rectangle area = (new Rectangle(0, 0, temp.Width, temp.Height));
            BitmapData bitmapData = temp.LockBits(area, ImageLockMode.ReadWrite, PixelFormat.Format16bppRgb565);
            int stride = bitmapData.Stride;
            IntPtr ptr = bitmapData.Scan0;

            byte[] array = new byte[(temp.Width* temp.Height)*2];
            Marshal.Copy(ptr, array, 0, (temp.Width * temp.Height) * 2);
            return array;
        }
        /// <summary>
        /// 像素高低位反转，用于RGB565
        /// </summary>
        /// <param name="buf">像素集合</param>
        /// <returns>反转后的集合</returns>
        public byte[] byteTurn(byte[] buf)
        {
            byte[] new_buf = new byte[buf.Length];
            for (int i = 0; i < buf.Length; i += 2)
            {
                new_buf[i] = buf[i + 1];
                new_buf[i+1] = buf[i];
            }
            return new_buf;
        }
        /// <summary>
        /// 抓取截图线程函数
        /// </summary>
        private void CaptureFrame()
        {
            
            Bitmap bitmap = new Bitmap(160, 80, PixelFormat.Format16bppRgb565);
            Size size = bitmap.Size;

            DateTime beforDT = System.DateTime.Now;

            int frame = 0;
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                while (true)
                {
                    int y = captureForm.Top;
                    int x = captureForm.Left;

                    y += captureForm.capture_box.Top;
                    x += captureForm.capture_box.Left;
                    lock (graphics)
                        graphics.CopyFromScreen(x, y, 0, 0, size);//屏幕截图
                    if (preview_box.InvokeRequired)
                    {
                        while (!preview_box.IsHandleCreated)
                        {
                            if (preview_box.IsDisposed)
                                return;
                        }
                        //向usb设备发送图片
                        lock (usbcmd)
                        {
                            usbcmd.sendImage(byteTurn(imageToByte(bitmap)));
                            frame++;
                        }
                        //刷新预览框内容
                        preview_box.Invoke(new Action(() =>
                        {
                            preview_box.Image = bitmap;
                        }));
                        DateTime afterDT = System.DateTime.Now;
                        TimeSpan ts = afterDT.Subtract(beforDT);
                        if (ts.TotalSeconds >= 1)
                        {
                            lb_fps.Invoke(new Action(() =>
                            {
                                lb_fps.Text = "当前帧数:"+(int)(frame * 1.0 / ts.TotalMilliseconds * 1000);
                            }));
                            frame = 0;
                            beforDT = System.DateTime.Now;
                        }
                        
                    }
                }
            }
        }
        /// <summary>
        /// 刷新USB设备列表
        /// </summary>
        private void UpdateDeviceList()
        {
            UsbRegDeviceList allDevices = UsbDevice.AllDevices;

            device_list.BeginUpdate();
            device_list.Items.Clear();
            foreach (UsbRegistry usbRegistry in allDevices)
            {
                if (usbRegistry.Device == null)
                    continue;
                int vid = usbRegistry.Vid;
                int pid = usbRegistry.Pid;
                string desc = usbRegistry.Name;
                ComboboxItem item = new ComboboxItem();
                item.Text = String.Format("{0:X4}:{1:X4} {2}", vid, pid, desc);
                item.Tag = usbRegistry;
                device_list.Items.Add(item);
            }
            device_list.EndUpdate();
        }
        /// <summary>
        /// 设备发生变动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeviceNotifyEvent(object sender, DeviceNotifyEventArgs e)
        {
            
            if (inUsingDevce != null
                && inUsingDevce.UsbRegistryInfo.SymbolicName.Equals(e.Device.Name)
                && e.EventType ==EventType.DeviceRemoveComplete)
            {
                // 当前设备已移除
                disconnected();
            }
            if(inUsingDevce == null && e.EventType == EventType.DeviceArrival)
                UpdateDeviceList();

        }
        /// <summary>
        /// 初始化USB设备
        /// </summary>
        private void usbDeviceInit()
        {
            IUsbDevice wholeUsbDevice = inUsingDevce as IUsbDevice;
            if (!ReferenceEquals(wholeUsbDevice, null))
            {
                // This is a "whole" USB device. Before it can be used, 
                // the desired configuration and interface must be selected.

                // Select config #1
                wholeUsbDevice.SetConfiguration(1);

                // Claim interface #0.
                wholeUsbDevice.ClaimInterface(0);
            }
            writer = inUsingDevce.OpenEndpointWriter(WriteEndpointID.Ep02);
            usbcmd = new UsbCmd(writer);

        }
        /// <summary>
        /// 连接调用
        /// </summary>
        private void connected()
        {
            pl_conn.Enabled = false;
            pl_main.Enabled = true;
            btn_connect.Text = "断开连接";
            usbDeviceInit();
        }
        /// <summary>
        /// 断开连接调用
        /// </summary>
        private void disconnected()
        {
            

            pl_conn.Enabled = true;
            pl_main.Enabled = false;
            
            if (captureForm != null)
            {
                captureForm.Close();
                captureForm = null;
            }
            if (thread !=null && thread.IsAlive)
            {
                thread.Abort();
            }
            writer = null;
            usbcmd = null;
            btn_connect.Text = "连接设备";
            btn_capture.Text = "开始窗口捕捉";
            if (inUsingDevce != null)
            {
                inUsingDevce.Close();
                inUsingDevce = null;
            }
            
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            UsbDeviceNotifier.OnDeviceNotify += OnDeviceNotifyEvent;
            UpdateDeviceList();
        }
        /// <summary>
        /// 屏幕捕捉按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_capture_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["CaptureForm"];

            if (form == null)
            {
                captureForm = new CaptureForm();
                captureForm.Show();
                thread = new Thread(CaptureFrame);
                thread.Start();
                btn_capture.Text = "停止窗口捕捉";
            }
            else
            {
                thread.Abort();
                form.Close();
                btn_capture.Text = "开始窗口捕捉";
            }

        }
        /// <summary>
        /// 连接设备按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_connect_Click(object sender, EventArgs e)
        {
            UsbRegistry usbRegistry;
            ComboboxItem item = (ComboboxItem)device_list.SelectedItem;
            if (item == null || item.Tag == null)
            {
                MessageBox.Show("请选择一个设备");
                return;
            }
            usbRegistry = (UsbRegistry)item.Tag;
            if (inUsingDevce == null
                && usbRegistry.Open(out inUsingDevce)
                && inUsingDevce != null)
            {
                connected();
            }
            else
            {
                disconnected();
            }
        }

        private void btn_toBlDfu_Click(object sender, EventArgs e)
        {
            lock (usbcmd)
                usbcmd.rebootToBootloader();
            disconnected();
        }
    }
}
