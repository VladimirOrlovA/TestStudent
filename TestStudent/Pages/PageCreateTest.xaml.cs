using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DbOperation.lib;

namespace TestStudent.Pages
{
    /// <summary>
    /// Interaction logic for PageCreateTest.xaml
    /// </summary>
    public partial class PageCreateTest : Page
    {
        static StackPanel _stackPanel = null;
        static TextBox _textBox = null;

        public PageCreateTest()
        {
            InitializeComponent();
            FillSubject();
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

        private void FillSubject()
        {
            var subjects = MainWindow.db.GetSubjects();

            foreach (Subject item in subjects)
            {
                var rb = new RadioButton();
                rb.Content = item.Name;

                //var pos = spExpSection.Children.Count;
                spExpSubject.Children.Insert(0, rb);
            }
        }

        private void BtnAddSubject_Click(object sender, RoutedEventArgs e)
        {
            btnAddSubject.Visibility = Visibility.Collapsed;
            _stackPanel = spExpSubject;
            _stackPanel.Children.Add(AddBlockAddItem());
        }

        private void BtnAddSection_Click(object sender, RoutedEventArgs e)
        {
            btnAddSection.Visibility = Visibility.Collapsed;
            _stackPanel = spExpSection;
            _stackPanel.Children.Add(AddBlockAddItem());
        }

        private void BtnAddTest_Click(object sender, RoutedEventArgs e)
        {
            btnAddTest.Visibility = Visibility.Collapsed;
            _stackPanel = spExpTest;
            _stackPanel.Children.Add(AddBlockAddItem());
        }

        private StackPanel AddBlockAddItem()
        {
            StackPanel spTbAndBtn = new StackPanel() { Name = "spTbAndBtn", Margin = new Thickness(5, 5, 5, 5) };
            StackPanel spBtn = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };
            TextBox textBox = new TextBox();

            // для доступа к textBox, сохраняем ссылку в статич поле (к StackPanel есть доступ по имени, TextBox нет!!!)
            _textBox = textBox;

            Button btnSave = new Button() { Name = "btnSave", Content = "Сохранить", Width = 100, Margin = new Thickness(10, 10, 5, 10) };
            Button btnCancel = new Button() { Name = "btnCancel", Content = "Отмена", Width = 100, Margin = new Thickness(10, 10, 5, 10) };

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            spBtn.Children.Add(btnSave);
            spBtn.Children.Add(btnCancel);

            spTbAndBtn.Children.Add(textBox);
            spTbAndBtn.Children.Add(spBtn);

            return spTbAndBtn;
        }

        private void RemoveBlockAddItem()
        {
            int posLastChild = _stackPanel.Children.Count;
            _stackPanel.Children.RemoveAt(posLastChild - 1);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            RemoveBlockAddItem();
            _stackPanel.Children.OfType<Button>().Last().Visibility = Visibility.Visible;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Subject subject = new Subject();
            subject.Name = _textBox.Text;

            string errMes = MainWindow.db.AddSubject(subject);

            if (errMes == null)
            {
                // для отображения и доступности в текущем сеансе, т.к. заполнение контентом идет при загрузке страницы
                var newRbtn = new RadioButton() { Content = subject.Name };
                int pos = _stackPanel.Children.Count;
                _stackPanel.Children.Insert(pos - 2, newRbtn);

                RemoveBlockAddItem();
                _stackPanel.Children.OfType<Button>().Last().Visibility = Visibility.Visible;
            }
            else MessageBox.Show(errMes);
        }
    }
}
