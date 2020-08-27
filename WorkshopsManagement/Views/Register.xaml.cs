using DatabaseServiceReference;
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
using System.Windows.Shapes;
using WorkshopsManagement.Models;
using WorkshopsManagement.Services;

namespace WorkshopsManagement
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        Login login;
        UserService serviceMethods;

        public Register(Login login, UserService serviceMethods)
        {
            this.login = login;
            this.serviceMethods = serviceMethods;
            InitializeComponent();
        }


        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Close();
            login.Close();
        }

        private void BackToLogin(object sender, RoutedEventArgs e)
        {
            login.Show();
            Close();
        }

        private int getFirstAvailableId(List<Account> accounts)
        {
            int[] countIds = new int[100];
            countIds[0] = 1;

            foreach (Account account in accounts) countIds[account.getId()]++;

            int i = 0;
            while (i < countIds.Length)
            {
                if (countIds[i] == 0) return i;
                i++;
            }
            return accounts.Count;
        }
        private async void addToAccounts(object sender, RoutedEventArgs e)
        {
            if (serviceMethods.PasswordCheck(textBox_Pass.Password) == 0)
            {
                if (textBox_ConfirmPass.Password.Equals(textBox_Pass.Password))
                {
                    Account account = new Account(Convert.ToInt32(getFirstAvailableId(serviceMethods.getAccounts())), textBox_User.Text
                        , textBox_Pass.Password, textBox_First.Text, textBox_Last.Text, textBox_Email.Text);

                    await serviceMethods.add(account);
                    label_Register.Content = "Register Success!";
                    textBox_First.IsEnabled = false; textBox_Last.IsEnabled = false; textBox_User.IsEnabled = false;
                    textBox_Email.IsEnabled = false; textBox_Pass.IsEnabled = false; textBox_ConfirmPass.IsEnabled = false;
                }
                else
                {
                    textBox_ConfirmPass.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(255), Convert.ToByte(0),
                        Convert.ToByte(0)));
                    label_Register.Content = "Passwords Don't Match!";
                }
            }
            else
            {
                textBox_Pass.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(255), Convert.ToByte(0),
                        Convert.ToByte(0)));
                label_Register.Content = "Password is not Secure!";
            }
        }

        private void textBox_Password_TextChanged(object sender, RoutedEventArgs e)
        {
            textBox_ConfirmPass.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(0), Convert.ToByte(0),
                    Convert.ToByte(0)));
        }

        private void wrongChanged(object sender, RoutedEventArgs e)
        {
            textBox_Pass.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(0), Convert.ToByte(0),
                    Convert.ToByte(0)));
        }
    }
}
