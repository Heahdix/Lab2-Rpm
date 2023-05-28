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
    public partial class Books : ContentPage
    {
        Label stackLabel;
        bool loaded = false;
        public Books()
        {
            InitializeComponent();
            Button backBtn = new Button { Text = "Назад" };
            backBtn.Clicked += BackButton_Clicked;
            Button war = new Button { Text = "Война и мир" };
            war.Clicked += War_Clicked;
            stackLabel = new Label();
            Content = new StackLayout { Children = { stackLabel, war, backBtn  } };
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            Page prev = navPage.Navigation.NavigationStack[navPage.Navigation.NavigationStack.Count - 1];
            if (prev is Admin)
            {
                ((Admin)prev).DisplayStack();
            }
            else if(prev is Librarian)
            {
                ((Librarian)prev).DisplayStack();
            }
        }
        private async void War_Clicked(object sender, EventArgs e)
        {
            LoveAndWar loveAndWar = new LoveAndWar();
            await Navigation.PushAsync(loveAndWar);
            loveAndWar.DisplayStack();
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