using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Windows;





namespace WindowsFormsApplication6
{


    public partial class Form1 : Form
    {


        //Accounts[] account = new Accounts[10];


        public Form1()
        {
            InitializeComponent();
        }

        string Serialize(Account act)
        {
            return act.acc_id + "|" + act.accountname;
        }

        Account DeSerialize(string str)
        {
            string[] parts = str.Split('|');

            if (parts.Length == 2)
            {
                Account act = new Account();
                act.acc_id = int.Parse(parts[0]);
                act.accountname = parts[1];
                return act;
            }

            throw new Exception("Invalid account string.");
        }

        private List<Account> GetAccounts()
        {
            List<Account> acts = new List<Account>();

            try
            {
                foreach (string str in Properties.Settings.Default.Accounts)
                {
                    try
                    {
                        acts.Add(DeSerialize(str));
                    }
                    catch { }
                }
            }
            catch { }

            return acts;
        }

        readonly int MAX_ID = 50;

        bool StoreData = false;
        List<byte> FetchData = new List<byte>();
        byte[] pwd_hw = new byte[20];
        int found_newID = -1;
        string uName = "";

        void CreateThread(object obj)
        {
            System.Threading.Thread.Sleep(250);

            StoreData = false;

            if (FetchData.Count == 20)
            {
                bool fail = false;
                for (int i = 0; i < 20; i++)
                {
                    if (FetchData[i] != pwd_hw[i])
                    {
                        fail = true;
                        break;
                    }
                }

                if (fail == false)
                {
                    Account newAct = new Account();
                    newAct.acc_id = found_newID;
                    newAct.accountname = uName;

                    string act_ser = Serialize(newAct);

                    if (Properties.Settings.Default.Accounts == null)
                        Properties.Settings.Default.Accounts = new System.Collections.Specialized.StringCollection();



                    Properties.Settings.Default.Accounts.Add(act_ser);
                    Properties.Settings.Default.Save();

                    this.Invoke(new MethodInvoker(delegate
                    {
                        UpdateDisplay();
                    }));

                }

            }
        }

        private void createaccount_Click(object sender, EventArgs e)
        {

            using (CreateAccount create = new CreateAccount())
            {
                if (DialogResult.OK == create.ShowDialog())
                {
                    uName = create.username.Text.Replace("|", ":");
                    pwd_hw = new byte[20];

                    byte[] pwd_act = Encoding.ASCII.GetBytes(create.password.Text);

                    Array.Copy(pwd_act, pwd_hw, pwd_act.Length);

                    List<Account> acts = GetAccounts();

                    found_newID = -1;

                    for (int i = 1; i < MAX_ID; i++)
                    {
                        bool found = false;
                        foreach (Account act in acts)
                        {
                            if (act.acc_id == i)
                            {
                                found = true;
                                break;
                            }
                        }

                        if (found == false)
                        {
                            found_newID = i;
                            break;
                        }
                    }

                    if (found_newID != -1)
                    {

                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("No Free Memory's Available");
                    }


                    int addr = found_newID * 20;

                    StoreData = true;
                    FetchData.Clear();

                    for (int j = addr; j < addr + 20; j++)
                    {
                        byte[] packet = new byte[7];
                        packet[0] = (byte)'S';
                        packet[1] = (byte)'W';
                        packet[2] = (byte)((j >> 8) & 0xFF);
                        packet[3] = (byte)(j & 0xFF);
                        packet[4] = pwd_hw[j - addr];
                        packet[5] = (byte)'E';
                        packet[6] = (byte)'P';

                        serialPort1.Write(packet, 0, 7);
                    }


                    ParameterizedThreadStart pts = new ParameterizedThreadStart(CreateThread);

                    System.Threading.Thread thr = new System.Threading.Thread(pts);

                    thr.Start();
                    richTextBox1.Invoke(new MethodInvoker(delegate
                    {
                        richTextBox1.Clear();
                        richTextBox1.AppendText("New Account Created");
                    }));



                }
            }
        }
        



        private void UpdateDisplay()
        {
            comboBox1.Items.Clear();

            List<Account> acts = GetAccounts();

            foreach (Account act in acts)
            {
               
                
                    comboBox1.Items.Add(act.accountname);
                




            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                setPassword.Enabled = true;
                button2.Enabled = true;
            }

        }



        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            byte[] buff = new byte[serialPort1.BytesToRead];
            serialPort1.Read(buff, 0, serialPort1.BytesToRead);

            if (StoreData)
            {
                FetchData.AddRange(buff);
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buff.Length; i++)
            {
                sb.Append((char)buff[i] + "");
            }

        


        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void setPassword_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            int index = -1;

            List<Account> acts = GetAccounts();

            foreach (Account act in acts)
            {
                if (act.accountname == comboBox1.Text)
                {
                    index = act.acc_id;
                    break;
                }
            }

            StoreData = true;
            FetchData.Clear();

            uint addr = (uint)(index * 20);

