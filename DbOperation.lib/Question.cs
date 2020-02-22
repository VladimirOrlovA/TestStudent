using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOperation.lib
{
    public class Question
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string SectionName { get; set; }
        public string TestName { get; set; }
        public string QuestionText { get; set; }
        public string QuestionRating { get; set; }

        public List<AnswerVariant> answerVariant = new List<AnswerVariant>();
    }
}


public class AnswerVariant
{
    public int Id { get; set; }
    public string Text { get; set; }
    public bool IsRight { get; set; }
}