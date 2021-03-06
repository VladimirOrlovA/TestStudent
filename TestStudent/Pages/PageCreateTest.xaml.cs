﻿using System.Collections.Generic;
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

        static List<Question> _questions = null;
        static Question _question = null;
        static int _questionNumber = 0;

        static bool _newQuestionFlag = false;

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

        // для закрытия всех открытых экспандеров
        private void Item_Expanded(object sender, RoutedEventArgs e)
        {
            var obj = mainGridCont.Children.OfType<Expander>();

            foreach (Expander item in obj)
            {
                if (item != (sender as Expander))
                    item.IsExpanded = false;
            }
        }

        // запись сделаного выбора в экспандере Предмет
        private void ExpSubject_Collapsed(object sender, RoutedEventArgs e)
        {
            lbChoiceValExpSubject.Content = GetValRadioButton(spExpSubject.Children);
            LabelChoiceCheck();
        }

        // запись сделаного выбора в экспандере Раздел
        private void ExpSection_Collapsed(object sender, RoutedEventArgs e)
        {
            lbChoiceValExpSection.Content = GetValRadioButton(spExpSection.Children);
            LabelChoiceCheck();
        }

        // запись сделаного выбора в экспандере Название теста
        private void ExpTest_Collapsed(object sender, RoutedEventArgs e)
        {
            lbChoiceValExpTest.Content = GetValRadioButton(spExpTest.Children);
            LabelChoiceCheck();
        }


        private void ExpQuestion_Collapsed(object sender, RoutedEventArgs e)
        {
            LabelChoiceCheck();
            sliderQuestionNumber.Visibility = Visibility.Collapsed;
            gbQuestionEdit.Visibility = Visibility.Collapsed;
        }

        // проверка сделан ли выбор раздела
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

            FillContentFromDbQuestions();
            btnEditQuestion.Visibility = Visibility.Visible;
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

        private void FillContentFromDbQuestions()
        {
            var questionsDB = MainWindow.db.GetQuestion();

            //выборка из коллекции согласно выбора пользователя
            List<Question> questions = questionsDB.FindAll(f =>
            f.SubjectName == (string)lbChoiceValExpSubject.Content &&
            f.SectionName == (string)lbChoiceValExpSection.Content &&
            f.TestName == (string)lbChoiceValExpTest.Content);

            if (questions.Count != 0)
            {
                lbQuestionCount.Content = "Вопросов в тесте " + questions.Count();
                lbChoiceValExpQuestion.Content = lbQuestionCount.Content;
            }

            _questions = questions;

            sliderQuestionNumber.Value = 0;
            if (_questions.Count() != 0)
                sliderQuestionNumber.Maximum = _questions.Count() - 1;
            sliderQuestionNumber.Visibility = Visibility.Visible;
            GetQuestionBox();
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

        private void BtnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            _newQuestionFlag = true;
            ReadCurrentQuestion();
            GetQuestionBox();
        }

        private void SliderQuestionNumber_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _questionNumber = (int)((Slider)sender).Value;
            GetQuestionBox();
        }

        private void ExpQuestion_Expanded(object sender, RoutedEventArgs e)
        {
            if (PbFullComlete.Value < 3) return;
            gbQuestionEdit.Visibility = Visibility.Visible;

        }

        private void GetQuestionBox()
        {
            if (_questions.Count != 0 && !_newQuestionFlag)
            {
                _question = _questions[_questionNumber];
                gbQuestionEdit.Header = "Вопрос " + (_questionNumber+1);
            }
            else
            {
                _question = new Question();
                gbQuestionEdit.Header = "Вопрос - Новый";
                _newQuestionFlag = false;
            }

            tbQuestionText.Text = _question.QuestionText;
            tbQuestionRating.Text = _question.QuestionRating;

            var textBoxes = gcQuestionEdit.Children.OfType<TextBox>().ToList();
            var checkBoxes = gcQuestionEdit.Children.OfType<CheckBox>().ToList();
            var variant = _question.answerVariant;

            // Если добавляем новый вопрос (откр чистую форму) то просто пробегаемся по полям и пишем в них ноль
            if (variant.Count() != 0)
            {
                for (int i = 0; i < 5; i++)
                {
                     try { textBoxes[i].Text = variant[i].Text; } catch { textBoxes[i].Text = null; };
                    try { checkBoxes[i].IsChecked = variant[i].IsRight; } catch { checkBoxes[i].IsChecked = false; }
                }
            }
            else
            {
                for (int i = 0; i < textBoxes.Count(); i++)
                {
                    textBoxes[i].Text = null;
                    checkBoxes[i].IsChecked = false;
                }
            }

            //gbQuestionEdit.Visibility = Visibility.Visible;
        }

        private void ReadCurrentQuestion()
        {
            Question currentQuestion = new Question();

            currentQuestion.Id = _question.Id;
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
                    var currentAnswerVariant = new AnswerVariant();
                    currentAnswerVariant.Text = textBoxes[i].Text;
                    currentAnswerVariant.IsRight = (bool)checkBoxes[i].IsChecked;
                    currentQuestion.answerVariant.Add(currentAnswerVariant);
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
            string errMes = MainWindow.db.AddQuestion(_question);
            MessageBox.Show("Сохранение вопроса с id - " + _question.Id + "\n" + errMes);
        }

        private void BtnQuestionDelete_Click(object sender, RoutedEventArgs e)
        {
            //ReadCurrentQuestion();
            string errMes = MainWindow.db.DeleteQuestion(_question);
            MessageBox.Show("Удаление вопроса с id - " + _question.Id + "\n" + errMes);
        }

        private void BtnQuestionCancel_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
