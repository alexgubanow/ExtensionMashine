using System;
using System.Windows;
using System.Windows.Controls;

namespace controlExtension
{
    /// <summary>
    /// Interaction logic for numPowTen.xaml
    /// </summary>
    public partial class numPowTen : UserControl
    {
        public numPowTen() : base()
        {
            InitializeComponent();
        }
        
        public string Clabel { get { return (string)GetValue(ClabelProperty); } set { SetValue(ClabelProperty, value); } }

        public static readonly DependencyProperty ClabelProperty = DependencyProperty.Register("Clabel", typeof(string), typeof(numPowTen), new UIPropertyMetadata(""));

        public int PowTen { get { return (int)GetValue(PowTenProperty); } set { SetValue(PowTenProperty, value); } }

        public static readonly DependencyProperty PowTenProperty = DependencyProperty.Register("PowTen", typeof(int), typeof(numPowTen), new UIPropertyMetadata(1));

        public double num { get { return (int)GetValue(numProperty); } set { SetValue(numProperty, value); } }

        public static readonly DependencyProperty numProperty = DependencyProperty.Register("num", typeof(double), typeof(numPowTen), new UIPropertyMetadata(0.0));

        public double finalValue { get { return num * Math.Pow(10,PowTen); } set { } }

        public static readonly DependencyProperty finalValueProperty = DependencyProperty.Register("finalValue", typeof(double), typeof(numPowTen), new UIPropertyMetadata(0.0));
    }
}
