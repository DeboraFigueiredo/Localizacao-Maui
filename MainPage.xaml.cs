using Localizacao.ViewModel;

namespace Localizacao
{
    public partial class MainPage : ContentPage
    {
     

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

    }

}
