using System;
using System.Collections.Generic;
using System.Text;

namespace WorkshopsManagement.Models
{
    public class Workshops
    {
        public string name { get; set; }
        public string speakerName { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public string imagePath { get; set; }

        public Workshops(string name, string speakerName, DateTime date, string description, string imagePath)
        {
            this.name = name;
            this.speakerName = speakerName;
            this.date = date;
            this.description = description;
            this.imagePath = imagePath;
        }
        public Workshops()
        {

        }
    }
}
