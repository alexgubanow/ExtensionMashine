using GalaSoft.MvvmLight;
using OxyPlot;
using OxyPlot.Series;
using System.ComponentModel;

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
        }

        public MainWin MainWin { get; set; }
        public comPort comPort { get; set; }
        public MainBoard MainBoard { get; set; }
        public hx711 hx711 { get; set; }
    }

    public class MainWin : INotifyPropertyChanged
    {
        public MainWin()
        {
            Status = "";
            chkValue = false;
            plotViewModel = new PlotModel();
            //for (int j = 0; j < linearModel.timeMoments[0].Nodes.Length; j++)
            //{
            //    var s1 = new LineSeries();
            //    s1.Title = "node" + j;
            //    s1.StrokeThickness = 1.2;
            //    s1.LineStyle = LineStyle.Solid;
            //    s1.Color = OxyColor.FromRgb(255, 0, 0);
            //    //s1.Color = OxyColor.FromRgb(41, 177, 255);
            //    //s1.RenderInLegend = false;
            //    for (int i = 0; i < counts; i++)
            //    {
            //        s1.Points.Add(new DataPoint(linearModel.time[i], linearModel.timeMoments[i].Nodes[j].derivatives.displ[0]));
            //    }
            //    plotViewModel.Series.Add(s1);
            //}
        }

        private string _Status;
        public string Status { get { return _Status; } set { _Status = value; RaisePropertyChanged("Status"); } }

        private bool _chkValue;

        public bool chkValue
        {
            get { return _chkValue; }
            set
            {
                _chkValue = value;
                RaisePropertyChanged("chkValue");
            }
        }

        private PlotModel _plotViewModel;

        public PlotModel plotViewModel
        {
            get => _plotViewModel;
            set
            {
                _plotViewModel = value;
                RaisePropertyChanged("plotViewModel");
                if ((plotViewModel.Series[0] as LineSeries).Points.Count > 100)
                {
                    (plotViewModel.Series[0] as LineSeries).Points.RemoveAt(0);
                }
            }
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

        public bool IsConnected
        {
            get { return _IsConnected; }
            set { _IsConnected = value; RaisePropertyChanged("IsConnected"); }
        }

        private string _IsConnectedText;

        public string IsConnectedText
        {
            get { return _IsConnected ? "Connected" : "Disconnected"; }
            set { _IsConnectedText = (!_IsConnected ? "Connected" : "Disconnected"); RaisePropertyChanged("IsConnectedText"); }
        }

        private string _IsConnectedButtonText;

        public string IsConnectedButtonText
        {
            get { return _IsConnected ? "Disconnect" : "Connect"; }
            set { _IsConnectedButtonText = (!_IsConnected ? "Disconnect" : "Connect"); RaisePropertyChanged("IsConnectedButtonText"); }
        }

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