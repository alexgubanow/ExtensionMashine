using controlExtension.ViewModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ZedGraph;

namespace controlExtension
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainViewModel Vm;
        private SerialPort mySerialPort;
        private DispatcherTimer dispatcherTimer;
        private Thread runExprThrd;
        private int endstopsState;

        public MainWindow()
        {
            Vm = new MainViewModel();
            InitializeComponent();
            DataContext = Vm;
            initComPort(1);
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
        }

        private void ApplyEffect(Window win)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
                objBlur.Radius = 4;
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
                overlayrect.Visibility = Visibility.Collapsed;
                overlayring.Visibility = Visibility.Collapsed;
            }));
        }

        

        private void updZed(ZedGraphControl zedGraph)
        {
            zedGraph.AxisChange();
            zedGraph.Invalidate();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Vm.MainWin.chkValue)
            {
                Thread newThread = new Thread(DoWork);
                newThread.Start();
            }
        }

        public void DoWork()
        {
            if (mySerialPort.IsOpen)
            {
                sendComm("hx711?");
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
            sendComm("hx711?");
        }

        private void autoGetCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Vm.MainWin.chkValue = (bool)autoGetCheckBox.IsChecked;
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


        private void comPorts_ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            Vm.comPort.avaibleComPorts = SerialPort.GetPortNames();
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
                Vm.comPort.IsConnected = mySerialPort.IsOpen;
                Vm.comPort.RaisePropertyChanged("IsConnectedVis");
                Vm.comPort.RaisePropertyChanged("IsConnectedText");
                Vm.comPort.RaisePropertyChanged("IsConnectedButtonText");
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
                Vm.comPort.IsConnected = mySerialPort.IsOpen;
                Vm.MainWin.Status = "";
                Vm.comPort.RaisePropertyChanged("IsConnectedVis");
                Vm.comPort.RaisePropertyChanged("IsConnectedText");
                Vm.comPort.RaisePropertyChanged("IsConnectedButtonText");
            }
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (mySerialPort.IsOpen)
            {
                JObject commJSON = JObject.Parse(Vm.comPort.comm);
                mySerialPort.WriteLine(Vm.comPort.comm);
                Vm.MainWin.Status = "Sending command " + commJSON["Comm"].ToString();
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
                Vm.exper = new exper();
                //home pos
                Dispatcher.Invoke(new Action(async () =>
                {
                    RAWdataPlot.graphPane.AddCurve("RAWdata", new double[1] { 0 }, new double[1] { 0 }, Color.Red, SymbolType.None);
                    miuPlot.graphPane.AddCurve("miu", new double[1] { 0 }, new double[1] { 0 }, Color.Blue, SymbolType.None);
                    await this.ShowMessageAsync("Wait for ready", "Are you ready?", MessageDialogStyle.Affirmative);
                    ready = 1;
                }));
                while (ready == 0) { }
                ready = 0;
                sendComm("mRun?", new string[2] { "speed", "dir" }, new string[2] { Vm.MainWin.Velosity.ToString(), "1" });
                //wait for Ready ->show messag box
                Dispatcher.Invoke(new Action(async () =>
                {
                    await this.ShowMessageAsync("Wait for ready", "Put load and click OK!", MessageDialogStyle.Affirmative);
                    ready = 1;
                }));
                while (ready == 0) { }
                ready = 0;
                Vm.MainWin.VisPlot = Visibility.Visible;
                //run
                sendComm("mRun?", new string[2] { "speed", "dir" }, new string[2] { Vm.MainWin.Velosity.ToString(), "0" });
                Vm.exper.tStart = DateTime.UtcNow;
                while (endstopsState != 1 && mySerialPort.IsOpen)
                {
                    sendComm("endStops?");
                    sendComm("hx711?");
                }
            }
        }
        private void runExperBtn_Click(object sender, RoutedEventArgs e)
        {
            Vm.MainWin.VisPlot = Visibility.Collapsed;
            runExprThrd = new Thread(RunExpr);
            runExprThrd.Start();
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            sendComm("mRun?", new string[1] { "speed" }, new string[1] { "0" });
            if(runExprThrd != null)
            {
                runExprThrd.Abort();
            }
        }

        private void rlsBtn_Click(object sender, RoutedEventArgs e)
        {
            sendComm("mRelease?");
            if (runExprThrd != null)
            {
                runExprThrd.Abort();
            }
        }
    }
}