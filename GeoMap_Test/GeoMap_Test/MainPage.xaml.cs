using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace GeoMap_Test {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        /* With this section, we'll implement the 
         GeoLocation API by Xamarin Essentials*/
        #region GeoLocation API
        private async void GetYourLocation_Clicked(object sender, EventArgs e) {
            try
            {
                var yourLocation = await Geolocation.GetLocationAsync(new GeolocationRequest()
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });

                if (yourLocation == null)
                    await DisplayAlert("Attention", "GPS is not available", "Ok");
                else
                    YourLocationLatitude.Text = $"{yourLocation.Latitude}";
                    YourLocationLongitude.Text = $"{yourLocation.Longitude}";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ooops!", $"Something went wrong: {ex.Message}", "Ok");
            }
        }
        #endregion


        /* With this section we'll implement the 
         Maps API by Xamarin Essentials*/
        #region Maps API
        private async void GoToMap_Clicked(object sender, EventArgs e) {
            try
            {
                await Map.OpenAsync(double.Parse(YourLocationLatitude.Text), double.Parse(YourLocationLongitude.Text), new MapLaunchOptions
                {
                    Name = "Your location",
                    NavigationMode = NavigationMode.None
                });
            }
            catch (Exception ex1)
            {
                await DisplayAlert("Ooops!", $"Remenber to search your coordinates, first.", "Ok");
            }
        }
        #endregion
    }
}
