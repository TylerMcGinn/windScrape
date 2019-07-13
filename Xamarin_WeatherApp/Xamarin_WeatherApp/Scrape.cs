using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace Xamarin_WeatherApp
{
    public class Scrape
    {
        public delegate void myDel(string errorMessage);
        public async Task<ObservableCollection<WeatherProperties>> scrapeData(myDel del)
        {
            string webUrl = @"https://weather.gc.ca/forecast/hourly/bc-84_metric_e.html";
            WebClient webClient = new WebClient();
            string rawHTML = await Task.Run(() => webClient.DownloadString(webUrl));
            return filterRawHTML(rawHTML, del);
        }

        public ObservableCollection<WeatherProperties> filterRawHTML(string raw, myDel del)
        {
            string[] separators = { "\r\n", "\r", "\n" };
            int rawlength = raw.Length;
            int tableStartIndex = raw.IndexOf("table", 0, rawlength);
            int tableEndIndex = raw.IndexOf("</table>", 0, rawlength);
            string tableString = raw.Substring(tableStartIndex + 18, (tableEndIndex - tableStartIndex) - 10);
            string[] tableArray = tableString.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            List<string> tableList = new List<string>(tableArray);

            List<string> timeList = Lists.timeDataList(tableList);
            List<int> tempList = Lists.temperatureDataList(tableList);
            List<string> conditionList = Lists.conditionDataList(tableList);
            List<string> precipList = Lists.precipitationDataList(tableList);
            List<string> windList = Lists.windDataList(tableList);
            List<string> temp2List = new List<string>();
            tempList.ForEach(item => temp2List.Add(item.ToString()));

            int listLength = timeList.Count;
            //Debug point
            //Console.WriteLine($"time:{timeList.Count}");
            //Console.WriteLine($"temp:{tempList.Count}");
            //Console.WriteLine($"cond:{conditionList.Count}");
            //Console.WriteLine($"precip:{precipList.Count}");
            //Console.WriteLine($"wind:{windList.Count}");
            
            for (int i = 0; i < listLength; i++)
            {
                try
                {
                    //Debug point
                    //Console.WriteLine(timeList[i]);
                    //Console.WriteLine(tempList[i]);
                    //Console.WriteLine(conditionList[i]);
                    //Console.WriteLine(precipList[i]);
                    //Console.WriteLine(windList[i]);

                    Lists.masterList.Add(new WeatherProperties() { Time = "Time: "+timeList[i], Temp = "Temp.: " + temp2List[i], Conditions = "Conditions: " + conditionList[i], Precipitation = "Precip.: " + precipList[i], Wind = "Wind: " + windList[i], Icon = setWeatherIcon(conditionList[i]) });
                }
                catch (Exception e)
                {
                    del($"{e.GetType()}:{e.Message}\n\n" +
                        $"List Counts\n" +
                        $"Time:{timeList.Count}\n" +
                        $"Temp:{tempList.Count}\n" +
                        $"Contition:{conditionList.Count}\n" +
                        $"Precipitation:{precipList.Count}\n" +
                        $"Wind:{windList.Count}");
                    //return Lists.masterList;
                }
            }
            Lists.masterList.Add(new WeatherProperties() { });
            return Lists.masterList;
        }

        public static string setWeatherIcon(string condition)
        {
            if (condition.ToLower() == "sunny" || condition.ToLower() == "clear")
            {
                return "daySunny.png";
            }
            else if (condition.ToLower().Contains("mix") && condition.ToLower().Contains("sun") && condition.ToLower().Contains("cloud"))
            {
                return "dayMixed.png";
            }
            else if (condition.ToLower().Contains("thunderstorm"))
            {
                return "dayThunderstorm.png";
            }
            else if (condition.ToLower().Contains("showers") || condition.ToLower().Contains("rain"))
            {
                return "dayRain.png";
            }
            else if (condition.ToLower().Contains("cloud"))
            {
                return "dayCloudy.png";
            }
            else
            {
                return "";
            }
        }

    }
}
