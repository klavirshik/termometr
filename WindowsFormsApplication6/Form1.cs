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
using System.IO;
using System.Collections;
using ZedGraph;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        object locker = new object();


        private delegate void LineReceivedEvent(double temp, double davl);
        private delegate void LineReceivedEvent1(double temp);
        public Form1()
        {
            InitializeComponent();                    
           // DrawGraph();

            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            serialPort1.PortName = "COM1";
            serialPort1.BaudRate = 9600;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.DataBits = 8;
            serialPort1.Handshake = Handshake.None;
            serialPort1.Open(); //Порт открыт
            serialPort1.DataReceived += serialPort1_DataReceived;
            serialPort1.ReceivedBytesThreshold = 4;

            
        }
       
//************************************** Считывание данных с порта
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            if (serialPort1.ReadByte() == 128)
            {
                
                byte[] data_read = new byte[5];
                serialPort1.Read(data_read, 0, data_read.Length);
                int temp = (data_read[0] + (data_read[1] << 8))*100; //Соединяем два байта в один, Температура
                temp = temp / 16;
                int davl = (data_read[2] + (data_read[3] << 8))*100; //Соединяем два байта в один, Давление
                davl = davl / 16;
                if (((data_read[0] + data_read[1] + data_read[2] + data_read[3]) / 4) == data_read[4])
                {
                    System.Threading.Thread.Sleep(100);
                    byte[] data_write = new byte[4];
                    data_write[0] = 130;
                    data_write[1] = Convert.ToByte(DateTime.Now.Hour);
                    data_write[2] = Convert.ToByte(DateTime.Now.Minute);
                   
                    data_write[3] = Convert.ToByte((DateTime.Now.Hour + DateTime.Now.Minute) / 2);
                    serialPort1.Write(data_write, 0, data_write.Length);
                    this.BeginInvoke(new LineReceivedEvent(LineReceived), temp, davl);        
                }
                      
            }
            if (serialPort1.ReadByte() == 127)
            {

                byte[] data_read = new byte[3];
                serialPort1.Read(data_read, 0, data_read.Length);
                int temp = (data_read[0] + (data_read[1] << 8)) * 100; //Соединяем два байта в один, Температура
                temp = temp / 16;
                if (((data_read[0] + data_read[1]) /2) == data_read[2])
                {
                    System.Threading.Thread.Sleep(100);
                    byte[] data_write = new byte[4];
                    data_write[0] = 130;
                    data_write[1] = Convert.ToByte(DateTime.Now.Hour);
                    data_write[2] = Convert.ToByte(DateTime.Now.Minute);

                    data_write[3] = Convert.ToByte((DateTime.Now.Hour + DateTime.Now.Minute) / 2);
                    serialPort1.Write(data_write, 0, data_write.Length);
                    this.BeginInvoke(new LineReceivedEvent1(LineReceived), temp);
                }

            }

        }

        //запись давления и температуры      
        private void LineReceived(double temp, double davl)
        {           
            string path = "Temp_and_Davl.txt";
            string date = DateTime.Now.ToString();
            // Создание файла и запись в него
            temp = temp / 100;
            davl = davl / 100;
            label1.Text = temp.ToString();
            label2.Text = davl.ToString();
            lock (locker)
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(128);
                    sw.WriteLine(temp);
                    sw.WriteLine(davl);
                    sw.WriteLine(date);
                    sw.Close();
                }
            }
        }

        private void LineReceived(double temp)
        {
            string path = "Temp_and_Davl.txt";
            string date = DateTime.Now.ToString();
            // Создание файла и запись в него
            temp = temp / 100;

            label8.Text = temp.ToString();

            lock (locker)
            {
                using (StreamWriter sw = File.AppendText(path))
                {

                    sw.WriteLine(127);
                    sw.WriteLine(temp);
                    sw.WriteLine(date);
                    sw.Close();
                }
            }
        }

       // static void Stream()
       // {


            // }

            //****************************************************** trey         

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            notifyIcon1.ContextMenuStrip = contextMenuStrip1; 
        }
    
        protected override void OnResize(EventArgs e)
        {
            notifyIcon1.BalloonTipText = "Thermometer launched";
            notifyIcon1.ShowBalloonTip(3000);           
          
            if (WindowState == FormWindowState.Minimized)
            {               
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
            else
            {
                ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
            base.OnResize(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Hide();
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                timer1.Enabled = false;
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
            base.OnFormClosing(e);          
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            serialPort1.Close();
        }

        private void графикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal; 
            //if (WindowState == FormWindowState.Minimized)
            //{
            //    ShowInTaskbar = true;
            //    WindowState = FormWindowState.Normal;
            //}            
        }

        //******************************************************************************* Graph


        private void DrawGraph (DateTime startDate, DateTime endDate)
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            myPane.CurveList.Clear();

            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            lock (locker)
            {
                string path = "Temp_and_Davl.txt";
                StreamReader sr = File.OpenText(path);


                while (sr.Peek() > 0)
                {
                    DateTime currentDate;
                    switch (Convert.ToInt16(sr.ReadLine()))
                    {
                       
                        case 128:
                            double yValue = Convert.ToDouble(sr.ReadLine());
                            double yValue1 = Convert.ToDouble(sr.ReadLine());
                            currentDate = Convert.ToDateTime(sr.ReadLine()); ;
                            if (currentDate > startDate && currentDate < endDate)
                         {
                                list1.Add(new XDate(currentDate), yValue);

                                list2.Add(new XDate(currentDate), yValue1);
                         }
                            break;
                        case 127:
                            double yValue2 = Convert.ToDouble(sr.ReadLine());
                            currentDate = Convert.ToDateTime(sr.ReadLine()); ;
                            if (currentDate > startDate && currentDate < endDate)
                            {
                                list3.Add(new XDate(currentDate), yValue2);
                            }
                            break;
                    }
                }
                sr.Close();
            }
            LineItem myCurve1 = myPane.AddCurve("Температура", list1, Color.Red, SymbolType.None);
            LineItem myCurve2 = myPane.AddCurve("Давление", list2, Color.Blue, SymbolType.None);
            LineItem myCurve3 = myPane.AddCurve("Температура у дисплея", list3, Color.Green, SymbolType.None);
            myPane.XAxis.Type = AxisType.Date;

            myPane.YAxis.Min = 0;
            myPane.YAxis.Max = 90;

           myPane.XAxis.Min = new XDate(startDate);
           myPane.XAxis.Max = new XDate(endDate);

           // myPane. = true;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void renew_graf_Click(object sender, EventArgs e)
        {

            timer1.Enabled = false;
            DateTime start_range = new DateTime(start_date.Value.Year, start_date.Value.Month, start_date.Value.Day, Convert.ToInt16(start_time.Text.Split(':')[0]), Convert.ToInt16(start_time.Text.Split(':')[1]), 0);
            DateTime end_range = new DateTime(end_date.Value.Year, end_date.Value.Month, end_date.Value.Day, Convert.ToInt16(end_time.Text.Split(':')[0]), Convert.ToInt16(end_time.Text.Split(':')[1]),0);
            DrawGraph(start_range, end_range);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            DrawGraph(DateTime.Now.AddHours(-(trackBar1.Value)), DateTime.Now);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            DrawGraph(DateTime.Now.AddHours(-(trackBar1.Value)), DateTime.Now);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label9.Text = trackBar1.Value.ToString();
        }
    }
}