using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VMSRXMifare1K;

namespace VMSRXMifare1K
{
    static class Program
    {
        static public HFReader reader1 = new HFReader();
        static public LF5557Reader reader2 = new LF5557Reader();
        static public LF4200Reader reader3 = new LF4200Reader();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}