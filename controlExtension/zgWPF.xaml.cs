using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ZedGraph;

namespace controlExtension
{
    /// <summary>
    /// Interaction logic for zgWPF.xaml
    /// </summary>
    public partial class zgWPF : UserControl
    {
        public zgWPF() : base()
        {
            InitializeComponent();
        }

        #region graphPane

        [Category("GraphPane"), Description("Set the title of graph pane")]
        public GraphPane graphPane { get { return (GraphPane)GetValue(graphPaneProperty); } set { SetValue(graphPaneProperty, value); } }

        public static readonly DependencyProperty graphPaneProperty = DependencyProperty.Register("graphPane", typeof(GraphPane), typeof(zgWPF), new FrameworkPropertyMetadata(new GraphPane(), OngraphPanePropertyChanged));

        private static void OngraphPanePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            zgWPF control = source as zgWPF;
            control.graphPane = (GraphPane)e.NewValue;
        }

        //[Category("GraphPane"), Description("Set the title of graph pane")]
        //public string gpTitle { get { return (string)GetValue(gpTitleProperty); } set { SetValue(gpTitleProperty, value); } }

        //public static readonly DependencyProperty gpTitleProperty = DependencyProperty.Register("gpTitle", typeof(string), typeof(zgWPF), new FrameworkPropertyMetadata("", OngpTitlePropertyChanged));

        //private static void OngpTitlePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        //{
        //    zgWPF control = source as zgWPF;
        //    control.graphPane.Title.Text = (string)e.NewValue;
        //}

        //[Category("GraphPane"), Description("Enable X axis zero line")]
        //public bool IsFontsScaled { get { return (bool)GetValue(IsFontsScaledProperty); } set { SetValue(IsFontsScaledProperty, value); } }

        //public static readonly DependencyProperty IsFontsScaledProperty = DependencyProperty.Register("IsFontsScaled", typeof(bool), typeof(zgWPF), new FrameworkPropertyMetadata(true, OnIsFontsScaledPropertyChanged));

        //private static void OnIsFontsScaledPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        //{
        //    zgWPF control = source as zgWPF;
        //    control.graphPane.IsFontsScaled = (bool)e.NewValue;
        //}

        //[Category("GraphPane"), Description("Enable X axis zero line")]
        //public CurveList plotList { get { return (CurveList)GetValue(plotListProperty); } set { SetValue(plotListProperty, value); } }

        //public static readonly DependencyProperty plotListProperty = DependencyProperty.Register("plotList", typeof(bool), typeof(zgWPF), new FrameworkPropertyMetadata(true, OnplotListPropertyChanged));

        //private static void OnplotListPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        //{
        //    zgWPF control = source as zgWPF;
        //    control.graphPane.CurveList = (CurveList)e.NewValue;
        //}
        #endregion
        //#region Xaxis
        //[Category("XAxis"), Description("Enable X axis zero line")]
        //public bool IsZeroLineX { get { return (bool)GetValue(IsZeroLineXProperty); } set { SetValue(IsZeroLineXProperty, value); } }

        //public static readonly DependencyProperty IsZeroLineXProperty = DependencyProperty.Register("IsZeroLineX", typeof(bool), typeof(zgWPF), new FrameworkPropertyMetadata(true, OnIsZeroLineXPropertyChanged));

        //private static void OnIsZeroLineXPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        //{
        //    zgWPF control = source as zgWPF;
        //    control.graphPane.XAxis.MajorGrid.IsZeroLine = (bool)e.NewValue;
        //}

        //[Category("XAxis"), Description("Set the title of XAxis")]
        //public string XAxisTitle { get { return (string)GetValue(XAxisTitleProperty); } set { SetValue(XAxisTitleProperty, value); } }

        //public static readonly DependencyProperty XAxisTitleProperty = DependencyProperty.Register("XAxisTitle", typeof(string), typeof(zgWPF), new FrameworkPropertyMetadata("X", OnXAxisTitlePropertyChanged));

        //private static void OnXAxisTitlePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        //{
        //    zgWPF control = source as zgWPF;
        //    control.graphPane.XAxis.Title.Text = (string)e.NewValue;
        //}
        //#endregion
        //#region Yaxis
        //[Category("YAxis"), Description("Enable Y axis zero line")]
        //public bool IsZeroLineY { get { return (bool)GetValue(IsZeroLineYProperty); } set { graphPane.YAxis.MajorGrid.IsZeroLine = value; SetValue(IsZeroLineYProperty, value); } }

        //public static readonly DependencyProperty IsZeroLineYProperty = DependencyProperty.Register("IsZeroLineY", typeof(bool), typeof(zgWPF), new FrameworkPropertyMetadata(true, OnIsZeroLineYPropertyChanged));

        //private static void OnIsZeroLineYPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        //{
        //    zgWPF control = source as zgWPF;
        //    control.graphPane.YAxis.MajorGrid.IsZeroLine = (bool)e.NewValue;
        //}

        //[Category("YAxis"), Description("Set the title of graph pane")]
        //public string YAxisTitle { get { return (string)GetValue(YAxisTitleProperty); } set { SetValue(YAxisTitleProperty, value); } }

        //public static readonly DependencyProperty YAxisTitleProperty = DependencyProperty.Register("YAxisTitle", typeof(string), typeof(zgWPF), new FrameworkPropertyMetadata("Y", OnYAxisTitlePropertyChanged));

        //private static void OnYAxisTitlePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        //{
        //    zgWPF control = source as zgWPF;
        //    control.graphPane.YAxis.Title.Text = (string)e.NewValue;
        //}
        //#endregion

        private void parent_Loaded(object sender, RoutedEventArgs e)
        {
            if (ZedGraphPlot.DataBindings.Count < 1)
            {
                ZedGraphPlot.DataBindings.Add(new System.Windows.Forms.Binding("GraphPane", this, "graphPane", false, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            }
        }
    }
}