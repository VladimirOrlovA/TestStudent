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
        public string TestDescription { get; set; }

        public List<Answer> variant = new List<Answer>();
    }
}


public class Answer
{
    public int Id { get; set; }
    public string Text { get; set; }
    public bool IsRight { get; set; }
    public int Rating { get; set; }

}