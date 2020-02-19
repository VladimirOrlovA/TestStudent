using System;
using System.Windows.Controls;

namespace TestStudent.Pages
{
    /// <summary>
    /// Interaction logic for PageUserProfile.xaml
    /// </summary>
    public partial class PageUserProfile : Page
    {
        public PageUserProfile()
        {
            InitializeComponent();

            lbFullnameVal.Content = MainWindow.user.Fullname;
            lbLoginVal.Content = MainWindow.user.Login;
            lbCreatedDateVal.Content = Convert.ToString(MainWindow.user.CreatedDate);
            lbLoginCountVal.Content = MainWindow.db.GetUserInfo().LoginCount;
        }
    }
}
