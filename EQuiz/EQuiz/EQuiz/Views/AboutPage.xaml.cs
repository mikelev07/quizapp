using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }
        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Выйти", "Вы хотите выйти?", "OK");
            Page page = new LoginPage();
           
            await Navigation.PushAsync(new MainPage());
            Application.Current.MainPage = new NavigationPage(new MainPage())
            {
                BackgroundColor = Color.White
            }; 
        }

    }
}