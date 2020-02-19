using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;



namespace TestStudent
{
    public static class MenuBox
    {
        public static void Menu()
        {
            MainWindow._MainMenu.Visibility = System.Windows.Visibility.Visible;

            // Создаем пункты меню бар
            var menuFile = new MenuItem() { Header = "Файл" };
            var menuTools = new MenuItem() { Header = "Инструменты" };

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

            MainWindow._MainMenu.Items.Add(menuFile);
            MainWindow._MainMenu.Items.Add(menuTools);  
        }

        private static void MenuFileExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private static void MenuToolsCreateTests_Click(object sender, RoutedEventArgs e)
        {

        }

        private static void MenuToolsEditTests_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
