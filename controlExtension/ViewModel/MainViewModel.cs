using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Windows;
using ZedGraph;

namespace controlExtension.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            MainWin = new MainWin();
            comPort = new comPort();
            MainBoard = new MainBoard();
            hx711 = new hx711();
            RAWdata = new RAWdata();
            koef = new koef();
        }

        public MainWin MainWin { get; set; }
        public comPort comPort { get; set; }
        public MainBoard MainBoard { get; set; }
        public hx711 hx711 { get; set; }
        public RAWdata RAWdata { get; set; }
        public koef koef { get; set; }
    }

    public class MainWin : INotifyPropertyChanged
    {
        public MainWin()
        {
            Status = "";
            chkValue = false;
        }

        private string _Status;
        public string Status { get { return _Status; } set { _Status = value; RaisePropertyChanged("Status"); } }

        private bool _chkValue;

        public bool chkValue { get { return _chkValue; } set { _chkValue = value; RaisePropertyChanged("chkValue"); } }

        private double _massa;

        public double massa { get { return _massa; } set { _massa = value; RaisePropertyChanged("massa"); } }

        private int _Velosity;

        public int Velosity { get { return _Velosity; } set { _Velosity = value; RaisePropertyChanged("Velosity"); } }

        private int _Distanse;

        public int Distanse { get { return _Distanse; } set { _Distanse = value; RaisePropertyChanged("Distanse"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RAWdata : INotifyPropertyChanged
    {
        public RAWdata()
        {
            graphPane = new GraphPane();
            graphPane.Title.Text = "RAW data";
            graphPane.XAxis.Title.Text = "time";
            graphPane.YAxis.Title.Text = "Y";
            graphPane.XAxis.Scale.FontSpec.Size = 15;
            graphPane.YAxis.Scale.FontSpec.Size = 15;
            graphPane.XAxis.Title.FontSpec.Size = 15;
            graphPane.YAxis.Title.FontSpec.Size = 15;
            graphPane.Legend.FontSpec.Size = 15;
            graphPane.Title.FontSpec.Size = 15;
            graphPane.IsFontsScaled = false;
            graphPane.Border.IsVisible = false;
            graphPane.XAxis.MajorGrid.IsZeroLine = false;
            graphPane.YAxis.MajorGrid.IsZeroLine = false;
            graphPane.IsBoundedRanges = false;
        }

        private GraphPane _graphPane;

        public GraphPane graphPane
        {
            get { return _graphPane; }
            set { _graphPane = value; RaisePropertyChanged("graphPane"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class koef : INotifyPropertyChanged
    {
        public koef()
        {
            graphPane = new GraphPane();
            graphPane.Title.Text = "koef";
            graphPane.XAxis.Title.Text = "time";
            graphPane.YAxis.Title.Text = "Y";
            graphPane.XAxis.Scale.FontSpec.Size = 15;
            graphPane.YAxis.Scale.FontSpec.Size = 15;
            graphPane.XAxis.Title.FontSpec.Size = 15;
            graphPane.YAxis.Title.FontSpec.Size = 15;
            graphPane.Legend.FontSpec.Size = 15;
            graphPane.Title.FontSpec.Size = 15;
            graphPane.IsFontsScaled = false;
            graphPane.Border.IsVisible = false;
            graphPane.XAxis.MajorGrid.IsZeroLine = false;
            graphPane.YAxis.MajorGrid.IsZeroLine = false;
            graphPane.IsBoundedRanges = false;
        }

        private GraphPane _graphPane;

        public GraphPane graphPane
        {
            get { return _graphPane; }
            set { _graphPane = value; RaisePropertyChanged("graphPane"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class comPort : INotifyPropertyChanged
    {
        public comPort()
        {
            answer = "";
            comm = "{\"comm\":\"STAT?\"}";
            IsConnected = false;
            IsConnectedText = "";
            IsConnectedButtonText = "";
            avaibleComPorts = new string[0];
        }

        private bool _IsConnected;

        public bool IsConnected { get { return _IsConnected; } set { _IsConnected = value; RaisePropertyChanged("IsConnected"); } }

        public Visibility IsConnectedVis { get { return IsConnected ? Visibility.Visible : Visibility.Collapsed; } set { IsConnectedVis = (!_IsConnected ? Visibility.Visible : Visibility.Collapsed); RaisePropertyChanged("IsConnectedVis"); } }

        private string _IsConnectedText;

        public string IsConnectedText { get { return _IsConnected ? "Connected" : "Disconnected"; } set { _IsConnectedText = (!_IsConnected ? "Connected" : "Disconnected"); RaisePropertyChanged("IsConnectedText"); } }

        private string _IsConnectedButtonText;

        public string IsConnectedButtonText { get { return _IsConnected ? "Disconnect" : "Connect"; } set { _IsConnectedButtonText = (!_IsConnected ? "Disconnect" : "Connect"); RaisePropertyChanged("IsConnectedButtonText"); } }

        private string[] _avaibleComPorts;
        public string[] avaibleComPorts { get { return _avaibleComPorts; } set { _avaibleComPorts = value; RaisePropertyChanged("avaibleComPorts"); } }

        private string _currComPort;
        public string currComPort { get { return _currComPort; } set { _currComPort = value; RaisePropertyChanged("currComPort"); } }

        private string _comm;
        public string comm { get { return _comm; } set { _comm = value; RaisePropertyChanged("comm"); } }

        private string _answer;
        public string answer { get { return _answer; } set { _answer = value; RaisePropertyChanged("answer"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MainBoard : INotifyPropertyChanged
    {
        public MainBoard()
        {
            Status = "";
        }

        private string _Status;
        public string Status { get { return _Status; } set { _Status = value; RaisePropertyChanged("Status"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class hx711 : INotifyPropertyChanged
    {
        public hx711()
        {
            Value = "";
        }

        private string _Value;
        public string Value { get { return _Value; } set { _Value = value; RaisePropertyChanged("Value"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}