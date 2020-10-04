using Prism.Mvvm;
using OxyPlot;
using OxyPlot.Series;
using Prism.Events;
using Events;
using System.Linq;
using Prism.Commands;
using System;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using OxyPlot.Axes;

namespace ExtensionControl.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IEventAggregator _ea;
        private int t = 0;
        private bool isRunning = false;

        private IList<DataPoint> _HX711RawData = new List<DataPoint>();
        public IList<DataPoint> HX711RawData
        {
            get { return _HX711RawData; }
            set { SetProperty(ref _HX711RawData, value); }
        }
        private PlotModel _HX711RawValuePlotModel = new PlotModel { Title = "HX711 Raw data" };
        public PlotModel HX711RawValuePlotModel
        {
            get { return _HX711RawValuePlotModel; }
            set { SetProperty(ref _HX711RawValuePlotModel, value); }
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set { SetProperty(ref _isConnected, value); }
        }

        private string _StartStoptxt = "Start";
        public string StartStoptxt
        {
            get { return _StartStoptxt; }
            set { SetProperty(ref _StartStoptxt, value); }
        }
        private string _title = "Extension Machine Control";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DelegateCommand _StartStopCMD;
        public DelegateCommand StartStopCMD => _StartStopCMD ??= new DelegateCommand(() =>
        {
            if (!isRunning)
            {
                StartStoptxt = "Stop";
                getHX711valueTimer.Start();
                isRunning = true;
            }
            else
            {
                StartStoptxt = "Start";
                getHX711valueTimer.Stop();
                isRunning = false;
            }
        });
        private DispatcherTimer getHX711valueTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 100) };
        public MainWindowViewModel(IEventAggregator ea)
        {
            _ea = ea;
            getHX711valueTimer.Tick += new EventHandler(getHX711valueTimer_Tick);
            _ea.GetEvent<ConnectEvent>().Subscribe((value) => IsConnected = value);
            _ea.GetEvent<ConnectEvent>().Subscribe((value) =>
            {
                if (!value && isRunning)
                {
                    StartStoptxt = "Start";
                    getHX711valueTimer.Stop();
                    isRunning = false;
                }
            });
            _ea.GetEvent<NewHX711valueEvent>().Subscribe((value) => AddNewHX711Value(value)); 
            HX711RawValuePlotModel.Axes.Add(new LinearAxis() { 
                Position = AxisPosition.Bottom, Title = "Sample number", MinorGridlineStyle = LineStyle.Dash,
                MajorGridlineStyle = LineStyle.Solid
            });
            HX711RawValuePlotModel.Axes.Add(new LinearAxis() {
                Position = AxisPosition.Left, Title = "Sample value", MinorGridlineStyle = LineStyle.Dash,
                MajorGridlineStyle = LineStyle.Solid });
            HX711RawValuePlotModel.Series.Add(new LineSeries() { ItemsSource = HX711RawData });
        }
        private void getHX711valueTimer_Tick(object Sender, EventArgs e)
        {
            _ea.GetEvent<GetHX711RawDataEvent>().Publish();
        }
        private void AddNewHX711Value(int val)
        {
            HX711RawData.Add(new DataPoint(t, val / 10000.0));
            t++;
            HX711RawValuePlotModel.InvalidatePlot(true);
        }
    }
}
