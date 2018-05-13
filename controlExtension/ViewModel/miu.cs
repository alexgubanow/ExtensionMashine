using System.ComponentModel;
using ZedGraph;

namespace controlExtension.ViewModel
{
    public class miu : INotifyPropertyChanged
    {
        public miu()
        {
            graphPane = new GraphPane();
            graphPane.Title.Text = "miu";
            graphPane.XAxis.Title.Text = "time";
            graphPane.YAxis.Title.Text = "Y";
            graphPane.XAxis.Scale.FontSpec.Size = 15;
            graphPane.YAxis.Scale.FontSpec.Size = 15;
            graphPane.XAxis.Title.FontSpec.Size = 15;
            graphPane.YAxis.Title.FontSpec.Size = 15;
            graphPane.Legend.FontSpec.Size = 15;
            graphPane.Title.FontSpec.Size = 15;
            graphPane.IsFontsScaled = false;
            graphPane.Border.IsVisible = false;
            graphPane.XAxis.MajorGrid.IsZeroLine = false;
            graphPane.YAxis.MajorGrid.IsZeroLine = false;
            graphPane.IsBoundedRanges = false;
        }

        private GraphPane _graphPane;

        public GraphPane graphPane
        {
            get { return _graphPane; }
            set { _graphPane = value; RaisePropertyChanged("graphPane"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}