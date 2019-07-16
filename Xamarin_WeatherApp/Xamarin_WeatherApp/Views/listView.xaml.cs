using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading;
using System.Collections.ObjectModel;
using Xamarin.Forms.Xaml;

namespace Xamarin_WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class listView : ContentPage
    {
        Scrape webScraper = new Scrape();

        ObservableCollection<WeatherProperties> listData = new ObservableCollection<WeatherProperties>();
        ObservableCollection<WeatherProperties> initialList = new ObservableCollection<WeatherProperties>();
        public string title;
        public listView()
        {
            InitializeComponent();
            title = "Hourly";
            //webScraper.DownloadComplete += WebScraper_DownloadComplete;
            refreshListBtn.Clicked += RefreshListBtn_Clicked;
            getData();
            //BindingContext = this;
            //populateList();
            //BindingContext = listData;
        }

        private void WebScraper_DownloadComplete(object source, EventArgs args)
        {
            DisplayAlert("Event Message", "event Fired", "ok");
        }

        private void RefreshListBtn_Clicked(object sender, EventArgs e)
        {
            initialList.Clear();
            listData.Clear();
            refreshListBtn.IsEnabled = false;
            getData();
            refreshListBtn.IsEnabled = true;
        }

        //private void WeatherList_BindingContextChanged(object sender, EventArgs e)
        //{
        //    if(BindingContext != null)
        //    {

        //    }
        //}

       

        private async void getData()
        {
            //myActivityIndicator.IsRunning = true;
            await Navigation.PushModalAsync(new splashScreen());
            initialList = await webScraper.scrapeData(displayError);
            await Navigation.PopModalAsync();
            //myActivityIndicator.IsRunning = false;
            populateList();
        }

        private async void populateList()
        {

            weatherList.ItemsSource = null;
            weatherList.ItemsSource = listData;
            foreach (var item in initialList)
            {
                try
                {

                    await Task.Delay(100);
                    listData.Add(item);
                }
                catch (Exception)
                {
                    
                }
            }
            listData.Add(new WeatherProperties());
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