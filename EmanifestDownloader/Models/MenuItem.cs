using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanifestDownloader.Model
{
    public class CustomMenuItem : INotifyPropertyChanged
    {
        private bool isEnabled = true;
        private string text;
        private ObservableCollection<CustomMenuItem> subItems;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                if (this.isEnabled != value)
                {
                    this.isEnabled = value;
                    this.OnNotifyPropertyChanged("IsEnabled");
                }
            }
        }
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.OnNotifyPropertyChanged("Text");
                }
            }
        }
        public ObservableCollection<CustomMenuItem> SubItems
        {
            get
            {
                if (this.subItems == null)
                {
                    this.subItems = new ObservableCollection<CustomMenuItem>();
                }
                return this.subItems;
            }
            set
            {
                if (this.subItems != value)
                {
                    this.subItems = value;
                    this.OnNotifyPropertyChanged("SubItems");
                }
            }
        }
        private void OnNotifyPropertyChanged(string ptopertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(ptopertyName));
            }
        }
    }
}
