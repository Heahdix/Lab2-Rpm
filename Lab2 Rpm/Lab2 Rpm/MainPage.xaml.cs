using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Lab2_Rpm
{
    public partial class MainPage : ContentPage
    {
        Label stackLabel;
        bool loaded = false;
        public MainPage()
        {
            InitializeComponent();
            stackLabel = new Label();
            MainPlace.Children.Add(stackLabel);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (loaded == false)
            {
                DisplayStack();
                loaded = true;
            }
        }

        protected internal void DisplayStack()
        {
            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            stackLabel.Text = "";
            foreach (Page p in navPage.Navigation.NavigationStack)
            {
                stackLabel.Text += p.Title + "\n";
            }
        }

        private async void RegButton_Clicked(object sender, EventArgs e)
        {
            if(Login.Text == Preferences.Get("Login", null) && Password.Text == Preferences.Get("Password", null) && Preferences.Get("Role", null) == "Администратор")
            {
                Admin admin = new Admin();
                await Navigation.PushAsync(admin);
                admin.DisplayStack();
            }
            if (Login.Text == Preferences.Get("Login", null) && Password.Text == Preferences.Get("Password", null) && Preferences.Get("Role", null) == "Библиотекарь")
            {
                Librarian librarian = new Librarian();
                await Navigation.PushAsync(librarian);
                librarian.DisplayStack();
            }
        }

        private async void NewUser_Clicked(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            await Navigation.PushAsync(registration);
        }
    }
}
