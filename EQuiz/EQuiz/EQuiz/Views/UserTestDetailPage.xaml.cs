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
    public partial class UserTestDetailPage : ContentPage
    {
        private ObservableCollection<Models.Grouping<string, RadioOption>> RadioOptionsList = new ObservableCollection<Models.Grouping<string, RadioOption>>();

        AnswerViewModel ViewModel;
        List<UserAnswer> userAnswers;

        UserTestDetailViewModel viewModel;

        public UserTestDetailPage(UserTestDetailViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = new AnswerViewModel();
            // BinuserAnswersdingContext = viewModel;
            userAnswers = new List<UserAnswer>();

            BindingContext = this.viewModel = viewModel;

            Initialize(viewModel);
        }
        public UserTestDetailPage()
        {
            InitializeComponent();

            var item = new UserTest
            {
                UserId = "Item 1"
            };

            ViewModel = new AnswerViewModel();
            // BinuserAnswersdingContext = viewModel;
            userAnswers = new List<UserAnswer>();

            viewModel = new UserTestDetailViewModel(item);
            BindingContext = viewModel;
        }

        public async void Handle_Clicked(object sender, EventArgs e)
        {
            await this.DisplayAlert("", "Ваш выбор сохранен (нет)", "OK");

            var userTest = new UserTest();

            userTest.Answers = userAnswers;

            //await viewModel.CreateUserAnswers(userTest);
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

        private async void Initialize(UserTestDetailViewModel viewModel)
        {
            // List<Answer> list = await viewModel.GetFriends();

           
            // Build a list of items

            var list = viewModel.UserTest.Answers.ToList();

            var a = list.FirstOrDefault();
            var items = new List<RadioOption>()
            {
                new RadioOption(1, RadioCategory.CategoryA,a.QuestionId.ToString(), "1"),
                new RadioOption(2, RadioCategory.CategoryA,"Ты дурак?", "2"),
                new RadioOption(3, RadioCategory.CategoryA,"Ты дурак?", "3"),
                new RadioOption(3, RadioCategory.CategoryA,"Ты дурак?", "4"),
                new RadioOption(3, RadioCategory.CategoryA,"Ты дурак?", "5"),

                new RadioOption(1, RadioCategory.CategoryB,"Ты мудак?", "1"),
                new RadioOption(2, RadioCategory.CategoryB,"Ты мудак?", "2"),
                new RadioOption(3, RadioCategory.CategoryB,"Ты мудак?", "3"),
                new RadioOption(3, RadioCategory.CategoryB,"Ты мудак?", "4"),
                new RadioOption(3, RadioCategory.CategoryB,"Ты мудак?", "5"),

            };

            

            var sorted = from item in items
                         group item by item.NameQuestion into radioGroups
                         select new Models.Grouping<string, RadioOption>(radioGroups.Key.ToString(), radioGroups);


            RadioOptionsList = new ObservableCollection<Models.Grouping<string, RadioOption>>(sorted);
            ListView_Radio.ItemsSource = RadioOptionsList;
        }


    }
}