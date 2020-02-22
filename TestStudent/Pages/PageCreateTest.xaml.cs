using System.Collections.Generic;
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
        static Question _question = null;
        static int _questionNumber = 0;

        public PageCreateTest()
        {
            InitializeComponent();
            FillContentFromDB();

            var obj = mainGridCont.Children.OfType<Expander>();
            foreach (Expander item in obj)
            {
                item.Expanded += Item_Expanded;
            }
        }

        private void Item_Expanded(object sender, RoutedEventArgs e)
        {
            var obj = mainGridCont.Children.OfType<Expander>();

            foreach (Expander item in obj)
            {
                if (item != (sender as Expander))
                    item.IsExpanded = false;
            }
        }
        private void ExpSubject_Collapsed(object sender, RoutedEventArgs e)
        {
            lbChoiceValExpSubject.Content = GetValRadioButton(spExpSubject.Children);
            LabelChoiceCheck();
        }

        private void ExpSection_Collapsed(object sender, RoutedEventArgs e)
        {
            lbChoiceValExpSection.Content = GetValRadioButton(spExpSection.Children);
            LabelChoiceCheck();
        }

        private void ExpTest_Collapsed(object sender, RoutedEventArgs e)
        {
            lbChoiceValExpTest.Content = GetValRadioButton(spExpTest.Children);
            LabelChoiceCheck();
        }

        private void ExpQuestion_Collapsed(object sender, RoutedEventArgs e)
        {
            LabelChoiceCheck();
        }

        private void LabelChoiceCheck()
        {
            int countChoised = 0;
            var lbCol = mainGridCont.Children.OfType<Label>();
            foreach (Label lb in lbCol)
            {
                if (!string.IsNullOrEmpty((string)lb.Content))
                    countChoised++;
            }
            PbFullComlete.Value = countChoised;

            if (PbFullComlete.Value < 3)
            {
                lbQuestionCount.Content = "Пока рано, выбрать еще " + (3 - PbFullComlete.Value);
                return;
            }

            btnEditQuestion.IsEnabled = true;

            // придумать способ достать кол-во записей по проще, не вытаскивая все записи из БД !!!!!!!!!!!!!!!!
            List<Question> questionsDB = MainWindow.db.GetQuestion();
            lbQuestionCount.Content = "Вопросов в тесте " + questionsDB.Count();
            lbChoiceValExpQuestion.Content = lbQuestionCount.Content;
        }

        private string GetValRadioButton(UIElementCollection uIElementCollection)
        {
            foreach (var item in uIElementCollection)
            {
                if (item is RadioButton)
                {
                    var rb = item as RadioButton;

                    if ((bool)rb.IsChecked)
                        return (string)rb.Content;
                }
            }
            return null;
        }

        private void FillContentFromDB()
        {
            var subjects = MainWindow.db.GetSubjects();
            var sections = MainWindow.db.GetSection();
            var testNames = MainWindow.db.GetTestName();
            //var question = MainWindow.db.GetQuestion();  //////////////////////////////////////////////////// 

            foreach (Subject item in subjects)
            {
                var rb = new RadioButton();
                rb.Content = item.Name;

                //var pos = spExpSection.Children.Count;
                spExpSubject.Children.Insert(0, rb);
            }

            foreach (Section item in sections)
            {
                var rb = new RadioButton();
                rb.Content = item.Name;

                //var pos = spExpSection.Children.Count;
                spExpSection.Children.Insert(0, rb);
            }

            foreach (TestName item in testNames)
            {
                var rb = new RadioButton();
                rb.Content = item.Name;

                //var pos = spExpSection.Children.Count;
                spExpTest.Children.Insert(0, rb);
            }
        }

        private void BtnEditSubject_Click(object sender, RoutedEventArgs e)
        {
            _stackPanel = spExpSubject;
            _stackPanel.Children.OfType<Button>().Last().Visibility = Visibility.Collapsed;
            _stackPanel.Children.Add(AddBlockEdit());
        }

        private void BtnEditSection_Click(object sender, RoutedEventArgs e)
        {
            _stackPanel = spExpSection;
            _stackPanel.Children.OfType<Button>().Last().Visibility = Visibility.Collapsed;
            _stackPanel.Children.Add(AddBlockEdit());
        }

        private void BtnEditTest_Click(object sender, RoutedEventArgs e)
        {
            _stackPanel = spExpTest;
            _stackPanel.Children.OfType<Button>().Last().Visibility = Visibility.Collapsed;
            _stackPanel.Children.Add(AddBlockEdit());
        }

        private void BtnEditQuestion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SliderQuestionNumber_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _questionNumber = (int)((Slider)sender).Value;
            GetQuestionBox();
        }

        private void ExpQuestion_Expanded(object sender, RoutedEventArgs e)
        {
            List<Question> questions = MainWindow.db.GetQuestion();
            sliderQuestionNumber.Value = 0;
            sliderQuestionNumber.Maximum = questions.Count() - 1;
            sliderQuestionNumber.Visibility = Visibility.Visible;
            GetQuestionBox();
        }

        private void GetQuestionBox()
        {
            List<Question> questions = MainWindow.db.GetQuestion();

            // выборка из коллекции согласно выбора пользователя
            //List<Question> questions = questionsDB.FindAll(f =>
            //f.SubjectName == (string)lbChoiceValExpSubject.Content &&
            //f.SectionName == (string)lbChoiceValExpSection.Content &&
            //f.TestName == (string)lbChoiceValExpTest.Content);

            //_question = questions.Last();

            if (questions.Count != 0)
            {
                _question = questions[_questionNumber];
                gbQuestionEdit.Header = "Вопрос " + (_questionNumber+1);
            }
            else
            {
                ReadCurrentQuestion();
            }

            tbQuestionText.Text = _question.QuestionText;
            tbQuestionRating.Text = _question.QuestionRating;

            var textBoxes = gcQuestionEdit.Children.OfType<TextBox>().ToList();
            var checkBoxes = gcQuestionEdit.Children.OfType<CheckBox>().ToList();
            var variant = _question.answerVariant;

            for (int i = 0; i < variant.Count(); i++)
            {
                textBoxes[i].Text = variant[i].Text;
                checkBoxes[i].IsEnabled = variant[i].IsRight;
                checkBoxes[i].IsChecked = variant[i].IsRight;
            }

            gbQuestionEdit.Visibility = Visibility.Visible;
        }

        private void ReadCurrentQuestion()
        {
            Question currentQuestion = new Question();

            currentQuestion.SubjectName = (string)lbChoiceValExpSubject.Content;
            currentQuestion.SectionName = lbChoiceValExpSection.Content.ToString();
            currentQuestion.TestName = lbChoiceValExpTest.Content.ToString();

            currentQuestion.QuestionText = tbQuestionText.Text;
            currentQuestion.QuestionRating = tbQuestionRating.Text;

            var textBoxes = gcQuestionEdit.Children.OfType<TextBox>().ToList();
            var checkBoxes = gcQuestionEdit.Children.OfType<CheckBox>().ToList();

            for (int i = 0; i < textBoxes.Count; i++)
            {
                if (!string.IsNullOrEmpty(textBoxes[i].Text))
                {
                    var newAnswerVariant = new AnswerVariant();
                    newAnswerVariant.Text = textBoxes[i].Text;
                    newAnswerVariant.IsRight = (bool)checkBoxes[i].IsChecked;
                    currentQuestion.answerVariant.Add(newAnswerVariant);
                }
            }
            _question = currentQuestion;
        }

        private StackPanel AddBlockEdit()
        {
            StackPanel spTbAndBtn = new StackPanel() { Name = "spTbAndBtn", Margin = new Thickness(5, 5, 5, 5) };
            StackPanel spBtn = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };
            TextBox textBox = new TextBox();

            // для доступа к textBox, сохраняем ссылку в статич поле (к StackPanel есть доступ по имени, TextBox нет!!!)
            _textBox = textBox;

            Button btnSave = new Button() { Name = "btnSave", Content = "Сохранить", Width = 85, Margin = new Thickness(0, 10, 0, 10) };
            Button btnDelete = new Button() { Name = "btnDelete", Content = "Удалить", Width = 85, Margin = new Thickness(5, 10, 0, 10) };
            Button btnCancel = new Button() { Name = "btnCancel", Content = "Отмена", Width = 70, Margin = new Thickness(5, 10, 0, 10) };

            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;
            btnCancel.Click += BtnCancel_Click;

            spBtn.Children.Add(btnSave);
            spBtn.Children.Add(btnDelete);
            spBtn.Children.Add(btnCancel);

            spTbAndBtn.Children.Add(textBox);
            spTbAndBtn.Children.Add(spBtn);

            return spTbAndBtn;
        }

        private void RemoveBlockEdit()
        {
            int posLastChild = _stackPanel.Children.Count;
            _stackPanel.Children.RemoveAt(posLastChild - 1);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            RemoveBlockEdit();
            _stackPanel.Children.OfType<Button>().Last().Visibility = Visibility.Visible;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string errMes = null;

            if (string.IsNullOrWhiteSpace(_textBox.Text))
            {
                MessageBox.Show("Вы ничего не ввели");
                return;
            }


            if (_stackPanel.Name == "spExpSubject")
            {
                Subject obj = new Subject();
                obj.Name = _textBox.Text;
                errMes = MainWindow.db.AddSubject(obj);
            }

            if (_stackPanel.Name == "spExpSection")
            {
                Section obj = new Section();
                obj.Name = _textBox.Text;
                errMes = MainWindow.db.AddSection(obj);
            }

            if (_stackPanel.Name == "spExpTest")
            {
                TestName obj = new TestName();
                obj.Name = _textBox.Text;
                errMes = MainWindow.db.AddTestName(obj);
            }

            if (errMes == null)
            {
                // для отображения и доступности в текущем сеансе, т.к. заполнение контентом идет при загрузке страницы
                var newRbtn = new RadioButton() { Content = _textBox.Text };
                int pos = _stackPanel.Children.Count;
                _stackPanel.Children.Insert(pos - 2, newRbtn);

                RemoveBlockEdit();
                _stackPanel.Children.OfType<Button>().Last().Visibility = Visibility.Visible;
            }
            else MessageBox.Show(errMes);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            string errMes = null;

            if (string.IsNullOrWhiteSpace(_textBox.Text))
            {
                MessageBox.Show("Вы ничего не ввели");
                return;
            }


            if (_stackPanel.Name == "spExpSubject")
            {
                Subject obj = new Subject();
                obj.Name = _textBox.Text;
                errMes = MainWindow.db.DeleteSubject(obj);
            }

            if (_stackPanel.Name == "spExpSection")
            {
                Section obj = new Section();
                obj.Name = _textBox.Text;
                errMes = MainWindow.db.DeleteSection(obj);
            }

            if (_stackPanel.Name == "spExpTest")
            {
                TestName obj = new TestName();
                obj.Name = _textBox.Text;
                errMes = MainWindow.db.DeleteTestName(obj);
            }

            if (errMes == null)
            {
                // скрываем визуальную часть в текущем сеансе, т.к. заполнение контентом идет при загрузке страницы
                var newRbtn = new RadioButton() { Content = _textBox.Text };
                var rbCol = _stackPanel.Children.OfType<RadioButton>();

                foreach (RadioButton rbItem in rbCol)
                {
                    if ((string)rbItem.Content == _textBox.Text)
                        rbItem.Visibility = Visibility.Collapsed;
                }

                RemoveBlockEdit();
                _stackPanel.Children.OfType<Button>().Last().Visibility = Visibility.Visible;
            }
            else MessageBox.Show(errMes);
        }


        private void BtnQuestionSave_Click(object sender, RoutedEventArgs e)
        {
            ReadCurrentQuestion();
            MainWindow.db.AddQuestion(_question);
            MessageBox.Show("Сохранено " + _question.Id);
        }

        private void BtnQuestionDelete_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.db.DeleteQuestion(_question);
            MessageBox.Show("Удалено " + _question.Id);
        }

        private void BtnQuestionCancel_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
