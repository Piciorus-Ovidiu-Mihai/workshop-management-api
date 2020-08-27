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

namespace WorkshopsManagement.Views
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        public Admin()
        {
            InitializeComponent();

        }


        public class Workshop
        {
            public string workshopId { get; set; }
            public string workshopName { get; set; }
            public string workshopOrganizer { get; set; }
            public string workshopDescription { get; set; }
            public string workshopDate { get; set; }
        }

        private void AddWorkshop_Clicked(object sender, RoutedEventArgs e)
        {
            Workshop tempWorkshop = new Workshop();
            tempWorkshop.workshopName = TextBoxName.Text;
            tempWorkshop.workshopOrganizer = TextBoxOrganizer.Text;
            tempWorkshop.workshopDescription = TextBoxDescription.Text;
            tempWorkshop.workshopDate = TextBoxDate.Text;

            WorkshopDataGrid.Items.Add(tempWorkshop);
        }

        //private void AddNewWorkshop_Clicked(object sender, RoutedEventArgs e)
        //{
        //    AddNewWorkshop.Visibility = Visibility.Visible;
        //    Workshop tempWorkshop = new Workshop();
        //    tempWorkshop.workshopName = TextBoxName.Text;
        //    tempWorkshop.workshopOrganizer = TextBoxOrganizer.Text;
        //    tempWorkshop.workshopDescription = TextBoxDescription.Text;
        //    tempWorkshop.workshopDate = TextBoxDate.Text;

        //    WorkshopDataGrid.Items.Add(tempWorkshop);

        //    }
        //}
    }
}