            for (uint i = addr; i < addr + 20; i++)
            {
                byte[] packet = new byte[7];
                packet[0] = (byte)'S';
                packet[1] = (byte)'R';
                packet[2] = (byte)((i >> 8) & 0xFF);
                packet[3] = (byte)(i & 0xFF);
                packet[5] = (byte)'E';
                packet[4] = (byte)0;
                packet[6] = (byte)'P';

                serialPort1.Write(packet, 0, 7);
            }
                      
            ParameterizedThreadStart pts = new ParameterizedThreadStart(retreiveThread);

            System.Threading.Thread thr = new System.Threading.Thread(pts);

            thr.Start();
            richTextBox1.Invoke(new MethodInvoker(delegate
            {
                richTextBox1.Clear();
                richTextBox1.AppendText("Password Copied to Clipboard");
            }));


        }

        void retreiveThread(object obj)
        {
            Thread.Sleep(250);

            StoreData = false;

            this.Invoke(new MethodInvoker(delegate
            {
                System.Windows.Forms.Clipboard.SetText(Encoding.ASCII.GetString(FetchData.ToArray()), System.Windows.Forms.TextDataFormat.Text);
            }));
        }

        private void inputText_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {


            serialPort1.Open();

          //  Properties.Settings.Default.Accounts.Clear();
            //Properties.Settings.Default.Save();
            int appX = Width;
            int appY = Height;

            int appF_X = (int)SystemParameters.MaximizedPrimaryScreenWidth;
            int appF_Y = (int)SystemParameters.MaximizedPrimaryScreenHeight;

            this.SetBounds(appF_X - appX-15, appF_Y - appY-15, appX, appY);



            // Properties.Settings.Default.Accounts.Clear();
            // Properties.Settings.Default.Save();

            UpdateDisplay();
            if (comboBox1.Items.Count <= 0)
            {
                button2.Enabled = false;
                setPassword.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                setPassword.Enabled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            /* if (Properties.Settings.Default.Accounts != null)
             {
                 string cbName = comboBox1.Text;

                 for(int i=0;i<Properties.Settings.Default.Accounts.Count;i++)
                 {
                     string st = Properties.Settings.Default.Accounts[i];

                     Account a = DeSerialize(st);
                     if(a.accountname == cbName)
                     {
                         Properties.Settings.Default.Accounts.RemoveAt(i);
                         Properties.Settings.Default.Save(); 
                         break;
                     }
                 }
             }*/


            List<Account> acts = GetAccounts();

            for (int i = 0; i < acts.Count; i++)
            {
                if (acts[i].accountname == comboBox1.SelectedItem.ToString())
                {

                    Properties.Settings.Default.Accounts.RemoveAt(i);
                    Properties.Settings.Default.Save();

                    UpdateDisplay();
                    break;
                }
            }

            UpdateDisplay();
            richTextBox1.Invoke(new MethodInvoker(delegate
            {
                richTextBox1.Clear();
                richTextBox1.AppendText("Account Deleted");
            }));

            if (comboBox1.Items.Count <= 0)
                button2.Enabled = false;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {           
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                serialPort1.Close();
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_Change_Click(object sender, EventArgs e)
        {
            using(Form2 f2 = new Form2(comboBox1.Text))
            {
                 if (DialogResult.OK == f2.ShowDialog())
                 {

                     pwd_hw = new byte[20];
                     
                     byte[] pwd_act = Encoding.ASCII.GetBytes(f2.textBox3.Text);

                     Array.Copy(pwd_act, pwd_hw, pwd_act.Length);

                     List<Account> acts = GetAccounts();

                     found_newID = -1;

                     Account temp=acts[0]; 
                         foreach (Account act in acts)
                         {
                             if (act.accountname == comboBox1.Text)
                             {
                                 temp = act;
                                 found_newID=act.acc_id;
                                 break;
                             }
                         }


                     int addr = found_newID * 20;

                     StoreData = true;
                     FetchData.Clear();

                     for (int j = addr; j < addr + 20; j++)
                     {
                         byte[] packet = new byte[7];
                         packet[0] = (byte)'S';
                         packet[1] = (byte)'W';
                         packet[2] = (byte)((j >> 8) & 0xFF);
                         packet[3] = (byte)(j & 0xFF);
                         packet[4] = pwd_hw[j - addr];
                         packet[5] = (byte)'E';
                         packet[6] = (byte)'P';

                         serialPort1.Write(packet, 0, 7);
                     }
                     
                     Properties.Settings.Default.Accounts.Remove(Serialize(temp));

                     ParameterizedThreadStart pts = new ParameterizedThreadStart(CreateThread);

                     System.Threading.Thread thr = new System.Threading.Thread(pts);

                     thr.Start();




                     richTextBox1.Invoke(new MethodInvoker(delegate
                     {
                         richTextBox1.Clear();
                         richTextBox1.AppendText("Password Changed");
                     }));













                 }




            }
        }
    }

    struct Account 
    {
        public string accountname;
        public int acc_id;
    };




}