using GalaSoft.MvvmLight;
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
        }

        public MainWin MainWin { get; set; }
    }

    public class MainWin : INotifyPropertyChanged
    {
        public MainWin()
        {
            text1 = "13";
        }

        private string _text1;

        /// <summary>
        /// Sets and gets the  property.
        /// Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string text1
        {
            get
            {
                return _text1;
            }

            set
            {
                if (_text1 == value)
                {
                    return;
                }

                _text1 = value;
                RaisePropertyChanged(text1);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}