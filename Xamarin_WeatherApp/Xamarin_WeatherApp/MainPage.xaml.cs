using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;
using System.Collections.ObjectModel;

namespace Xamarin_WeatherApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {

        Scrape webScraper = new Scrape();
        public delegate void myDel(string message);
        ObservableCollection<WeatherProperties> listData = new ObservableCollection<WeatherProperties>();

        ObservableCollection<WeatherProperties> initialList = new ObservableCollection<WeatherProperties>();
        //List<WeatherProperties> newList = new List<WeatherProperties>();
        public MainPage()
        {
            InitializeComponent();
            refreshButton.Clicked += onRefreshButtonClicked;
            getData();
            //populateList();
            //BindingContext = listData;
        }



        //private void WeatherList_BindingContextChanged(object sender, EventArgs e)
        //{
        //    if(BindingContext != null)
        //    {

        //    }
        //}

        private  void onRefreshButtonClicked(object sender, EventArgs e)
        {
            //getData();
            //listData.Clear();
            //populateList();
            listData.Add(new WeatherProperties() {  });
        }

        private async void getData()
        {
            myActivityIndicator.IsRunning = true;
            initialList = await webScraper.scrapeData(displayError);
            myActivityIndicator.IsRunning = false;
            populateList();
        }

        private async void populateList()
        {
            
            weatherList.ItemsSource = null;
            weatherList.ItemsSource = listData;
            foreach (var item in initialList)
            {
                await Task.Delay(200);
                listData.Add(item);
            }
        }

        public async void displayError(string message)
        {
            await DisplayAlert("Error", message, "ok");
        }

        //private async void ViewCell_Appearing(object sender, EventArgs e)
        //{
        //    var cell = (ViewCell)sender;
        //    await cell.View.TranslateTo(10, 0, 7, Easing.SinIn);
        //    //await cell.View.FadeTo(0, 500, Easing.Linear);
        //    //await cell.View.FadeTo(1, 500, Easing.SpringIn);
        //    ViewExtensions.CancelAnimations(cell.View);
        //}
        
    }
}

