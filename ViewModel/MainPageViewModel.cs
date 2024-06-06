using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Localizacao.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IMap map;
        private readonly IGeolocation geolocation;
        private readonly IConnectivity connectivity;

        public MainPageViewModel(IMap map, IGeolocation geolocation, IConnectivity connectivity)
        {
            this.map = map;
            this.geolocation = geolocation; 
            this.connectivity = connectivity;
        }

        [RelayCommand]
        public async Task CheckLocation()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Ops", "Você precisa de internet para isso, ok", "OK");
                return;
            }

            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Ops", "Você precisa de internet para isso, ok", "OK");
                return;
            }

            var location = await geolocation.GetLocationAsync(
                new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Best,
                    Timeout = TimeSpan.FromSeconds(30),
                    RequestFullAccuracy = true
                });


            if (location == null)
            {
                location = await geolocation.GetLastKnownLocationAsync();
                await Shell.Current.DisplayAlert("Ops", "Desculpa, nós não podemos buscar sua localização, ok", "OK");
                return;
            }

            await map.OpenAsync(location.Latitude, location.Longitude, new MapLaunchOptions
            {
                Name = "Minha localização atual",
                NavigationMode = NavigationMode.None
            });
        }
    }
}
