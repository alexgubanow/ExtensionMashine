using System.ComponentModel;
using System.Windows;

namespace controlExtension.ViewModel
{
    public class comPort : INotifyPropertyChanged
    {
        public comPort()
        {
            answer = "";
            comm = "{\"Comm\":\"STAT?\"}";
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
}