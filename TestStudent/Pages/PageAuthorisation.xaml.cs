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
    /// Interaction logic for PageAuthorisation.xaml
    /// </summary>
    public partial class PageAuthorisation : Page
    {
        //private DbContext db = new DbContext(MainWindow.path);
        public PageAuthorisation()
        {
            InitializeComponent();
        }

        private void BtnEnt_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput("inputVal", "errField"))
            {
                MainWindow.user = MainWindow.db.GetUser(TbInputLogin.Text, PbInputPassword.Password, out Exception exc);

                if (MainWindow.user != null)
                {
                    MainWindow._MainFrame.Navigate(new PageUserProfile());
                }
                else
                {
                    MessageBox.Show("Ошибка авторизации" + exc);
                }
            }
        }

        private void TbInputLogin_KeyDown(object sender, KeyEventArgs e)
        {
            string inputStr = TbInputLogin.Text;
            CheckInput(inputStr, "LbInputLoginErrMess");
        }

        private void TbInputLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            string inputStr = TbInputLogin.Text;
            CheckInput(inputStr, "LbInputLoginErrMess");
        }


        private void PbInputPassword_KeyDown(object sender, KeyEventArgs e)
        {
            string inputStr = PbInputPassword.Password;
            CheckInput(inputStr, "LbInputPasswordErrMess");
        }

        private void PbInputPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            string inputStr = PbInputPassword.Password;
            CheckInput(inputStr, "LbInputPasswordErrMess");
        }

        private bool CheckInput(string inputVal, string errField)
        {
            if (string.IsNullOrWhiteSpace(inputVal) && errField == "LbInputLoginErrMess")
            {
                LbInputLoginErrMess.Content = "Поле обязательное для заполнения!";
                LbInputLoginErrMess.Foreground = new SolidColorBrush(Colors.Red);
                TbInputLogin.BorderBrush = Brushes.Red;
                return false;
            }

            if (string.IsNullOrWhiteSpace(inputVal) && errField == "LbInputPasswordErrMess")
            {
                LbInputPasswordErrMess.Content = "Поле обязательное для заполнения!";
                LbInputPasswordErrMess.Foreground = new SolidColorBrush(Colors.Red);
                PbInputPassword.BorderBrush = Brushes.Red;
                return false;
            }

            LbInputLoginErrMess.Content = LbInputPasswordErrMess.Content = "";
            return true;
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._MainFrame.Navigate(new PageUserRegistration());
        }
    }
}
