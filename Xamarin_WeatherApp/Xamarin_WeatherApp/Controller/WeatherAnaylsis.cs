using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Xamarin_WeatherApp.Controller
{
    public class WeatherAnalysis
    {
        
        private enum TemperatureConditionWeightedValues
        {
            Hot = 85,
            warm = 85,
            moderate = 60,
            cool = 30,
            cold = 10,
        }


        private enum WeatherConditionWeightedValues
        {
            sunny = 85,
            mixed = 65,
            cloudy = 35,
            thunderstorm = 10,
            rainy = 0,
            undefined = 0
        }

        public int WindSpeed(string wind)
        {
            try
            {
                string pattern = @"\d+";
                return Convert.ToInt16(Regex.Match(wind, pattern));
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public static string KiteProbability(string currentTime, int currentWindSpeed, int currentTemperature, string currentWeatherConditions)
        {
            float mapVal = timeWeight(currentTime) + windWeight(currentWindSpeed) + tempWeight(currentTemperature) + conditionWeight(currentWeatherConditions);
            if (mapVal < 127.5)
            {
                string rHex = (mapVal * 2).ToString("x2");
                string gHex = (255).ToString("x2");
                string bHex = (0).ToString("x2");
                string rgb = $"#ff{rHex}{gHex}{bHex}";
                return rgb;
            }
            else if (mapVal > 127.5)
            {
                string rHex = (255).ToString("x2");
                string gHex = (255 - (2*(mapVal-127.5))).ToString("x2");
                string bHex = (0).ToString("x2");
                string rgb = $"#ff{rHex}{gHex}{bHex}";
                return rgb;
            }
            else
            {
                return "#ff00ff00";
            }
            

            int timeWeight(string time)
            {
                int weight;
                switch (time)
                {
                    case "06:00":
                        weight = 31;
                        break;
                    case "07:00":
                        weight = 31;
                        break;
                    case "08:00":
                        weight = 45;
                        break;
                    case "09:00":
                        weight = 63;
                        break;
                    case "10:00":
                        weight = 63;
                        break;
                    case "11:00":
                        weight = 63;
                        break;
                    case "12:00":
                        weight = 63;
                        break;
                    case "13:00":
                        weight = 63;
                        break;
                    case "14:00":
                        weight = 63;
                        break;
                    case "15:00":
                        weight = 63;
                        break;
                    case "16:00":
                        weight = 63;
                        break;
                    case "17:00":
                        weight = 63;
                        break;
                    case "18:00":
                        weight = 45;
                        break;
                    case "19:00":
                        weight = 31;
                        break;
                    case "20:00":
                        weight = 15;
                        break;
                    case "21:00":
                        weight = 0;
                        break;
                    case "23:00":
                        weight = 0;
                        break;
                    case "00:00":
                        weight = 0;
                        break;
                    default:
                        weight = 0;
                        break;
                }
                return weight;
            }


            int windWeight(int wind)
            {
                if (wind > 40)
                {
                    return 85;
                }
                else
                {
                    return ((85 / 40) * (wind));
                }
            }


            int tempWeight(int temp)
            {
                if (temp < 8)
                {
                    return (int)TemperatureConditionWeightedValues.cold;
                }
                else if (temp >= 8 && temp < 15)
                {
                    return (int)TemperatureConditionWeightedValues.cool;
                }
                else if (temp >= 15 && temp < 20)
                {
                    return (int)TemperatureConditionWeightedValues.moderate;
                }
                else if (temp >= 20 && temp < 25)
                {
                    return (int)TemperatureConditionWeightedValues.warm;
                }
                else
                {
                    return (int)TemperatureConditionWeightedValues.Hot;
                }
            }


            int conditionWeight(string conditions)
            {
                if (conditions.ToLower().Contains("sunny") || conditions.ToLower().Contains("clear"))
                {
                    return (int)WeatherConditionWeightedValues.sunny;
                }
                else if (conditions.ToLower().Contains("mix") && conditions.ToLower().Contains("sun") && conditions.ToLower().Contains("cloud"))
                {
                    return (int)WeatherConditionWeightedValues.mixed;
                }
                else if (conditions.ToLower().Contains("thunderstorm"))
                {
                    return (int)WeatherConditionWeightedValues.thunderstorm;
                }
                else if (conditions.ToLower().Contains("showers") || conditions.ToLower().Contains("rain"))
                {
                    return (int)WeatherConditionWeightedValues.rainy;
                }
                else if (conditions.ToLower().Contains("cloud") || conditions.ToLower().Contains("cloudy"))
                {
                    return (int)WeatherConditionWeightedValues.cloudy;
                }
                else
                {
                    return (int)WeatherConditionWeightedValues.undefined;
                }
            }
        }
    }
}
