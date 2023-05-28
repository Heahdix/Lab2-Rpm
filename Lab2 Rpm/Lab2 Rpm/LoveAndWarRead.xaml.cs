using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab2_Rpm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoveAndWarRead : ContentPage
    {
        Label stackLabel;
        Label soder = new Label();
        bool loaded = false;
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public LoveAndWarRead()
        {
            InitializeComponent();
            if (File.Exists(Path.Combine(folderPath, "LoveAndWar.txt")))
                soder.Text = File.ReadAllText(Path.Combine(folderPath, "LoveAndWar.txt"));
            Button backBtn = new Button { Text = "Назад" };
            backBtn.Clicked += BackButton_Clicked;
            stackLabel = new Label();
            Content = new StackLayout { Children = { stackLabel, soder, backBtn } };
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            ((LoveAndWar)navPage.CurrentPage).DisplayStack();
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
    }
}