namespace Younique_Pro_Assistant
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Dark;
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
