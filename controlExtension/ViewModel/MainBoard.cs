using System.ComponentModel;

namespace controlExtension.ViewModel
{
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
}