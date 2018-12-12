using LibUsbDotNet;
using LibUsbDotNet.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniLCDController
{
    class UsbCmd
    {
        private const int MAX_TIMEOUT = 200;
        private const int CMD_BUF_HEAD = 0x22;
        private const int CMD_BUF_END = 0x33;

        private const int ST7735_WIDTH = 160;
        private const int ST7735_HEIGHT = 80;
        public enum USB_CMD
        {
            USB_CMD_INVERTCOLOR = 0,
            USB_CMD_STOP,
            USB_CMD_SET_BACKLIGHT,
            USB_CMD_SET_SCREENCOLOR,
            USB_CMD_DRAWIMAGE,
            USB_CMD_PRINTTEXT,
            USB_CMD_FILLRECT,
            USB_CMD_TOBOOTLOADER
        }
        public enum USB_CMD_RET
        {
            USB_CMD_OK = 0,
            USB_CMD_FAIL
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct USB_CMD_BUF
        {
            public byte head;
            public byte cmd;
            public ushort length;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public  byte[] data;
            public byte end;
        }

        UsbEndpointWriter writer;
        public UsbCmd(UsbEndpointWriter writer)
        {
            this.writer = writer;
        }
        /// <summary>
        /// 发送控制命令
        /// </summary>
        /// <param name="cmd">USB_CMD命令</param>
        /// <param name="length">欲发送数据长度</param>
        /// <param name="data">命令附加参数,即如:{x, y, width, height}</param>
        public void sendCommand(USB_CMD cmd,ushort length,byte[] data)
        {
            USB_CMD_BUF buf = new USB_CMD_BUF();
            buf.head = CMD_BUF_HEAD;
            buf.end = CMD_BUF_END;
            buf.cmd = (byte)cmd;
            buf.length = length;
            buf.data = data;
            int a;
            ErrorCode ec = writer.Transfer(getBytes(buf), 0, 9, MAX_TIMEOUT, out a);
            //if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);
        }
        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="data">像素集合</param>
        /// <param name="x">起始像素X坐标</param>
        /// <param name="y">起始像素Y坐标</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public void sendImage(byte[] data, byte x, byte y, byte width , byte height)
        {
            int data_len, data_offset, max_len;

            max_len = 64;
            data_len = data.Length;

            byte[] pos = new byte[4] { x, y, width, height };
            sendCommand(USB_CMD.USB_CMD_DRAWIMAGE, (ushort)data.Length, pos);//发送DRAWIMAGE请求

            data_offset = 0;
            for (int i = 0; i <= data_len / 64; i++)
            {
                if (i == (data_len / 64) && (data_len % 64) > 0)
                {
                    //最后一个数据包
                    int last_len = data_len % 64;
                    int a;
                    ErrorCode ec = writer.Transfer(data, data_offset, last_len, MAX_TIMEOUT, out a);
                    //if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);
                }
                else
                {
                    //发送数据
                    int a;
                    ErrorCode ec = writer.Transfer(data, data_offset, max_len, MAX_TIMEOUT, out a);
                    //if (ec != ErrorCode.None) throw new Exception(UsbDevice.LastErrorString);
                    data_offset += max_len;
                }
            }
        }
        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="data">像素集合</param>
        public void sendImage(byte[] data)
        {
            //默认刷全屏
            sendImage(data, 0, 0, ST7735_WIDTH, ST7735_HEIGHT);
        }
        /// <summary>
        /// 使stm32进入Bootloader
        /// </summary>
        public void rebootToBootloader()
        {
            byte[] data = new byte[4] {0,0,0,0};
            sendCommand(USB_CMD.USB_CMD_TOBOOTLOADER, 0, data);
        }
        /// <summary>
        /// 将 USB_CMD_BUF 转换成 byte数组
        /// </summary>
        /// <param name="buf">USB_CMD_BUF对象</param>
        /// <returns>byte数组</returns>
        private byte[] getBytes(USB_CMD_BUF buf)
        {
            int size = Marshal.SizeOf(buf);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(buf, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }
    }
}
