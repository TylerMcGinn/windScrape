using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Xamarin_WeatherApp
{
    public static class Lists
    {
        public static List<string> timeDataList(List<string> list)
        {
            string pattern1 = ">(.+)<";
            string pattern2 = ">|<";
            List<string> timeList = new List<string>();

            var timeData = list.Where(x => x.Contains("header1"));
            List<string> dataList = new List<string>(timeData);
            dataList.Remove(dataList.First());
            foreach (var line in dataList)
            {
                var matches = Regex.Matches(line, pattern1);
                foreach (var match in matches)
                {
                    timeList.Add(Regex.Replace(match.ToString(), pattern2, String.Empty));
                }
            }
            return timeList;
        }

        public static List<int> temperatureDataList(List<string> list)
        {
            string pattern1 = ">(.+)<";
            string pattern2 = ">|<";
            List<int> tempList = new List<int>();

            var tempData = list.Where(x => x.Contains("header2"));
            List<string> dataList = new List<string>(tempData);
            dataList.Remove(dataList.First());
            foreach (var line in dataList)
            {
                var matches = Regex.Matches(line, pattern1);
                foreach (var match in matches)
                {
                    tempList.Add(Convert.ToInt16(Regex.Replace(match.ToString(), pattern2, String.Empty)));
                }
            }
            return tempList;
        }

        public static List<string> conditionDataList(List<string> list)
        {
            string pattern1 = "<p>(.+)</p>";
            string pattern2 = "<p>|</p>";
            List<string> conditionList = new List<string>();

            var conditionData = list.Where(x => x.Contains("weathericons"));
            List<string> dataList = new List<string>(conditionData);
            foreach (var line in dataList)
            {
                var matches = Regex.Matches(line, pattern1);
                foreach (var match in matches)
                {
                    conditionList.Add(Regex.Replace(match.ToString(), pattern2, String.Empty));
                }
            }
            return conditionList;
        }

        public static List<string> precipitationDataList(List<string> list)
        {
            string pattern1 = ">(.+)<";
            string pattern2 = ">|<";
            List<string> precipList = new List<string>();

            var precipData = list.Where(x => x.Contains("header4"));
            List<string> dataList = new List<string>(precipData);
            dataList.Remove(dataList.First());
            foreach (var line in dataList)
            {
                var matches = Regex.Matches(line, pattern1);
                foreach (var match in matches)
                {
                    precipList.Add(Regex.Replace(match.ToString(), pattern2, String.Empty));
                }
            }
            return precipList;
        }

        public static List<string> windDataList(List<string> list)
        {
            string pattern1 = ">(.+)<";
            string pattern2 = ">|<";
            string pattern3 = @"/(\w+)";
            List<string> windList = new List<string>();

            var windData = list.Where(x => x.ToLower().Contains("north") || x.ToLower().Contains("south") || x.ToLower().Contains("east") || x.ToLower().Contains("west") || x.ToLower().Contains("variable") || x.ToLower().Contains("calm"));
            List<string> dataList = new List<string>(windData);
            foreach (var line in dataList)
            {
                var matches = Regex.Matches(line, pattern1);
                foreach (var match in matches)
                {
                    var prefilter = Regex.Replace(match.ToString(), pattern2, String.Empty);
                    windList.Add(Regex.Replace(prefilter.ToString(), pattern3, String.Empty));
                }
            }
            return windList;
        }

        public static ObservableCollection<WeatherProperties> masterList = new ObservableCollection<WeatherProperties>();
    }
}
