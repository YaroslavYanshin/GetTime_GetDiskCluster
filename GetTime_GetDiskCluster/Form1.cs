using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GetTime_GetDiskCluster
{
    public struct SYSTEMTIME
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        public extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool GetDiskFreeSpace(string lpRootPathName,
                                            out uint lpSectorsPerCluster,
                                            out uint lpBytesPerSector,
                                            out uint lpNumberOfFreeClusters,
                                            out uint lpTotalNumberOfClusters);

        public Form1()
        {
            InitializeComponent();
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                comboBox1.Items.Add(d);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ulong size = 256;
            SYSTEMTIME stime = new SYSTEMTIME();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ulong size = 256;
            SYSTEMTIME stime = new SYSTEMTIME();
            GetSystemTime(ref stime);
            textBox1.Text = stime.wDay.ToString() + " день";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Выберете диск", "Предупреждение");
            }

            else
            {
                uint sectorsPerCluster, bytesPerSector, numberOfFreeClusters, totalNumberOfClusters;
                string disk = comboBox1.SelectedItem.ToString(); // @"C:\";
                GetDiskFreeSpace(disk, out sectorsPerCluster, out bytesPerSector, out numberOfFreeClusters, out totalNumberOfClusters);
                textBox2.Text = string.Format("Количество кластеров на диске {0}: {1}", disk, totalNumberOfClusters);
            }
            
        }
    }
}
