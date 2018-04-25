using controlExtension.ViewModel;
using MahApps.Metro.Controls;
using Newtonsoft.Json.Linq;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace controlExtension
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainViewModel vm;
        private SerialPort mySerialPort;
        private DispatcherTimer dispatcherTimer;
        private Thread UpdZedThrd;

        public MainWindow()
        {
            vm = new MainViewModel();
            InitializeComponent();
            DataContext = vm;

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
            mySerialPort = new SerialPort("COM1");
            mySerialPort.BaudRate = 115200;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            vm.comPort.avaibleComPorts = SerialPort.GetPortNames();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (mySerialPort.IsOpen)
            {
                string str = mySerialPort.ReadExisting();
                vm.comPort.answer = str;
                try
                {
                    JObject inJSON = JObject.Parse(str);
                    switch (inJSON["comm"].ToString())
                    {
                        case "STAT?":
                            vm.MainBoard.Status = bool.Parse(inJSON["status"].ToString()) ? "Good" : "Bad";
                            vm.MainWin.Status = "Receive status command \"STAT?\" = " + vm.MainBoard.Status;
                            break;
                        case "hx711?":  
                            vm.hx711.Value = inJSON["hx711"].ToString();
                            vm.MainWin.Status = "Receive hx711 value command \"hx711?\" = " + vm.hx711.Value;
                            vm.exper.rawData.Add(Convert.ToDouble(vm.hx711.Value));
                            break;
                    }
                }
                catch
                {
                    vm.MainWin.Status = "Error parsing json";
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (vm.MainWin.chkValue)
            {
                Thread newThread = new Thread(DoWork);
                newThread.Start();
            }
        }

        public void DoWork()
        {
            if (mySerialPort.IsOpen)
            {
                mySerialPort.WriteLine("{\"comm\":\"hx711?\"}");
                vm.MainWin.Status = "Sending command \"hx711?\"";
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnData_Click(object sender, RoutedEventArgs e)
        {
            mySerialPort.WriteLine("{\"comm\":\"hx711?\"}");
            vm.MainWin.Status = "Sending command \"hx711?\"";
        }

        private void autoGetCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            vm.MainWin.chkValue = (bool)autoGetCheckBox.IsChecked;
        }

        private void comPorts_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mySerialPort != null)
            {
                if (mySerialPort.IsOpen)
                {
                    mySerialPort.Close();
                }
                else
                {
                    if (comPorts_ComboBox.SelectedIndex > -1)
                    {
                        initComPort(comPorts_ComboBox.SelectedIndex);
                    }
                }
            }
            else
            {
                if (comPorts_ComboBox.SelectedIndex > -1)
                {
                    initComPort(comPorts_ComboBox.SelectedIndex);
                }
            }
        }

        private void initComPort(int comPortNum)
        {
            mySerialPort = new SerialPort(vm.comPort.avaibleComPorts[comPortNum]);
            mySerialPort.BaudRate = 115200;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        private void comPorts_ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            vm.comPort.avaibleComPorts = SerialPort.GetPortNames();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (!mySerialPort.IsOpen && comPorts_ComboBox.SelectedIndex > -1)
            {
                try
                {
                    mySerialPort.Open();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Error while opening");
                }
                vm.comPort.IsConnected = mySerialPort.IsOpen;
                vm.comPort.RaisePropertyChanged("IsConnectedVis");
                vm.comPort.RaisePropertyChanged("IsConnectedText");
                vm.comPort.RaisePropertyChanged("IsConnectedButtonText");
            }
            else
            {
                try
                {
                    mySerialPort.Close();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Error while closing");
                }
                vm.comPort.IsConnected = mySerialPort.IsOpen;
                vm.MainWin.Status = "";
                vm.comPort.RaisePropertyChanged("IsConnectedVis");
                vm.comPort.RaisePropertyChanged("IsConnectedText");
                vm.comPort.RaisePropertyChanged("IsConnectedButtonText");
            }
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (mySerialPort.IsOpen)
            {
                JObject commJSON = JObject.Parse(vm.comPort.comm);
                mySerialPort.WriteLine(vm.comPort.comm);
                vm.MainWin.Status = "Sending command " + commJSON["comm"].ToString();
            }
        }

        private void btnToEnd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveExperBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exportRAWbtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exportKoefBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void runExperBtn_Click(object sender, RoutedEventArgs e)
        {
            //send massa
            //send velos
            //send dist
            //home pos
            //wait for Ready
            //run
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
