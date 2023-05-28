using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lab2_Rpm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        Button takePhotoBtn;
        Button getPhotoBtn;
        Image img;
        Label stackLabel;
        bool loaded = false;
        public Profile()
        {
            InitializeComponent();
            //string dateOfBith = Preferences.Get("BirthDate", null).ToString();
            Label login = new Label() { Text = Preferences.Get("Login", null) };
            Label phoneNumber = new Label() { Text = Preferences.Get("PhoneNumber", null) };
            //Label birthDate = new Label() { Text = dateOfBith };

            takePhotoBtn = new Button { Text = "Сделать фото" };
            getPhotoBtn = new Button { Text = "Выбрать фото" };
            img = new Image();

            object picSource;
            if (App.Current.Properties.TryGetValue("Photo", out picSource))
            {
                img.Source = (ImageSource)picSource;
            }
                // выбор фото
            getPhotoBtn.Clicked += GetPhotoAsync;

            // съемка фото
            takePhotoBtn.Clicked += TakePhotoAsync;
            Button backBtn = new Button { Text = "Назад" };
            backBtn.Clicked += BackButton_Clicked;
            stackLabel = new Label();
            Content = new StackLayout
            {
                Children = { new StackLayout {Children = {takePhotoBtn, getPhotoBtn},
                         Orientation =StackOrientation.Horizontal,
                         HorizontalOptions = LayoutOptions.CenterAndExpand}, img, login, phoneNumber, stackLabel, backBtn  }
            };
        }

        async void GetPhotoAsync(object sender, EventArgs e)
        {
            try
            {
                // выбираем фото
                var photo = await MediaPicker.PickPhotoAsync();
                // загружаем в ImageView
                img.Source = ImageSource.FromFile(photo.FullPath);
                App.Current.Properties.Add("Photo", ImageSource.FromFile(photo.FullPath));
                await Application.Current.SavePropertiesAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        async void TakePhotoAsync(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                // для примера сохраняем файл в локальном хранилище
                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile)) 
                    await stream.CopyToAsync(newStream);

                // загружаем в ImageView
                img.Source = ImageSource.FromFile(photo.FullPath);
                App.Current.Properties.Add("Photo", ImageSource.FromFile(photo.FullPath));
                await Application.Current.SavePropertiesAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
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
            else if (prev is Librarian)
            {
                ((Librarian)prev).DisplayStack();
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            object source;
            if (App.Current.Properties.TryGetValue("Photo", out source))
            {
                img.Source = (ImageSource)source;

            }
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