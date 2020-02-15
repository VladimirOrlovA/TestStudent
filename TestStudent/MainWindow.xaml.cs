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

using DbOperation.lib;
using TestStudent.Pages;

namespace TestStudent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        // Путь где хранится файл БД
        static string path = @"C:\Users\Vladimir\source\repos\TestStudent\TestStudent\DataBase\testStudentData.db";
        // Создаем экземпляр класса отвечающего за работу с БД
        public static DbConnection db = new DbConnection(path);

        public static User user = null;
        public static Frame _MainFrame = null;


        public MainWindow()
        {
            InitializeComponent();

            _MainFrame = MainFrame;
            MainWindow._MainFrame.Navigate(StartFirstPage());
        }

        public static Page StartFirstPage()
        {
            //MessageBox.Show(db.CheckDataBase(out int count));

            if (true)
            {
                var firstStart = new PageAuthorisation();
                Label firstMessage = new Label()
                {
                    Content = "Первый запуск программы! \nсоздание учетной записи администратора",
                    FontSize = 30,
                    Foreground = new SolidColorBrush(Colors.Lime),
                    Background = new SolidColorBrush(Colors.Black)
                };

                firstStart.SpPageAuthorisation.Children.Insert(0, firstMessage);

                firstStart.btnEnt.Visibility = Visibility.Hidden;

                return firstStart;
            }
        }
    }
}
