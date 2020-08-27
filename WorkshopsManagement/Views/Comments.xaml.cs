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

namespace WorkshopsManagement.Views
{
    /// <summary>
    /// Interaction logic for Comments.xaml
    /// </summary>
    public partial class Comments : Page
    {
        MainWindow mainWindow;
        public Comments()
        {
            InitializeComponent();
        }


        public Comments(string message,MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            commentTags.Items.Add(new Question()
            {
                name = message
            });
        }

        private void backToForum(object sender, RoutedEventArgs e)
        {
            mainWindow.MainWin.Content = new Forum(mainWindow);
        }
    }
}
