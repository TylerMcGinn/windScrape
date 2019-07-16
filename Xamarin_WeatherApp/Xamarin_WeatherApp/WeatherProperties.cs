using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin_WeatherApp
{
    public class WeatherProperties
    {
        public string Time { get; set; }
        public int Temperature { get; set; }
        public string Temp { get; set; }
        public string Conditions { get; set; }
        public string Precipitation { get; set; }
        public string Wind { get; set; }
        public string Icon { get; set; }
        public Color backgroundColor { get; set; }
        public override string ToString()
        {
            return $"Time:{Time}, Temperature:{Temperature}, Conditions:{Conditions}, Precipitation:{Precipitation}, Wind:{Wind}";
        }
        //public string tempString { get => this.Temperature.ToString(); }
    }
}
