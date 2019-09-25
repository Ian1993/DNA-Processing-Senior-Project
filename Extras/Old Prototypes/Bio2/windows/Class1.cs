using LiveCharts;
using LiveCharts.Defaults;
namespace windows
{
    class Class1
    {
        public ChartValues<ObservablePoint> ValuesA { get; set; }
        public ChartValues<ObservablePoint> ValuesB { get; set; }
        public Class1(ChartValues<ObservablePoint> valA, ChartValues<ObservablePoint> valB)
        {
            ValuesA = valA;
            ValuesB = valB;
        }

        public void Stuff()
        {
            for (int i = 0; i < 60; i++)
            {
                ValuesA.Add(new ObservablePoint(i, i * 0.01));
            }

        }
    }
}
