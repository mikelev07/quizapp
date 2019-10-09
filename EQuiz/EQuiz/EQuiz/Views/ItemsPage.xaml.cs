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
using EQuiz.ViewModels;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace EQuiz.Views
{
 
    public partial class ItemsPage : ContentPage
    {

        private ObservableCollection<Models.Grouping<string, RadioOption>> RadioOptionsList = new ObservableCollection<Models.Grouping<string, RadioOption>>();


        public ItemsPage()
        {
            InitializeComponent();

           

            Initialize();
        }

        public async void Handle_Clicked(object sender, EventArgs e)
        {
            await this.DisplayAlert("", "Ваш выбор сохранен (нет)", "OK");
           
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
                    }

                    item.IsSelected = true;
                }
            }
        }

        private void Initialize()
        {
            // Build a list of items
            var items = new List<RadioOption>()
            {
                new RadioOption(RadioCategory.CategoryA,"Ты дурак?", "Чо куришь?", true),
                new RadioOption(RadioCategory.CategoryA,"Ты дурак?", "Оранжевый"),
                new RadioOption(RadioCategory.CategoryA,"Ты дурак?", "Ну нахер"),

                new RadioOption(RadioCategory.CategoryB,"Ты мудак?", "Marvel", true),
                new RadioOption(RadioCategory.CategoryB,"Ты мудак?", "DC"),


                new RadioOption(RadioCategory.CategoryC,"Ты судак?", "Курица", true),
                new RadioOption(RadioCategory.CategoryC,"Ты судак?", "БОранина"),
             
            };

            var sorted = from item in items
                         group item by item.NameQuestion into radioGroups
                         select new Models.Grouping<string, RadioOption>(radioGroups.Key.ToString(), radioGroups);

      
            RadioOptionsList = new ObservableCollection<Models.Grouping<string, RadioOption>>(sorted);
            ListView_Radio.ItemsSource = RadioOptionsList;
        }

    }
}