using GalaSoft.MvvmLight;

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
}