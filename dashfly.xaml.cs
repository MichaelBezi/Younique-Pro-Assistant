namespace Younique_Pro_Assistant
{
    public partial class dashfly : FlyoutPage
    {
        public dashfly()
        {
            InitializeComponent();
            flyoutpage.btn.Clicked += OpenSecondPageClicked;
            flyoutpage.btnprof.Clicked += OpenProf;
        }
        private void OpenSecondPageClicked(object sender, EventArgs e)
        {
            if (!((IFlyoutPageController)this).ShouldShowSplitMode)
                IsPresented = false;
            Detail = new NavigationPage(new Frmdash());
        }

        private void OpenProf(object sender, EventArgs e)
        {
            if (!((IFlyoutPageController)this).ShouldShowSplitMode)
                IsPresented = false;
            Detail = new NavigationPage(new MainPage());
        }
    }
}
