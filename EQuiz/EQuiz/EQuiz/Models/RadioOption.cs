using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace EQuiz.Models
{
    public class RadioOption : INotifyPropertyChanged
    {
        public RadioCategory Category { get; }
        public string Title { get; }
        public string NameQuestion { get; }

        private bool _isSelected { get; set; }
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value != _isSelected)
                {
                    this._isSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RadioOption(RadioCategory category,string nameQuest, string title, bool isSelected = false)
        {
            this.NameQuestion = nameQuest;
            this.Category = category;
            this.Title = title;
            this.IsSelected = isSelected;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public enum RadioCategory
    {
        CategoryA,
        CategoryB,
        CategoryC,
        CategoryD
    }
}
