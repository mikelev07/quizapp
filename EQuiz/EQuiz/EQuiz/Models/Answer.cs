using EQuiz.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EQuiz.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string QuestionName { get; set; }

    }

    public class UserAnswer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
    }


    public class UserTest
    {
        public int Id { get; set; }
        public ICollection<UserAnswer> Answers {get; set;}
        public string Title { get; set; }
        public string UserId { get; set; }
        public UserTest()
        {
            Answers = new List<UserAnswer>();
        }

    }

    public class UserTestDetailViewModel 
    {
        public UserTest UserTest { get; set; }

        public string Title { get; set; }
        public UserTestDetailViewModel(UserTest item = null)
        {
            Title = item?.UserId;
            UserTest = item;
        }
    }



    public class UserTestViewModel
    {
        bool initialized = false;   // была ли начальная инициализация
        UserTest selectedFriend;  // выбранный друг
        private bool isBusy;    // идет ли загрузка с сервера

        public ObservableCollection<UserTest> UserTests { get; set; }
        UsetTestService friendsService = new UsetTestService();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateFriendCommand { protected set; get; }
        public ICommand DeleteFriendCommand { protected set; get; }
        public ICommand SaveFriendCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        public Command LoadItemsCommand { get; set; }

        public INavigation Navigation { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }
        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        public UserTestViewModel()
        {
            UserTests = new ObservableCollection<UserTest>();
            IsBusy = false;

          
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

        }

        async Task ExecuteLoadItemsCommand()
        {
            
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                UserTests.Clear();
                var items = await GetFriends();
              //  var items = new List<UserTest>() { new UserTest() { Id = 1, UserId = "vse ok", Text="ok i will go eat" } };
                foreach (var item in items)
                {
                    UserTests.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }



        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


        private void Back()
        {
            Navigation.PopAsync();
        }

        public async Task<List<UserTest>> GetFriends()
        {
            IEnumerable<UserTest> friends = await friendsService.Get();
            return friends.ToList();
        }

        public async Task<UserTest> CreateUserAnswers(UserTest answerObject)
        {
            UserTest answer = await friendsService.Add(answerObject);
            return answer;
        }

        private async void DeleteFriend(object friendObject)
        {
            Answer friend = friendObject as Answer;
            if (friend != null)
            {
                IsBusy = true;
                UserTest deletedFriend = await friendsService.Delete(friend.Id);
                if (deletedFriend != null)
                {
                    UserTests.Remove(deletedFriend);
                }
                IsBusy = false;
            }
            Back();
        }

    }



}
