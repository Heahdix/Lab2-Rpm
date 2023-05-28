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
    public partial class Statistics : ContentPage
    {
        Label stackLabel;
        bool loaded = false;
        public Statistics()
        {
            InitializeComponent();
            object kolvo = 0;
            Label name = new Label() { Text="Количество просмотров"};
            Label count = new Label();
            if (App.Current.Properties.TryGetValue("Count", out kolvo))
            {
                count.Text = kolvo.ToString();
            }
            Button backBtn = new Button { Text = "Назад" };
            backBtn.Clicked += BackButton_Clicked;
            stackLabel = new Label();
            Content = new StackLayout { Children = { stackLabel, name, count, backBtn } };
        }
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            ((Admin)navPage.CurrentPage).DisplayStack();
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