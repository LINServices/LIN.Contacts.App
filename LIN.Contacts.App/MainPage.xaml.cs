namespace LIN.Contacts.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Application.Current.RequestedThemeChanged += (s, a) =>
            {
                MauiProgram.Aa();
            };
        }


        protected override void OnAppearing()
        {
            MauiProgram.Aa();
            base.OnAppearing();
        }
    }
}
