using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace IQMan.CustomControls
{
    public class DataFormNumericUpDownField : DataFormDataField
    {
        public DataFormNumericUpDownField()
        {
            NumericUpDown = new RadNumericUpDown();
            NumericUpDown.HorizontalContentAlignment = HorizontalAlignment.Left;
        }
        public RadNumericUpDown NumericUpDown { get; set; }
        protected override DependencyProperty GetControlBindingProperty()
        {
            return RadNumericUpDown.ValueProperty;
        }
        protected override Control GetControl()
        {
            DependencyProperty dependencyProperty = this.GetControlBindingProperty();
            if (this.DataMemberBinding != null)
            {
                var binding = this.DataMemberBinding;
                NumericUpDown.SetBinding(dependencyProperty, binding);
            }
            NumericUpDown.SetBinding(RadNumericUpDown.IsEnabledProperty, new Binding("IsReadOnly") { Source = this, Converter = new InvertedBooleanConverter() });

            return NumericUpDown;
        }
    }
}
