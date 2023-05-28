using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab2_Rpm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Admin : ContentPage
    {
        Label stackLabel;
        bool loaded = false;
        public Admin()
        {
            InitializeComponent();
            Button books = new Button() { Text = "Книги" };
            books.Clicked += ToBooks_Clicked;

            Button profile = new Button() { Text = "Профиль" };
            profile.Clicked += ToProfile_Clicked;

            Button userRedact = new Button() { Text = "Редактирование пользователей" };
            userRedact.Clicked += ToRedactUser_Clicked;

            Button stat = new Button() { Text = "Статистика" };
            stat.Clicked += ToStatistics_Clicked;

            Button backBtn = new Button { Text = "Назад" };
            backBtn.Clicked += BackButton_Clicked;

            stackLabel = new Label();
            Content = new StackLayout { Children = { books, profile, userRedact, stat, stackLabel, backBtn } };
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

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            ((MainPage)navPage.CurrentPage).DisplayStack();
        }

        private async void ToProfile_Clicked(object sender, EventArgs e)
        {
            Profile profile = new Profile();
            await Navigation.PushAsync(profile);
            profile.DisplayStack();
        }

        private async void ToBooks_Clicked(object sender, EventArgs e)
        {
            Books books = new Books();
            await Navigation.PushAsync(books);
            books.DisplayStack();
        }

        private async void ToRedactUser_Clicked(object sender, EventArgs e)
        {
            UserRedact userRedact = new UserRedact();
            await Navigation.PushAsync(userRedact);
            userRedact.DisplayStack();
        }

        private async void ToStatistics_Clicked(object sender, EventArgs e)
        {
            Statistics statistics = new Statistics();
            await Navigation.PushAsync(statistics);
            statistics.DisplayStack();
        }
    }
}