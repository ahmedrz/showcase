using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IQMan.CustomControls
{
    /// <summary>
    /// Interaction logic for DropDownAutoComplete.xaml
    /// </summary>
    public partial class DropDownAutoComplete : UserControl
    {


        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSourceProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(DropDownAutoComplete), new PropertyMetadata(null));



        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchTextProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(DropDownAutoComplete), new PropertyMetadata(null));


        public DropDownAutoComplete()
        {
            InitializeComponent();
            //DataContext=this;
        }
        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.RadAutoCompleteBox.Focus();
            this.RadAutoCompleteBox.Populate(this.RadAutoCompleteBox.SearchText);
        }

        private void RadAutoCompleteBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RadAutoCompleteBox.SelectedItem == null)
            {
                RadAutoCompleteBox.SearchText = null;
            }
        }
    }
}
