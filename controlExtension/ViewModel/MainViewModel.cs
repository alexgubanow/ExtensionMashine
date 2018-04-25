using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System.Collections.Generic;
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
            RAWdata = new RAWdata();
            koef = new koef();
            exper = new exper();
        }

        public MainWin MainWin { get; set; }
        public comPort comPort { get; set; }
        public MainBoard MainBoard { get; set; }
        public hx711 hx711 { get; set; }
        public RAWdata RAWdata { get; set; }
        public koef koef { get; set; }
        public exper exper { get; set; }
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

    [JsonObject(MemberSerialization.OptIn)]
    public class exper
    {
        [JsonProperty]
        public double massa { get; set; }
        [JsonProperty]
        public double speed { get; set; }
        [JsonProperty]
        public double dT { get; set; }
        [JsonProperty]
        public List<double> rawData { get; set; }
        [JsonProperty]
        public List<double> miu { get; set; }
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