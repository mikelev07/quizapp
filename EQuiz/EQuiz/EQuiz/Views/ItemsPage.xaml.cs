using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EQuiz.Models;
using EQuiz.Views;

using System.Collections.ObjectModel;

using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EQuiz.Views
{
 
    public partial class ItemsPage : ContentPage
    {

        private ObservableCollection<Models.Grouping<string, RadioOption>> RadioOptionsList = new ObservableCollection<Models.Grouping<string, RadioOption>>();

        AnswerViewModel viewModel;
        List<UserAnswer> userAnswers;
       // public MainViewModel ViewModel { get; } = new MainViewModel(App.NavigationService);

        public ItemsPage()
        {
            InitializeComponent();

            viewModel = new AnswerViewModel();
            // BinuserAnswersdingContext = viewModel;
            userAnswers = new List<UserAnswer>();

           // BindingContext = ViewModel;

            Initialize();
        }

     

        public async void Handle_Clicked(object sender, EventArgs e)
        {
            await this.DisplayAlert("", "Ваш выбор сохранен (нет)", "OK");

            var userTest = new UserTest();
           
            userTest.Answers = userAnswers;

            await viewModel.CreateUserAnswers(userTest);
        }

        public void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            ListView_Radio.SelectedItem = null;
        }

        public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as RadioOption;

            if (item == null)
                return;

        
            foreach (var group in RadioOptionsList)
            {
                if (group.Contains(item))
                {
                    foreach (var s in group.Where(x => x.IsSelected))
                    {
                        s.IsSelected = false;
                        var elem = userAnswers.Where(t => t.AnswerId == s.Id).SingleOrDefault();
                        userAnswers.Remove(elem);
                    }

                    UserAnswer resultAnswer = new UserAnswer() { AnswerId = item.Id, QuestionId = 1 };
                    userAnswers.Add(resultAnswer);
                    item.IsSelected = true;
                }
               
            }

        
        }

        private void Initialize()
        {
            var list =  viewModel.GetFriends();
            // Build a list of items
            var items = new List<RadioOption>()
            {
                new RadioOption(1, RadioCategory.CategoryA,"Ты дурак?", "Чо куришь?"),
                new RadioOption(2, RadioCategory.CategoryA,"Ты дурак?", "Оранжевый"),
                new RadioOption(3, RadioCategory.CategoryA,"Ты дурак?", "Ну нахер"),

                new RadioOption(4, RadioCategory.CategoryB,"Ты мудак?", "Marvel"),
                new RadioOption(5, RadioCategory.CategoryB,"Ты мудак?", "DC"),

                new RadioOption(6, RadioCategory.CategoryC,"Ты судак?", "Курица"),
                new RadioOption(7, RadioCategory.CategoryC,"Ты судак?", "БОранина"),
             
            };

            var sorted = from item in items
                         group item by item.NameQuestion into radioGroups
                         select new Models.Grouping<string, RadioOption>(radioGroups.Key.ToString(), radioGroups);

      
            RadioOptionsList = new ObservableCollection<Models.Grouping<string, RadioOption>>(sorted);
            ListView_Radio.ItemsSource = RadioOptionsList;
        }

    }
}