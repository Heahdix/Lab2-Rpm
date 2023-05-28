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
    public partial class Librarian : ContentPage
    {
        Label stackLabel;
        bool loaded = false;
        public Librarian()
        {
            InitializeComponent();
            Button books = new Button() { Text = "Книги" };
            books.Clicked += ToBooks_Clicked;

            Button profile = new Button() { Text = "Профиль" };
            profile.Clicked += ToProfile_Clicked;

            Button bookRedact = new Button() { Text = "Редактирование книг" };
            bookRedact.Clicked += ToRedactBook_Clicked;

            Button bookDelete = new Button() { Text = "Удаление книг" };
            bookDelete.Clicked += bookDelete_Clicked;

            Button bookAdd = new Button() { Text = "Добавление книг" };
            bookAdd.Clicked += bookAdd_Clicked;

            Button backBtn = new Button { Text = "Назад" };
            backBtn.Clicked += BackButton_Clicked;

            stackLabel = new Label();
            Content = new StackLayout { Children = { books, profile, bookAdd, bookDelete, bookRedact, stackLabel, backBtn } };
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

        private async void bookDelete_Clicked(object sender, EventArgs e)
        {
            DeleteBook deleteBook = new DeleteBook();
            await Navigation.PushAsync(deleteBook);
            deleteBook.DisplayStack();
        }

        private async void ToRedactBook_Clicked(object sender, EventArgs e)
        {
            RedactBook redactBook = new RedactBook();
            await Navigation.PushAsync(redactBook);
            redactBook.DisplayStack();
        }

        private async void bookAdd_Clicked(object sender, EventArgs e)
        {
            AddBook addBook = new AddBook();
            await Navigation.PushAsync(addBook);
            addBook.DisplayStack();
        }
    }
}