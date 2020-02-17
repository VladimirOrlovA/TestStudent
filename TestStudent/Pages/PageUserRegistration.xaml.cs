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
        bool comparePassFlag = true;

        public PageUserRegistration()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            ComparePassword();

            bool checkInputFlag = (!string.IsNullOrEmpty((string)LbErrMesFullname.Content) || 
                                        !string.IsNullOrEmpty((string)LbErrMesLogin.Content) ||
                                        !string.IsNullOrEmpty((string)LbErrMesPassword.Content));

            if (checkInputFlag || comparePassFlag)
                LbErrMes.Content = "Исправить ошибки"; 
            else LbErrMes.Content = "Сохранение данных";
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
            var elem = sender as TextBox;

            elem.Background = Brushes.White;

            if (!CheckInput(elem.Text, out string strErrMes))
            {
                elem.BorderBrush = Brushes.Gray;
                return;
            }

            elem.BorderBrush = Brushes.Red;

            if (elem.Name == "TbFullname")
                LbErrMesFullname.Content = strErrMes;

            if (elem.Name == "TbLogin")
                LbErrMesLogin.Content = strErrMes;
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
            var elem = sender as PasswordBox;
            elem.Background = Brushes.White;

            if (!CheckInput(elem.Password, out string strErrMes))
            {
                elem.BorderBrush = Brushes.Gray;
                return;
            }

            elem.BorderBrush = Brushes.Red;

            if (elem.Name == "PbPassword")
                LbErrMesPassword.Content = strErrMes;

            if (elem.Name == "PbRePassword")
                LbErrMesRePassword.Content = strErrMes;
        }

        private void TbField_KeyDown(object sender, RoutedEventArgs e)
        {
            var elem = sender as TextBox;

            if (elem.Name == "TbFullname")
                LbErrMesFullname.Content = null;

            if (elem.Name == "TbLogin")
                LbErrMesLogin.Content = null;
        }

        private void PbField_KeyDown(object sender, RoutedEventArgs e)
        {
            var elem = sender as PasswordBox;

            if (elem.Name == "PbPassword")
                LbErrMesPassword.Content = null;

            if (elem.Name == "PbRePassword")
                LbErrMesRePassword.Content = null;
        }

        bool CheckInput(string inputVal, out string strErrMes)
        {
            if (string.IsNullOrWhiteSpace(inputVal))
            {
                strErrMes = "*поле обязательное для заполнения";
                return true;
            }
            if (inputVal.Length > 0 && inputVal[0] == ' ')
            {
                strErrMes = "*не может начинаться с пробела";
                return true;
            }
            if (inputVal.Length < 5)
            {
                strErrMes = "*введено менее 5 символов";
                return true;
            }
            strErrMes = null;
            return false;
        }

        void ComparePassword()
        {
            if (PbPassword.Password != PbRePassword.Password)
            {
                LbErrMesRePassword.Content = "Пароли должны быть идентичны";
                comparePassFlag = true;
            }
            else comparePassFlag = false;
        }

        private void PbRePassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (PbPassword.Password != PbRePassword.Password)
            {
                PbRePassword.BorderBrush = PbPassword.BorderBrush = Brushes.Red;
                PbRePassword.Foreground = Brushes.Red;
                comparePassFlag = true;
            }
            else
            {
                PbRePassword.BorderBrush = PbPassword.BorderBrush = Brushes.Gray;
                PbRePassword.Foreground = Brushes.Black;
                comparePassFlag = false;
            }
        }
    }
}
