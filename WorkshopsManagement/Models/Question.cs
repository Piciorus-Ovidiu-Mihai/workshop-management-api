using System;
using System.Collections.Generic;
using System.Text;

namespace WorkshopsManagement.Models
{
    public class Question
    {
        List<Answer> answers = new List<Answer>();
        public string name { get; set; }
        public int id { get; set; }
        public string username { get; set; }
        public int questionId { get; set; }

        public Question(int id, string name, string username, int questionId)
        {
            this.id = id;
            this.name = name;
            this.username = username;
            this.questionId = questionId;
        }
        public Question() { }

       override public string ToString()
        {
            return name;
        }

        public void addToAnswerList(Answer answer)
        {
            answers.Add(answer);
        }

        public void deleteFromAnswerList(Answer answer)
        {
            answers.Remove(answer);
        }

        public void setAnswerList(List<Answer> answers)
        {
            this.answers = answers;
        }

        public List<Answer> getAnswerList()
        {
            return answers;
        }
    }
}
