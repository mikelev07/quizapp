using EQuiz.Services;
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
    public partial class LoginPage : ContentPage
    {

        private LoginService loginService;
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginPage()
        {
            InitializeComponent();
           // userData = new UserDB();
            NavigationPage.SetHasBackButton(this, false);
            userNameEntry.ReturnCommand = new Command(() => passwordEntry.Focus());
            firstPassword.ReturnCommand = new Command(() => secondPassword.Focus());
            var forgetpassword_tap = new TapGestureRecognizer();
            forgetpassword_tap.Tapped += Forgetpassword_tap_Tapped;
            forgetLabel.GestureRecognizers.Add(forgetpassword_tap);
        }
        private async void Forgetpassword_tap_Tapped(object sender, EventArgs e)
        {
            popupLoadingView.IsVisible = true;
        }
        string logesh;
        private async void userIdCheckEvent(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(useridValidationEntry.Text)) || (string.IsNullOrWhiteSpace(useridValidationEntry.Text)))
            {
                await DisplayAlert("Alert", "Enter Mail Id", "OK");
            }
            else
            {
                logesh = useridValidationEntry.Text;
               // var textresult = userData.updateUserValidation(useridValidationEntry.Text);
              /*  if (textresult)
                {
                    popupLoadingView.IsVisible = false;
                    passwordView.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("User Mail Id Not Exist", "Enter Correct User Name", "OK");
                }*/
            }
        }
       
        private async void LoginValidation_ButtonClicked(object sender, EventArgs e)
        {
            
            if (userNameEntry.Text != null && passwordEntry.Text != null)
            {
                loginService = new LoginService();
                var result = loginService.LoginAsync(userNameEntry.Text, passwordEntry.Text).Result;
                var validData = result;
                if (validData)
                {
                    popupLoadingView.IsVisible = false;
                    
                    await App.NavigatiPageAsync(loginPage);
                }
                else
                {
                    popupLoadingView.IsVisible = false;
                    await DisplayAlert("Login Failed", "Почта или Пароль неверные", "OK");
                }
            }
            else
            {
                popupLoadingView.IsVisible = false;
                await DisplayAlert("He He", "Введите Почту и Пароль", "OK");
            }
        }
    }
}