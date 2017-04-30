using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VMSRXMifare1K;
using System.Runtime.InteropServices;

namespace Mifare1K
{     
    public partial class Mifare1K : Form
    {       

        public Mifare1K()
        {
            InitializeComponent();
        }

        private void Mifare1K_Load(object sender, EventArgs e)
        {
            cbxMass3.SelectedIndex = 0;

            txtSearchPurse.MaxLength = 12;

            txtDataOne3.MaxLength = 32;
            txtDataTwo3.MaxLength = 32;
            txtDataThree3.MaxLength = 32;
            txtKeyA3.MaxLength = 12;
            txtKey3.MaxLength = 8;
            txtKeyB3.MaxLength = 12;
            txtInputKey3.MaxLength = 12;

            txtInputKey3.Text = "ffffffffffff";

            DoRequest();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            DoRequest();
        }

        private void btnReadSector3_Click(object sender, EventArgs e)
        {
            short icdev = 0x0000;
            int status;
            byte mode = 0x60;
            byte secnr = 0x00;

            txtDataOne3.Text = "";
            txtDataTwo3.Text = "";
            txtDataThree3.Text = "";
            txtKeyA3.Text = "";
            txtKey3.Text = "";
            txtKeyB3.Text = "";

            if (rbtKeyB3.Checked)
                mode = 0x61;

            secnr = Convert.ToByte(cbxMass3.Text);

            IntPtr keyBuffer = Marshal.AllocHGlobal(1024);

            byte[] bytesKey = Utils.ToDigitsBytes(txtInputKey3.Text);
            for (int i = 0; i < bytesKey.Length; i++)
                Marshal.WriteByte(keyBuffer, i, bytesKey[i]);
            status = HFReader.rf_M1_authentication2(icdev, mode, (byte)(secnr * 4), keyBuffer);
            Marshal.FreeHGlobal(keyBuffer);
            if (status != 0)
            {
                MessageBox.Show("rf_M1_authentication2 failed!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IntPtr dataBuffer = Marshal.AllocHGlobal(1024);
            for (int i = 0; i < 4; i++)
            {
                int j;
                byte cLen = 0;
                status = HFReader.rf_M1_read(icdev, (byte)((secnr * 4) + i), dataBuffer, ref cLen);

                if (status != 0 || cLen != 16)
                {
                    MessageBox.Show("rf_M1_read failed!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Marshal.FreeHGlobal(dataBuffer);
                    return;
                }

                byte[] bytesData = new byte[16];
                for (j = 0; j < bytesData.Length; j++)
                    bytesData[j] = Marshal.ReadByte(dataBuffer, j);

                if (i == 0)
                    txtDataOne3.Text = Utils.ToHexString(bytesData);
                else if (i == 1)
                    txtDataTwo3.Text = Utils.ToHexString(bytesData);
                else if (i == 2)
                    txtDataThree3.Text = Utils.ToHexString(bytesData);
                else if (i == 3)
                {
                    byte[] byteskeyA = new byte[6];
                    byte[] byteskey = new byte[4];
                    byte[] byteskeyB = new byte[6];

                    for (j = 0; j < 16; j++)
                    {
                        if (j < 6)
                            byteskeyA[j] = bytesData[j];
                        else if (j >= 6 && j < 10)
                            byteskey[j - 6] = bytesData[j];
                        else
                            byteskeyB[j - 10] = bytesData[j];
                    }

                    txtKeyA3.Text = ""; // Utils.ToHexString(byteskeyA);
                    txtKey3.Text = Utils.ToHexString(byteskey);
                    txtKeyB3.Text = Utils.ToHexString(byteskeyB);
                }
            }
            Marshal.FreeHGlobal(dataBuffer);            
        }

        private void DoWriteBlock(int blockNo)
        {
            short icdev = 0x0000;
            int status;
            byte mode = 0x60;
            byte secnr = 0x00;
            byte adr;
            int i;

            if (rbtKeyB3.Checked)
                mode = 0x61;

            secnr = Convert.ToByte(cbxMass3.Text);
            adr = (byte)(blockNo + secnr * 4);

            if (blockNo == 3)
            {
                if (DialogResult.No == MessageBox.Show("Are you sure you want to overwrite block3!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return;
            }

            IntPtr keyBuffer = Marshal.AllocHGlobal(1024);

            byte[] bytesKey = Utils.ToDigitsBytes(txtInputKey3.Text);
            for (i = 0; i < bytesKey.Length; i++)
                Marshal.WriteByte(keyBuffer, i, bytesKey[i]);
            status = HFReader.rf_M1_authentication2(icdev, mode, (byte)(secnr * 4), keyBuffer);
            Marshal.FreeHGlobal(keyBuffer);
            if (status != 0)
            {
                MessageBox.Show("rf_M1_authentication2 failed!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            byte[] bytesBlock;
            if (blockNo == 0)
            {
                bytesBlock = Utils.ToDigitsBytes(txtDataOne3.Text);
            }
            else if (blockNo == 1)
            {
                bytesBlock = Utils.ToDigitsBytes(txtDataTwo3.Text);
            }
            else if (blockNo == 2)
            {
                bytesBlock = Utils.ToDigitsBytes(txtDataThree3.Text);
            }
            else
            {
                if(string.IsNullOrEmpty(txtKeyA3.Text) || txtKeyA3.Text.Length < 12)
                {
                    MessageBox.Show("Please enter keyA (12 symbols)");
                    return;
                }
                String strBlock3 = txtKeyA3.Text;
                strBlock3 += txtKey3.Text;
                strBlock3 += txtKeyB3.Text;
                bytesBlock = Utils.ToDigitsBytes(strBlock3);
            }

            IntPtr dataBuffer = Marshal.AllocHGlobal(1024);

            for (i = 0; i < bytesBlock.Length; i++)
                Marshal.WriteByte(dataBuffer, i, bytesBlock[i]);
            status = HFReader.rf_M1_write(icdev, adr, dataBuffer);
            Marshal.FreeHGlobal(dataBuffer);

            if (status != 0)
            {
                MessageBox.Show("rf_M1_write failed!!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void btnWriteBlock0_Click(object sender, EventArgs e)
        {
            DoWriteBlock(0);
        }

        private void btnWriteBlock1_Click(object sender, EventArgs e)
        {
            DoWriteBlock(1);
        }

        private void btnWriteBlock2_Click(object sender, EventArgs e)
        {
            DoWriteBlock(2);
        }

        private void btnWriteBlock3_Click_1(object sender, EventArgs e)
        {
            DoWriteBlock(3);
        }

        private void DoRequest()
        {
            short icdev = 0x0000;
            int status;
            byte bcnt = 0x04; //mifare
            IntPtr pSnr;
            byte len = 255;
            sbyte size = 0;

            pSnr = Marshal.AllocHGlobal(1024);

            for (int i = 0; i < 2; i++)
            {
                //
                status = HFReader.rf_anticoll(icdev, bcnt, pSnr, ref len);
                if (status != 0)
                    continue;

                status = HFReader.rf_select(icdev, pSnr, len, ref size);
                if (status != 0)
                    continue;

                byte[] szBytes = new byte[len];

                for (int j = 0; j < len; j++)
                {
                    szBytes[j] = Marshal.ReadByte(pSnr, j);
                }

                String m_cardNo = String.Empty;

                for (int q = 0; q < len; q++)
                {
                    m_cardNo += Utils.byteHEX(szBytes[q]);
                }
                txtSearchPurse.Text = m_cardNo;

                break;
            }

            Marshal.FreeHGlobal(pSnr);
        }

        private void cbxMass3_SelectedValueChanged(object sender, EventArgs e)
        {
            btnWriteBlock0.Enabled = !(cbxMass3.SelectedIndex == 0);
        }
    }
}