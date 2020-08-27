using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkshopsManagement.Models;

namespace WorkshopsManagement
{
    /// <summary>
    /// Interaction logic for Workshop.xaml
    /// </summary>
    public partial class Workshop : Page
    {
        public Workshop()
        {
            InitializeComponent();
            List<Models.Workshops> workshops = new List<Models.Workshops>();
            workshops.Add(new Models.Workshops()
            {
                name = "Comunicare",
                speakerName = "Ovidiu",
                imagePath = "/Images/workshop1.png",
                date = new DateTime(2017, 1, 18),
                description = "Descriere"
            });
            workshops.Add(new Models.Workshops()
            {
                name = "Comunicare",
                speakerName = "Vlad",
                imagePath = "/Images/workshop2.jpg",
                date = new DateTime(2017, 1, 18),
                description = "Descriere"
            });
            workshops.Add(new Models.Workshops()
            {
                name = "Comunicare",
                speakerName = "Gusti",
                imagePath = "/Images/workshop3.png",
                date = new DateTime(2017, 1, 18),
                description = "Descriere"
            });

            listBox1.ItemsSource = workshops;
        }
    }
}
