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
    public partial class LoveAndWar : ContentPage
    {
        Label stackLabel;
        bool loaded = false;
        public LoveAndWar()
        {
            InitializeComponent();
            object kolvo;
            int count = 0;
            if (App.Current.Properties.TryGetValue("Count", out kolvo))
            {
                count = (int)kolvo + 1;
                Application.Current.SavePropertiesAsync();
            }
            App.Current.Properties["Count"] = count;

            Button backBtn = new Button { Text = "Назад" };
            backBtn.Clicked += BackButton_Clicked;
            Button read = new Button { Text = "Читать" };
            read.Clicked += Read_Clicked;
            Button download = new Button { Text = "Скачать" };
            download.Clicked += Download_Clicked;
            stackLabel = new Label();
            Content = new StackLayout { Children = { stackLabel, read, download, backBtn } };
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            ((Books)navPage.CurrentPage).DisplayStack();
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
        private async void Read_Clicked(object sender, EventArgs e)
        {
            LoveAndWarRead loveAndWarRead = new LoveAndWarRead();
            await Navigation.PushAsync(loveAndWarRead);
            loveAndWarRead.DisplayStack();
        }

        private async void Download_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Скачивание", "Успешное скачивание", "Ok");
        }
    }
}