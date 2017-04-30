using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace VMSRXMifare1K
{
    class HFReader: IReader
    {
        [DllImport("kernel32.dll")]
        public static extern void Sleep(int dwMilliseconds);

        [DllImport("MasterRD.dll")]
        public static extern int lib_ver(ref uint pVer);

        [DllImport("MasterRD.dll")]
        public static extern int rf_init_com(int port, int baud);

        [DllImport("MasterRD.dll")]
        public static extern int rf_ClosePort();

        [DllImport("MasterRD.dll")]
        public static extern int rf_antenna_sta(short icdev, byte mode);

        [DllImport("MasterRD.dll")]
        public static extern int rf_init_type(short icdev, byte type);

        [DllImport("MasterRD.dll")]
        public static extern int rf_request(short icdev, byte mode, ref ushort pTagType);

        [DllImport("MasterRD.dll")]
        public static extern int rf_anticoll(short icdev, byte bcnt, IntPtr pSnr, ref byte pRLength);

        [DllImport("MasterRD.dll")]
        public static extern int rf_select(short icdev, IntPtr pSnr, byte srcLen, ref sbyte Size);

        [DllImport("MasterRD.dll")]
        public static extern int rf_halt(short icdev);

        [DllImport("MasterRD.dll")]
        public static extern int rf_M1_authentication2(short icdev, byte mode, byte secnr, IntPtr key);

        [DllImport("MasterRD.dll")]
        public static extern int rf_M1_initval(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]
        public static extern int rf_M1_increment(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]
        public static extern int rf_M1_decrement(short icdev, byte adr, Int32 value);

        [DllImport("MasterRD.dll")]
        public static extern int rf_M1_readval(short icdev, byte adr, ref Int32 pValue);

        [DllImport("MasterRD.dll")]
        public static extern int rf_M1_read(short icdev, byte adr, IntPtr pData, ref byte pLen);

        [DllImport("MasterRD.dll")]
        public static extern int rf_M1_write(short icdev, byte adr, IntPtr pData);

        public bool IsInitialized { get; set; }

        public int Port { get; set; }

        public bool Initialize(int port)
        {
            int status = rf_init_com(port, 115200);
            if (0 == status)
            {
                IsInitialized = true;
                Port = port;
                return true;
            }
            else
            {
                IsInitialized = false;
            }
            return false;
        }

        public bool IsCardPresent(out ushort cardTagType)
        {
            short icdev = 0x0000;
            ushort tagType = 0;
            byte mode = 0x52;

            int status = rf_request(icdev, mode, ref tagType);
            cardTagType = tagType;
            return status == 0;
        }

    }
}
