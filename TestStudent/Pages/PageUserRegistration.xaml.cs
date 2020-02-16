﻿using System;
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

            string inputVal = elem.Text;

            // проверяем ввод данных в поле
            if (string.IsNullOrWhiteSpace(inputVal))
            {
                // получаем текущую позицию элемента на котором возникло событие
                int pos = GridContainer.Children.IndexOf(elem);

                Label errMessage = new Label()
                {
                    Foreground = Brushes.Red,
                    Content = "Вы ничего не ввели " + pos,
                    Name = "Lb1",
                };

                elem.BorderBrush = Brushes.Red;

                // устанавливаем свойства положения элемента errMessage в контейнере Grid
                Grid.SetRow(errMessage, pos);
                Grid.SetColumn(errMessage, 1);
                errMessage.Name = "Lb1";

                // добавляем в контейнер элемент errMessage
                GridContainer.Children.Add(errMessage);
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
            //var elem = (sender as TextBox);
            //int pos = GridContainer.Children.IndexOf(elem);
            //GridContainer.Children.RemoveAt(pos+1);

            object searchElem = GridContainer.FindName("Lb1");
            if (searchElem is Label)
            {
                Label errMes = searchElem as Label;
                errMes.Foreground = Brushes.Blue;
            }
        }

        private void PbField_KeyDown(object sender, RoutedEventArgs e)
        {
            var elem = (sender as TextBox);
            var pos = GridContainer.Children.IndexOf(elem);
            GridContainer.Children.RemoveAt(pos);
        }

        void Find(object sender, RoutedEventArgs e)
        {
            object wantedNode = GridContainer.FindName("Lb1");
            if (wantedNode is Label)
            {
                // Following executed if Text element was found.
                Label wantedChild = wantedNode as Label;
                wantedChild.Foreground = Brushes.Blue;
            }
        }
    }
}
