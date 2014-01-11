using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirMouse
{


    public partial class Form1 : Form
    {
        private delegate void delegateStuff(string text);
        private delegate void delegateTwoFloats();

        private NotifyIcon _trayIcon;
        private ContextMenu _trayMenu;

        float x, y;
        private enum MessageType { Data, Close, Hello, Bye, Unset };
        UdpClient _socket;
        public int _port = 6000;
        private const float MIN_MOV = 0.035f;
        BackgroundWorker _bw;
        bool _on = true;


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public const int LEFTDOWN = 0x02;
        public const int LEFTUP = 0x04;
        public const int RIGHTDOWN = 0x08;
        public const int RIGHTUP = 0x10;
        public const int WHEEL_DOWN = 0x20;
        public const int WHEEL_UP = 0x40;
        public const int WHEEL_SCROLL = 0x800;

        public Form1()
        {

            InitializeComponent();
            MaximizeBox = false;

            _bw = new BackgroundWorker();

            _bw.WorkerReportsProgress = true;
            _bw.WorkerSupportsCancellation = true;

            _bw.DoWork += bw_DoWork;
            _bw.RunWorkerCompleted += bw_RunWorkerCompleted;

            // Create a simple tray menu with only one item.
            _trayMenu = new ContextMenu();
            _trayMenu.MenuItems.Add("Close", OnExit);


            _trayIcon = new NotifyIcon();
            _trayIcon.Text = Application.ProductName;
            _trayIcon.Icon = Properties.Resources.ico_tray_ico100;//new Icon(SystemIcons.Application, 40, 40);
            _trayIcon.BalloonTipTitle = Application.ProductName;
            _trayIcon.BalloonTipText = String.Format("{0} was minimized, to restore UI click over this icon", Application.ProductName);

            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = false;
            _trayIcon.Click += (o, e) => { WindowState = FormWindowState.Normal; this.Show(); };
            
            ShowInTaskbar = false;

        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                _trayIcon.Visible = true;
                _trayIcon.ShowBalloonTip(500);
                this.Hide();
               
            }

            else if (WindowState == FormWindowState.Normal)
            {
                _trayIcon.Visible = false;
                
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            WriteStatusText("Waiting for inoming connections...");
            _socket = new UdpClient(_port);
            _bw.RunWorkerAsync();
            base.OnLoad(e);
        }
        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, _port);

            while (_on)
            {

                var bytes = _socket.Receive(ref groupEP);
                var data = Encoding.UTF8.GetString(bytes);

                var msgType = ComputeReceivedData(data);
                if (msgType == MessageType.Hello)
                {
                    var answer = Encoding.ASCII.GetBytes("que ase");
                    _socket.Connect(groupEP.Address, groupEP.Port);
                    _socket.Send(answer, answer.Length);

                    WriteStatusText("Connected");
  
                }

                if (msgType == MessageType.Bye)
                {
                    WriteStatusText("Waiting for inoming connections...");
                    _socket.Close();
                    _socket = new UdpClient(_port);
                    groupEP = new IPEndPoint(IPAddress.Any, _port);
                }

            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //_socket.Disconnect(false);
            //_socket.Close();
            // _socket.Dispose();
        }



        private void WriteText(string text)
        {
            if (this.InvokeRequired)
            {
                var del = new delegateStuff(WriteText);
                this.Invoke(del, new[] { text });
            }
            else
            {
                textBox1.Text = "#" + text;
            }
        }

        private void WriteStatusText(string text)
        {
            if (this.InvokeRequired)
            {
                var del = new delegateStuff(WriteStatusText);
                this.Invoke(del, new[] { text });
            }
            else
            {
                labelStatus.Text = text;
            }
        }

        private MessageType ComputeReceivedData(string receivedData)
        {

            string[] splitedData = (receivedData.Split('|'));


            if (receivedData == "keyboard||")
                splitedData = new[] { "keyboard", "|" };


            if (splitedData.Count() == 1)
            {
                WriteText(receivedData);
                if (splitedData[0] == "hola")
                {
                    return MessageType.Hello;
                }
                else if (splitedData[0] == "adeu")
                {
                    return MessageType.Bye;
                }
                else if (splitedData[0] == "close")
                {
                    return MessageType.Close;
                }
                else if (splitedData[0] == "delete")
                {
                    SendKeys.SendWait("{BACKSPACE}");
                }
                else if (splitedData[0] == "enter")
                {
                    SendKeys.SendWait("{ENTER}");
                }
            }

            if (splitedData.Count() == 2)
            {
                string splitedDataX = splitedData[0];
                string splitedDataY = splitedData[1];



                if (splitedDataX == "keyboard")
                {
                    splitedDataY = splitedDataY.Replace("+", "{+}");
                    splitedDataY = splitedDataY.Replace("^", "{^}");
                    splitedDataY = splitedDataY.Replace("%", "{%}");
                    splitedDataY = splitedDataY.Replace("~", "{~}");
                    splitedDataY = splitedDataY.Replace("(", "{(}");
                    splitedDataY = splitedDataY.Replace(")", "{)}");

                    SendKeys.SendWait(splitedDataY);
                }


                float floatX, floatY;
                if (float.TryParse(splitedDataX, out floatX) && float.TryParse(splitedDataY, out floatY))
                {

                    x = floatX;
                    y = floatY;
                    FillProgress();
                }



                double splitedDataXF;
                double splitedDataZF;


                if (splitedDataX == "down")
                {
                    if (splitedDataY == "left")
                        MouseLeftDown();

                    else if (splitedDataY == "right")
                        MouseRightDown();
                }
                if (splitedDataX == "up")
                {
                    if (splitedDataY == "left")
                        MouseLeftUp();

                    else if (splitedDataY == "right")
                        MouseRightUp();
                }

                if (splitedDataX == "wheel")
                {
                    if (splitedDataY == "up")
                    {
                        MouseWheelClick();
                    }

                    else if (String.Empty != splitedDataY)
                    {
                        splitedDataY = splitedDataY.Replace('.', ',');
                        float delta;
                        if (float.TryParse(splitedDataY, out delta))
                        {
                            MousewheelScroll(Convert.ToInt32(delta));
                        }
                    }
                }
                else
                {

                    splitedDataX = splitedDataX.Replace('.', ',');
                    splitedDataY = splitedDataY.Replace('.', ',');
                    var currentX = Cursor.Position.X;
                    var currentY = Cursor.Position.Y;
                    int incX = 0;
                    int incY = 0;


                    if (double.TryParse(splitedDataX, out splitedDataXF))
                    {
                        if (Math.Abs(splitedDataXF) > MIN_MOV)
                        {
                            incY = (int)(splitedDataXF);
                            currentY -= incY;
                        }

                    }

                    if (double.TryParse(splitedDataY, out splitedDataZF))
                    {
                        if (Math.Abs(splitedDataZF) > MIN_MOV)
                        {
                            incX = (int)(splitedDataZF);
                            currentX -= incX;
                        }
                    }

                    Cursor.Position = new Point(currentX, currentY);
                }
                return MessageType.Data;
            }


            return MessageType.Unset;
        }

        private void FillProgress()
        {
            if (this.InvokeRequired)
            {
                var del = new delegateTwoFloats(FillProgress);

                this.Invoke(del);
            }
            else
            {
                const int max = 1000;
                if (Math.Abs(x) < max)
                {
                    if (x > 0)
                    {
                        progressBarRight.Value = Convert.ToInt32(x);
                        progressBarLeft.Value = 0;
                    }
                    else
                    {
                        progressBarRight.Value = 0;
                        progressBarLeft.Value = Convert.ToInt32(Math.Abs(x));
                    }
                }

                if (Math.Abs(y) < max)
                {
                    if (y > 0)
                    {
                        progressBarUp.Value = Convert.ToInt32(y);
                        progressBarDown.Value = 0;
                    }
                    else
                    {
                        progressBarUp.Value = 0;
                        progressBarDown.Value = Convert.ToInt32(Math.Abs(y));
                    }
                }
            }
        }

        #region Mouse Event Simulation
        private void MouseLeftDown()
        {
            mouse_event(LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }
        private void MouseLeftUp()
        {
            mouse_event(LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        private void MouseRightDown()
        {
            mouse_event(RIGHTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }
        private void MouseRightUp()
        {
            mouse_event(RIGHTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        private void MousewheelDown()
        {
            mouse_event(WHEEL_DOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }
        private void MousewheelUp()
        {
            mouse_event(WHEEL_UP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }
        private void MouseWheelClick()
        {
            MousewheelDown();
            MousewheelUp();
        }

        private void MousewheelScroll(int scrollValue)
        {
            mouse_event(WHEEL_SCROLL, Cursor.Position.X, Cursor.Position.Y, scrollValue, 0);
        }
        #endregion

        




    }
}
