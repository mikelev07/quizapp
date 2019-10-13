using EQuiz.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EQuiz.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingPage : ContentPage
    {
        private ObservableCollection<Models.Grouping<string, RadioOption>> RadioOptionsList = new ObservableCollection<Models.Grouping<string, RadioOption>>();

        AnswerViewModel viewModel;
        List<UserAnswer> userAnswers;
        // public MainViewModel ViewModel { get; } = new MainViewModel(App.NavigationService);

        public RatingPage()
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

        private async void Initialize()
        {
            List<Answer> list = await viewModel.GetFriends();

            var a = list.FirstOrDefault();
            // Build a list of items
            var items = new List<RadioOption>()
            {
                new RadioOption(1, RadioCategory.CategoryA,"Ты дурак?", "1"),
                new RadioOption(2, RadioCategory.CategoryA,"Ты дурак?", "2"),
                new RadioOption(3, RadioCategory.CategoryA,"Ты дурак?", "3"),
                new RadioOption(3, RadioCategory.CategoryA,"Ты дурак?", "4"),
                new RadioOption(3, RadioCategory.CategoryA,"Ты дурак?", "5"),

                new RadioOption(4, RadioCategory.CategoryB,"Ты мудак?", "4"),
                new RadioOption(5, RadioCategory.CategoryB,"Ты мудак?", "5"),

                new RadioOption(6, RadioCategory.CategoryC,"Ты судак?", "6"),
                new RadioOption(7, RadioCategory.CategoryC,"Ты судак?", "7"),

            };

           

            var sorted = from item in items
                         group item by item.NameQuestion into radioGroups
                         select new Models.Grouping<string, RadioOption>(radioGroups.Key.ToString(), radioGroups);


            RadioOptionsList = new ObservableCollection<Models.Grouping<string, RadioOption>>(sorted);
            ListView_Radio.ItemsSource = RadioOptionsList;
        }

    }
}