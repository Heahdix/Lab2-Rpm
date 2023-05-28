using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab2_Rpm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
        }

       

        

        private void RepeatPassword_Completed(object sender, EventArgs e)
        {
            if (Password.Text == RepeatPassword.Text)
            {
                PasswordNotMatch.IsVisible = false;
            }
            else
            {
                PasswordNotMatch.IsVisible = true;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Login.Text) && !string.IsNullOrEmpty(Password.Text) && !string.IsNullOrEmpty(RepeatPassword.Text) && Password.Text == RepeatPassword.Text && BirthDate.Date != null && Role.SelectedIndex != -1 && !string.IsNullOrEmpty(Telephone.Text))
            {
                Preferences.Set("Login", Login.Text);
                Preferences.Set("Password", Password.Text);
                Preferences.Set("BirthDate", BirthDate.Date);
                Preferences.Set("Role", Role.SelectedItem.ToString());
                Preferences.Set("PhoneNumber", Telephone.Text);
                DisplayAlert("Регистрация", "Вы успешно зарегистрировались", "Ok");

            }
            else
            {
                DisplayAlert("Регистрация", "Заполните все поля правильно", "Ok");
            }
        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}