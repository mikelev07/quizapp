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
    public partial class UserTestDetailPage : ContentPage
    {

        UserTestDetailViewModel viewModel;

        public UserTestDetailPage(UserTestDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        public UserTestDetailPage()
        {
            InitializeComponent();

            var item = new UserTest
            {
                UserId = "Item 1"
            };

            viewModel = new UserTestDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}