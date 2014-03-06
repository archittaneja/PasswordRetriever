using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        bool fail = false;
        bool StoreData = false;
        List<byte> FetchData = new List<byte>();
        byte[] pwd_hw = new byte[20];
        private void Form3_Load(object sender, EventArgs e)
        {
            serialPort1.Open();

            button1.Enabled = false;
            int addr = 0;

            StoreData = true;
            

            for (int j = addr; j < addr + 20; j++)
            {
                byte[] packet = new byte[7];
                packet[0] = (byte)'S';
                packet[1] = (byte)'R';
                packet[2] = (byte)((j >> 8) & 0xFF);
                packet[3] = (byte)(j & 0xFF);
                packet[4] = (byte)0;
                packet[5] = (byte)'E';
                packet[6] = (byte)'P';

                serialPort1.Write(packet, 0, 7);
            }

            ParameterizedThreadStart pts = new ParameterizedThreadStart(CreateThread);

            System.Threading.Thread thr = new System.Threading.Thread(pts);

            thr.Start();







        }
       
        void CreateThread(object obj)
        {
            System.Threading.Thread.Sleep(250);

            StoreData = false;

            if (FetchData.Count != 20)
            {
               
                 fail = true;
                    
                
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {


            StringBuilder builder = new StringBuilder();
            foreach (byte b in FetchData) // Loop through all strings
            {   
                if((char)b!='\0')
                builder.Append((char)b); // Append string to StringBuilder
            }
            string result = builder.ToString(); // Get string from StringBuilder
            
            if (fail == false)
            {
                if (result==textBox1.Text)
                {

                    
                    button1.Enabled = true;


                }



            }
        }

        private void serialPort1_DataReceived(object sender,SerialDataReceivedEventArgs e)
        {

            byte[] buff = new byte[serialPort1.BytesToRead];
            serialPort1.Read(buff, 0, serialPort1.BytesToRead);

            if (StoreData)
            {
                FetchData.AddRange(buff);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1=new Form1();

            serialPort1.Close();
            this.Hide();
            f1.ShowDialog();
            this.Close();
        }


        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    
    }

        

}
