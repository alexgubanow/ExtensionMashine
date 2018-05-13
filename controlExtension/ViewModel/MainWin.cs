using System.ComponentModel;
using System.Windows;

namespace controlExtension.ViewModel
{
    public class MainWin : INotifyPropertyChanged
    {
        public MainWin()
        {
            Status = "";
            chkValue = false;
            VisPlot = Visibility.Visible;
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

        private Visibility _VisPlot;

        public Visibility VisPlot { get { return _VisPlot; } set { _VisPlot = value; RaisePropertyChanged("VisPlot"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}