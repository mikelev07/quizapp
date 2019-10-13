using EQuiz.Models;
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
    public partial class UserTestsPage : ContentPage
    {
        UserTestViewModel viewModel;

        public UserTestsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UserTestViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as UserTest;
            if (item == null)
                return;

            await Navigation.PushAsync(new UserTestDetailPage(new UserTestDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.UserTests.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }




    }
}