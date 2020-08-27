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
using WorkshopsManagement.Views;

namespace WorkshopsManagement
{
    /// <summary>
    /// Interaction logic for Forum.xaml
    /// </summary>
    public partial class Forum : Page
    {
        MainWindow mainWindow;
        public Forum(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            forumTags.Items.Add(new Question()
            {
                name = "Testare1"
            });
            forumTags.Items.Add(new Question()
            {
                name = "Testare2"
            });
            forumTags.Items.Add(new Question()
            {
                name = "Testare3"
            });
            forumTags.Items.Add(new Question()
            {
                name = "Testare4"
            });
            forumTags.Items.Add(new Question()
            {
                name = "Testare5"
            });
        }

        private void selectare(object sender, SelectionChangedEventArgs e)
        {
            mainWindow.MainWin.Content = new Comments(forumTags.SelectedItem.ToString(),mainWindow);
        }
    }
}
