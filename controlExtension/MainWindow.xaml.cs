using controlExtension.ViewModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
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
        private Thread runExprThrd;
        private Thread UpdZedThrd;
        private int endstopsState;

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

        private void ApplyEffect(Window win)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
                objBlur.Radius = 4;
                //mngr.Visibility = Visibility.Collapsed;
                overlayrect.Visibility = Visibility.Visible;
                overlayring.Visibility = Visibility.Visible;
                mainPanel.Effect = objBlur;
                mainPanel.IsEnabled = false;
            }));
        }

        private void ClearEffect(Window win)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                mainPanel.IsEnabled = true;
                mainPanel.Effect = null;
                //mngr.Visibility = Visibility.Visible;
                overlayrect.Visibility = Visibility.Collapsed;
                overlayring.Visibility = Visibility.Collapsed;
            }));
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

                        case "endStops?":
                            endstopsState = Convert.ToInt32(inJSON["endStops"]);
                            vm.MainWin.Status = "Receive endstops State command \"endStops?\" = " + endstopsState;
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

        private enum direction
        {
            forward = 0,
            backward = 1
        };
        int ready = 0;
        private void RunExpr()
        {
            if (mySerialPort.IsOpen)
            {
                //clear exper
                vm.exper = new exper();
                //home pos
                Dispatcher.Invoke(new Action(async () =>
                {
                    await this.ShowMessageAsync("Wait for ready", "Are you ready?", MessageDialogStyle.Affirmative);
                    ready = 1;
                }));
                while (ready == 0){}
                ready = 0;
                mySerialPort.WriteLine("{\"comm\":\"mRun?\",\"speed\":" + vm.MainWin.Velosity + ",\"dir\":" + direction.backward + "}");
                vm.MainWin.Status = "Sending command \"mRun?\",\"speed\":" + vm.MainWin.Velosity + ",\"dir\":" + direction.backward + "}";
                //wait for Ready ->show messag box
                Dispatcher.Invoke(new Action(async () =>
                {
                    await this.ShowMessageAsync("Wait for ready", "Put load and click OK!", MessageDialogStyle.Affirmative);
                    ready = 1;
                }));
                while (ready == 0){}
                ready = 0;
                vm.MainWin.SelTab = 0;
                //run
                mySerialPort.WriteLine("{\"comm\":\"mRun?\",\"speed\":" + vm.MainWin.Velosity + ",\"dir\":" + direction.forward + "}");
                vm.MainWin.Status = "Sending command \"mRun?\",\"speed\":" + vm.MainWin.Velosity + ",\"dir\":" + direction.forward + "}";
                while (endstopsState == 0)
                {
                    mySerialPort.WriteLine("{\"comm\":\"endStops?\"}");
                    mySerialPort.WriteLine("{\"comm\":\"hx711?\"}");
                }
            }
        }

        private void runExperBtn_Click(object sender, RoutedEventArgs e)
        {
            vm.MainWin.SelTab = 2;
            runExprThrd = new Thread(RunExpr);
            runExprThrd.Start();
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}