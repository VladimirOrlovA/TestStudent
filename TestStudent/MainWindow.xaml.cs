using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using DbOperation.lib;
using TestStudent.Pages;

namespace TestStudent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public static string path = @"C:\Users\Vladimir\source\repos\TestStudent\TestStudent\DataBase\testStudentData.db";
        public static DbConnection db = new DbConnection(path);
        public static User user = null;
        public static int startCount = 0;
        public static Frame _MainFrame = null;
        public static Menu _MainMenu = null;


        public MainWindow()
        {
            InitializeComponent();
            _MainFrame = MainFrame;
            _MainMenu = MainMenu;

            MainWindow._MainFrame.Navigate(StartFirstPage());

            //MainWindow._MainFrame.Navigate(DebugPage());
        }

        public static Page StartFirstPage()
        {
            string errMes = db.CheckDataBase();

            if (!string.IsNullOrEmpty(errMes))
            {
                MessageBoxResult result = MessageBox.Show(errMes, "Подготовка к работе",
                                        MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Cancel)
                {
                    App.Current.Shutdown();
                }
                else
                {
                    //using (LiteDB.LiteDatabase ldb = new LiteDB.LiteDatabase(path)) { };
                }
            }


            using (LiteDB.LiteDatabase ldb = new LiteDB.LiteDatabase(path))
            {
                if (ldb.CollectionExists("User"))
                    startCount = 1;
            };

            var firstStart = new PageAuthorisation();
            if (startCount == 0)
            {
                Label firstMessage = new Label()
                {
                    Content = "Первый запуск программы! \nсоздание учетной записи администратора",
                    FontSize = 25,
                    Foreground = new SolidColorBrush(Colors.Black),
                    Background = new SolidColorBrush(Colors.White)
                };

                firstStart.SpPageAuthorisation.Children.Clear();
                firstStart.SpPageAuthorisation.Children.Insert(0, firstMessage);
                firstStart.SpPageAuthorisation.Children.Insert(1, firstStart.btnEnt);
                firstStart.SpPageAuthorisation.Children.Insert(2, firstStart.btnReg);

                firstStart.btnEnt.Visibility = Visibility.Hidden;

                return firstStart;
            }
            return firstStart;
        }

        public static Page DebugPage()
        {
            var page = new PageCreateTest();
            return page;
        }
    }
}
