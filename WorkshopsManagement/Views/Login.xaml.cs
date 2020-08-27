using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkshopsManagement.Services;
using DatabaseServiceReference;
using WorkshopsManagement.Models;
using System.Windows.Media.Media3D;

namespace WorkshopsManagement
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        readonly UserService serviceMethods = UserService.createUserService();
        public Login()
        {
            InitializeComponent();
        }


        private void Close_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GoToRegister(object sender, MouseButtonEventArgs e)
        {
            textBlock_WelcomeMessage.Text = "Login";
            Register register = new Register(this, serviceMethods);
            register.Show();
            Hide();
        }

        private async void GoToMainWindow(object sender, RoutedEventArgs e) //Login
        {
            
            string check = await serviceMethods.login(textBox_User.Text, textBox_Pass.Password);
            if (check.Equals("Ok"))
            {
                Account account = new Account();
                foreach (Account tempAccount in serviceMethods.getAccounts())
                {
                    if (tempAccount.username.Equals(textBox_User.Text))
                    {
                        account = tempAccount;
                        break;
                    }
                }
                user_Error_Label.Content = "";
                pass_Error_Label.Content = "";
                textBox_User.Clear();
                textBox_Pass.Clear();
                MainWindow mainWindow = new MainWindow(this,serviceMethods,account);
                mainWindow.Show();
                Hide();
            }
            else if (check.Equals("Incorrect Password!"))
            {
                user_Error_Label.Content = "";
                pass_Error_Label.Content = "Incorrect Password!";
                textBox_Pass.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(255), Convert.ToByte(0),
                    Convert.ToByte(0)));
            }
            else if (check.Equals("User Not Found!"))
            {
                pass_Error_Label.Content = "";
                user_Error_Label.Content = "User Not Found!";
                textBox_User.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(255), Convert.ToByte(0),
                    Convert.ToByte(0)));
            }
        }

        private void textBox_Pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            textBox_Pass.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(0), Convert.ToByte(0),
                    Convert.ToByte(0)));
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBox_User.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(0), Convert.ToByte(0),
                    Convert.ToByte(0)));
        }

        
    }
}
