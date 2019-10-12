using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EQuiz.Services;
using EQuiz.Views;
using System.Threading.Tasks;

namespace EQuiz
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string AzureBackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
        public static bool UseMockDataStore = true;

     

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public static async void NavigatiPage(Page name)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            // new NavigationPage(new UsersListPage());
            //Application.Current.MainPage = navPage;
            await name.Navigation.PushAsync(new MainPage());
        }

        internal static async Task NavigatiPageAsync(Page name)
        {
            Application.Current.MainPage = new NavigationPage(new AppShell());
            // new NavigationPage(new UsersListPage());
            Application.Current.MainPage = new AppShell();
            //Application.Current.MainPage = navPage;
            await name.Navigation.PushAsync(new AppShell());
        }


        // public static INavigationService NavigationService { get; } = _navigationService;
        // Use a service for providing this information
        // public static bool IsUserLoggedIn { get; set; }



        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
