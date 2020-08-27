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
using WorkshopsManagement.Models;
using WorkshopsManagement.Services;

namespace WorkshopsManagement
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : Page
    {
        UserService serviceMethods;
        Account account;
        public Accounts(UserService serviceMethods,Account account)
        {
            this.serviceMethods = serviceMethods;
            this.account = account;

            InitializeComponent();
            textBox_User.Text = account.username;
            textBox_Pass.Password = account.password;
            textBox_Last.Text = account.lastName;
            textBox_First.Text = account.firstName;
            textBox_Email.Text = account.email;
            
        }

        private void saveData(object sender, RoutedEventArgs e)
        {
            Account newAccount = new Account(account.getId(), textBox_User.Text,
                textBox_Pass.Password, textBox_First.Text, textBox_Last.Text, textBox_Email.Text);
            serviceMethods.update(account.username, newAccount);
            

            textBox_First.IsEnabled = false;
            textBox_Last.IsEnabled = false;
            textBox_Pass.IsEnabled = false;
            textBox_User.IsEnabled = false;
            textBox_Email.IsEnabled = false;

            button_Edit.IsEnabled = true;
            button_Save.IsEnabled = false;
        }

        private void editData(object sender, RoutedEventArgs e)
        {
            textBox_First.IsEnabled = true;
            textBox_Last.IsEnabled = true; 
            textBox_Pass.IsEnabled = true;
            textBox_User.IsEnabled = true;
            textBox_Email.IsEnabled = true;


            button_Save.IsEnabled = true;
            button_Edit.IsEnabled = false;
        }

    }
}
