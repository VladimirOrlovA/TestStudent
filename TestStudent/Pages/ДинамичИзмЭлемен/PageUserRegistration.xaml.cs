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
    /// Interaction logic for PageUserRegistration.xaml
    /// </summary>
    public partial class PageUserRegistration : Page
    {
        // для хранения ссылок на динамический созданные элементы
        private Dictionary<string, Label> ErrMesLabels = new Dictionary<string, Label>();

        public PageUserRegistration()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._MainFrame.Navigate(MainWindow.StartFirstPage());
        }

        private void TbField_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as TextBox).Background = new SolidColorBrush(Colors.LightGreen);
            (sender as TextBox).Focus();
        }

        private void TbField_GotFocus(object sender, RoutedEventArgs e)
        {
            var elem = (sender as TextBox);
            elem.Background = Brushes.LightGreen;
        }

        private void TbField_LostFocus(object sender, RoutedEventArgs e)
        {
            var elem = (sender as TextBox);
            elem.Background = Brushes.White;


            // проверяем ввод данных в поле
            if (string.IsNullOrWhiteSpace(elem.Text))
            {
                // получаем текущую позицию элемента на котором возникло событие
                int pos = GridContainer.Children.IndexOf(elem);

                Label errMessage = new Label()
                {
                    Foreground = Brushes.Red,
                    Content = "Вы ничего не ввели " + pos,
                };

                elem.BorderBrush = Brushes.Red;

                // устанавливаем свойства положения элемента errMessage в контейнере Grid
                Grid.SetRow(errMessage, pos);
                Grid.SetColumn(errMessage, 1);

                //// на повторном появлении ошибки ввода увдаляем ссылку на ранее имевшую место Label ошибки
                //if (ErrMesLabels.ContainsKey(elem.Name))
                //    ErrMesLabels.Remove(elem.Name);

                // добавляем в контейнер элемент errMessage
                GridContainer.Children.Add(errMessage);

                // записываем ссылку в словарь на динамический созданный элемент
                if (!ErrMesLabels.ContainsKey(elem.Name))
                {
                    ErrMesLabels.Add(elem.Name, errMessage);
                    ErrMesLabels[elem.Name].Foreground = Brushes.Red;
                }

            }


        }

        private void PbField_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as PasswordBox).Background = new SolidColorBrush(Colors.LightGreen);
            (sender as PasswordBox).Focus();
        }

        private void PbField_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as PasswordBox).Background = Brushes.LightGreen;
        }

        private void PbField_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as PasswordBox).Background = Brushes.White;
        }

        private void TbField_KeyDown(object sender, RoutedEventArgs e)
        {
            var elem = sender as TextBox;

            // если ошибка ранее не появлялась, то исключаем обращение к несуществующему Label
            if (ErrMesLabels.ContainsKey(elem.Name))
                ErrMesLabels[elem.Name].Foreground = Brushes.Blue;

            // этот поиск работает только со статичными элементами (созданными на этапе разработки)
            //object searchElem = GridContainer.FindName("Lb1");
            //if (searchElem is Label)
            //{
            //    Label errMes = searchElem as Label;
            //    errMes.Foreground = Brushes.Blue;
            //}
        }

        private void PbField_KeyDown(object sender, RoutedEventArgs e)
        {
            var elem = (sender as TextBox);
            var pos = GridContainer.Children.IndexOf(elem);
            GridContainer.Children.RemoveAt(pos);
        }
    }
}
