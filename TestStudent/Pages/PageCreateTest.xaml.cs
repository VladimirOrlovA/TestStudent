using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TestStudent.Pages
{
    /// <summary>
    /// Interaction logic for PageCreateTest.xaml
    /// </summary>
    public partial class PageCreateTest : Page
    {
        public PageCreateTest()
        {
            InitializeComponent();
        }

        private void ExpSubject_Collapsed(object sender, RoutedEventArgs e)
        {
            lbChoiceValExpSubject.Content = GetValRadioButton(spExpSubject.Children);
        }

        private void ExpSection_Collapsed(object sender, RoutedEventArgs e)
        {
            lbChoiceValExpSection.Content = GetValRadioButton(spExpSection.Children);
        }

        private void ExpTest_Collapsed(object sender, RoutedEventArgs e)
        {
            lbChoiceValExpTest.Content = GetValRadioButton(spExpTest.Children);
        }

        private string GetValRadioButton(UIElementCollection uIElementCollection)
        {
            foreach (var item in uIElementCollection)
            {
                if (item is RadioButton)
                {
                    var rb = item as RadioButton;

                    if ((bool)rb.IsChecked)
                    {
                        return (string)rb.Content;
                    }
                }
            }
            return null;
        }
    }
}
