using EQuiz.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace EQuiz.Models
{
   public class AnswerViewModel
   {
        bool initialized = false;   // была ли начальная инициализация
        Answer selectedFriend;  // выбранный друг
        private bool isBusy;    // идет ли загрузка с сервера

        public ObservableCollection<Answer> Answers { get; set; }
        AnswerService friendsService = new AnswerService();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateFriendCommand { protected set; get; }
        public ICommand DeleteFriendCommand { protected set; get; }
        public ICommand SaveFriendCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

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

        public AnswerViewModel()
        {
            Answers = new ObservableCollection<Answer>();
            IsBusy = false;
         
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

        public async Task<IEnumerable<Answer>> GetFriends()
        {
            IEnumerable<Answer> friends = await friendsService.Get();
            return friends;
        }
        private async void SaveFriend(object friendObject)
        {
            Answer friend = friendObject as Answer;
            if (friend != null)
            {
                IsBusy = true;
                // редактирование
                if (friend.Id > 0)
                {
                    Answer updatedFriend = await friendsService.Update(friend);
                    // заменяем объект в списке на новый
                    if (updatedFriend != null)
                    {
                        int pos = Answers.IndexOf(updatedFriend);
                        Answers.RemoveAt(pos);
                        Answers.Insert(pos, updatedFriend);
                    }
                }
                // добавление
                else
                {
                    Answer addedFriend = await friendsService.Add(friend);
                    if (addedFriend != null)
                        Answers.Add(addedFriend);
                }
                IsBusy = false;
            }
            Back();
        }
        private async void DeleteFriend(object friendObject)
        {
            Answer friend = friendObject as Answer;
            if (friend != null)
            {
                IsBusy = true;
                Answer deletedFriend = await friendsService.Delete(friend.Id);
                if (deletedFriend != null)
                {
                    Answers.Remove(deletedFriend);
                }
                IsBusy = false;
            }
            Back();
        }

    } 
}
