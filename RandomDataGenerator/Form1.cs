using CLRandomDataGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomDataGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnGenerateRandomCustomers_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "randomCustomers.csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    toolStripStatusLabel1.Text = "Generating list. Plz wait";
                    Application.DoEvents();

                    StringBuilder sbBuilder = new StringBuilder();
                    DataGenerator dataGenerator = new DataGenerator();
                    Dictionary<int, NameAddressEntry> dictionary = dataGenerator.GenerateNameAddressEntry((int) nudRandomCustomerCount.Value);

                    toolStripStatusLabel1.Text = "Building save file";
                    Application.DoEvents();
                    toolStripProgressBar1.Visible = true;
                    toolStripProgressBar1.Maximum = dictionary.Count +1 ;
                    toolStripProgressBar1.Value = 0;
                    for (int i = 1; i <= dictionary.Count; i++)
                    {
                        sbBuilder.AppendLine(i + ";" + dictionary[i].ToString(";"));
                        toolStripProgressBar1.Value = i;
                        Application.DoEvents();
                    }
                    File.WriteAllText(saveFileDialog1.FileName, sbBuilder.ToString());

                }
                catch (Exception exception)
                {
                    toolStripStatusLabel1.Text = "Error: " + exception.Message;
                    //throw;
                }
                finally
                {
                    toolStripStatusLabel1.Text = "Done";
                    toolStripProgressBar1.Visible = false;
                    this.Cursor = DefaultCursor;
                }
            }
        }
    }
}
