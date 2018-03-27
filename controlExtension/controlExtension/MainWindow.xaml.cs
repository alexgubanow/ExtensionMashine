using controlExtension.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace controlExtension
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel vm;

        public MainWindow()
        {
            vm = new MainViewModel();
            InitializeComponent();
            DataContext = vm;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            int val = 0;
            if(!Int32.TryParse(vm.MainWin.text1, out val))
            { return; }
            txt2.Text = "Square = " + Math.Sqrt(val).ToString();
        }
    }
}
