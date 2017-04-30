using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace VMSRXMifare1K
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            tmrReaderScan.Interval = 200;
            tmrReaderScan.Enabled = true;
        }

        private void tmrReaderScan_Tick(object sender, EventArgs e)
        {
            if (!Program.reader1.IsInitialized)
            {
                string port = ConfigurationManager.AppSettings["HFReaderPort"];
                txtPort1.Text = port;
                if (!string.IsNullOrEmpty(txtPort1.Text))
                {
                    int port1;
                    if (int.TryParse(txtPort1.Text, out port1))
                    {
                        bool initResult = Program.reader1.Initialize(port1);
                        if (initResult)
                        {
                            lblReader1Status.Text = "Initialized";
                        }
                        else
                        {
                            lblReader1Status.Text = "Initialization Error";
                        }
                    }
                }
            }

            if(Program.reader1.IsInitialized)
            {
                ushort cardTagType;
                bool isCardPresent = Program.reader1.IsCardPresent(out cardTagType);
                lblReader1Status.Text = isCardPresent ? "Card detected [" + cardTagType + "]" : "No card";

                if(cardTagType == 4) // Mifar Classic 1K
                {
                    tmrReaderScan.Enabled = false;
                    Mifare1K.Mifare1K frm = new Mifare1K.Mifare1K();
                    frm.ShowDialog(this);
                    tmrReaderScan.Enabled = true;
                }

            }
        }       
    }
}
