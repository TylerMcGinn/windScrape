using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using chartDataEntry = Microcharts.Entry;
using Microcharts;
using Microcharts.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SkiaSharp;

namespace Xamarin_WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class chartView : ContentPage
    {
        
        public string title;
        List<chartDataEntry> chartData = new List<chartDataEntry>();
        
        //private ObservableCollection<WeatherProperties> notifiedChartData = new ObservableCollection<WeatherProperties>();
        //public event PropertyChangedEventHandler PropertyChanged;
        public chartView()
        {
            InitializeComponent();
            Scrape scrape = new Scrape();
            title = "Daily";
            chartRefreshButton.Clicked += ChartRefreshButton_Clicked;
            //scrape.DownloadComplete += Scrape_DownloadComplete;
            Lists.masterList.CollectionChanged += MasterList_CollectionChanged;
            
            var chart = new BarChart() { Entries = chartData };
            chart1.Chart = chart;
        }

        private void MasterList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            getChartData();
        }

        //private void Scrape_DownloadComplete(object source, EventArgs args)
        //{
        //    noDataLabel.Text = "fired";
        //}





        private void getChartData()
        {
            chartData.Clear();
            foreach (var item in Lists.masterList)
            {
                if (item.Temperature < -20 && item.Temperature > -40)
                {
                    int mapVal = ((255 / 20) * (item.Temperature + 40));
                    string rHex = (0).ToString("x2");
                    string gHex = (255 - mapVal).ToString("x2");
                    string bHex = (255).ToString("x2");
                    string rgb = $"#ff{rHex}{gHex}{bHex}";
                    chartData.Add(new chartDataEntry((float)item.Temperature)
                    {
                        Label = item.Time,
                        ValueLabel = item.Temp,
                        Color = SKColor.Parse(rgb),
                    });
                }
                else if (item.Temperature < 0 && item.Temperature > -20)
                {
                    int mapVal = ((255 / 20) * (item.Temperature + 20));
                    string rHex = (0).ToString("x2");
                    string gHex = (255).ToString("x2");
                    string bHex = (255 - (255 - mapVal)).ToString("x2");
                    string rgb = $"#ff{rHex}{gHex}{bHex}";
                    chartData.Add(new chartDataEntry((float)item.Temperature)
                    {
                        Label = item.Time,
                        ValueLabel = item.Temp,
                        Color = SKColor.Parse(rgb),
                    });
                }
                else if (item.Temperature > 0 && item.Temperature <= 20)
                {
                    int mapVal = ((255 / 20) * (item.Temperature));
                    string rHex = (255 - (255 - mapVal)).ToString("x2");
                    string gHex = (255).ToString("x2");
                    string bHex = (0).ToString("x2");
                    string rgb = $"#ff{rHex}{gHex}{bHex}";
                    chartData.Add(new chartDataEntry((float)item.Temperature)
                    {
                        Label = item.Time,
                        ValueLabel = item.Temp,
                        Color = SKColor.Parse(rgb),
                    });
                }
                else if (item.Temperature > 20 && item.Temperature <= 40)
                {
                    int mapVal = ((255 / 20) * (item.Temperature - 20));
                    string rHex = (255).ToString("x2");
                    string gHex = (255 - mapVal).ToString("x2");
                    string bHex = (0).ToString("x2");
                    string rgb = $"#ff{rHex}{gHex}{bHex}";
                    Console.WriteLine(mapVal);
                    chartData.Add(new chartDataEntry((float)item.Temperature)
                    {
                        Label = item.Time,
                        ValueLabel = item.Temp,
                        Color = SKColor.Parse(rgb),
                        TextColor = SKColor.Parse(rgb)
                    });
                }
            }
            var chart = new LineChart() { Entries = chartData , LineMode=LineMode.Straight, PointMode=PointMode.Circle, BackgroundColor = SKColor.Parse("107c7c7c"), LabelTextSize = 30};
            chart1.Chart = chart;
            
        }
        //public void passedData()
        //{
        //    foreach (var item in Lists.masterList)
        //    {
        //        chartData.Add(new chartDataEntry((float)item.Temperature)
        //        {
        //            Label = item.Time,
        //            ValueLabel = item.Temp,
        //            Color = SKColor.Parse("#777777")
        //        });
        //        //TODO:need to create an event to fire once the masterlist has been populated, then a method on this page should update the chart data.
        //    }
        //}
        private void ChartRefreshButton_Clicked(object sender, EventArgs e)
        {

            //chartData.Add(new chartDataEntry(20)
            //{
            //    Label = "20",
            //    ValueLabel = "20",
            //    Color = SKColor.Parse("#777777")
            //});
            //chartData.Add(new chartDataEntry(40)
            //{
            //    Label = "40",
            //    ValueLabel = "40",
            //    Color = SKColor.Parse("#777777")
            //});
            //chartData.Add(new chartDataEntry(60)
            //{
            //    Label = "60",
            //    ValueLabel = "60",
            //    Color = SKColor.Parse("#777777")
            //});
            
            foreach (var item in Lists.masterList)
            {
                
                if(item.Temperature < 0 && item.Temperature > -20)
                {
                    int mapVal = ((255 / 20) * (-1*item.Temperature + 20));
                    string rHex = (0).ToString("x2");
                    string gHex = (255).ToString("x2");
                    string bHex = (255 - (255 - mapVal)).ToString("x2");
                    string rgb = $"#ff{rHex}{gHex}{bHex}";
                    chartData.Add(new chartDataEntry((float)item.Temperature)
                    {
                        Label = item.Time,
                        ValueLabel = item.Temp,
                        Color = SKColor.Parse(rgb),
                    });
                }
                else if(item.Temperature > 0 && item.Temperature <= 20)
                {
                    int mapVal = ((255 / 20) * (item.Temperature));
                    string rHex = (255 - (255 - mapVal)).ToString("x2");
                    string gHex = (255).ToString("x2");
                    string bHex = (0).ToString("x2");
                    string rgb = $"#ff{rHex}{gHex}{bHex}";
                    chartData.Add(new chartDataEntry((float)item.Temperature)
                    {
                        Label = item.Time,
                        ValueLabel = item.Temp,
                        Color = SKColor.Parse(rgb),
                    });
                }
                else if (item.Temperature > 20 && item.Temperature <= 40)
                {
                    int mapVal = ((255 / 20) * (item.Temperature - 20));
                    string rHex = (255).ToString("x2");
                    string gHex = (255 -mapVal).ToString("x2");
                    string bHex = (0).ToString("x2");
                    string rgb = $"#ff{rHex}{gHex}{bHex}";
                    Console.WriteLine(mapVal);
                    chartData.Add(new chartDataEntry((float)item.Temperature)
                    {
                        Label = item.Time,
                        ValueLabel = item.Temp,
                        Color = SKColor.Parse(rgb),
                    });
                }
            }
            var chart = new PointChart() { Entries = chartData };
            chart1.Chart = chart;
        }
    }
}