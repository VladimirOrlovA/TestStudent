using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using TestStudent.Pages;



namespace TestStudent
{
    public static class MenuBox
    {
        public static void Menu()
        {
            // вход Администратора
            if (MainWindow.user.status_id == 1)
                AdminOptions();

            // вход Пользователя
            if (MainWindow.user.status_id == 2)
                UserOptions();
        }

        private static void UserOptions()
        {
            MainWindow._MainMenu.Visibility = System.Windows.Visibility.Visible;

            // Создаем пункты меню бар
            var menuFile = new MenuItem() { Header = "Файл" };
            var menuTools = new MenuItem() { Header = "Инструменты" };

            // Создаем пункт меню "Профиль"
            var menuFileProfile = new MenuItem() { Header = "Профиль"};
            menuFileProfile.Click += MenuFileProfile_Click;
            menuFile.Items.Add(menuFileProfile);

            // Создаем пункт меню "Выход"
            var menuFileExit = new MenuItem() { Header = "Выход" };
            menuFileExit.Click += MenuFileExit_Click;
            menuFile.Items.Add(menuFileExit);

            // Создаем пункт меню "Создание тестов"
            var menuToolsCreateTests = new MenuItem() { Header = "Создание тестов", Visibility=Visibility.Collapsed};
            menuToolsCreateTests.Click += MenuToolsCreateTests_Click;
            menuTools.Items.Add(menuToolsCreateTests);

            // Создаем пункт меню "Редактирование тестов"
            var menuToolsEditTests = new MenuItem() { Header = "Редактирование тестов", Visibility = Visibility.Collapsed };
            menuToolsEditTests.Click += MenuToolsEditTests_Click;
            menuTools.Items.Add(menuToolsEditTests);

            // Создаем пункт меню "Список тестов"
            var menuToolsListTests = new MenuItem() { Header = "Список тестов" };
            menuToolsListTests.Click += MenuToolsListTests_Click;
            menuTools.Items.Add(menuToolsListTests);

            MainWindow._MainMenu.Items.Add(menuFile);
            MainWindow._MainMenu.Items.Add(menuTools);
        }

        private static void AdminOptions()
        {
            MainWindow._MainMenu.Visibility = System.Windows.Visibility.Visible;

            // Создаем пункты меню бар
            var menuFile = new MenuItem() { Header = "Файл" };
            var menuTools = new MenuItem() { Header = "Инструменты" };

            // Создаем пункт меню "Профиль"
            var menuFileProfile = new MenuItem() { Header = "Профиль" };
            menuFileProfile.Click += MenuFileProfile_Click;
            menuFile.Items.Add(menuFileProfile);

            // Создаем пункт меню "Выход"
            var menuFileExit = new MenuItem() { Header = "Выход" };
            menuFileExit.Click += MenuFileExit_Click;
            menuFile.Items.Add(menuFileExit);

            // Создаем пункт меню "Создание тестов"
            var menuToolsCreateTests = new MenuItem() { Header = "Создание тестов" };
            menuToolsCreateTests.Click += MenuToolsCreateTests_Click;
            menuTools.Items.Add(menuToolsCreateTests);

            // Создаем пункт меню "Редактирование тестов"
            var menuToolsEditTests = new MenuItem() { Header = "Редактирование тестов" };
            menuToolsEditTests.Click += MenuToolsEditTests_Click;
            menuTools.Items.Add(menuToolsEditTests);

            // Создаем пункт меню "Список тестов"
            var menuToolsListTests = new MenuItem() { Header = "Список тестов" };
            menuToolsListTests.Click += MenuToolsListTests_Click;
            menuTools.Items.Add(menuToolsListTests);

            MainWindow._MainMenu.Items.Add(menuFile);
            MainWindow._MainMenu.Items.Add(menuTools);
        }

        private static void MenuToolsListTests_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private static void MenuFileProfile_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._MainFrame.Navigate(new PageUserProfile());
        }

        private static void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private static void MenuToolsCreateTests_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._MainFrame.Navigate(new PageCreateTest());
        }

        private static void MenuToolsEditTests_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._MainFrame.Navigate(new PageCreateTest());
        }

    }
}
