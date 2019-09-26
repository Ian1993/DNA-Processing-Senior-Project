using LiveCharts;
using LiveCharts.Defaults;
namespace windows
{
    class Class1
    {
        public ChartValues<ObservablePoint> ValuesA { get; set; }
        public ChartValues<ObservablePoint> ValuesB{ get; set; }
        public Class1(ChartValues<ObservablePoint> A, ChartValues<ObservablePoint> B)
        {
           ValuesA = A;
           ValuesB = B;
           
        }

        public void Start()
        {
            for (int i = 0; i < 60; i++)
            {
                ValuesA.Add(new ObservablePoint(i, i * 0.11));
            }
            for (int i = 0; i < 60; i++)
            {
                ValuesB.Add(new ObservablePoint(i, i * 2));
            }
        }
    }
}
