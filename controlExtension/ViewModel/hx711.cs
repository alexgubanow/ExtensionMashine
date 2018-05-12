using System.ComponentModel;

namespace controlExtension.ViewModel
{
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